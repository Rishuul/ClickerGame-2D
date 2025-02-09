using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    public ClickButton clickButton;
    public UpgradeManager upgradeManager;
    public BurgerPlace burgerPlace;
    public MovieTheatre movieTheatre;
    public CoinManager coinManager;
    public GameObject shopMenu;
    public GameObject shopBar;
    public GameObject managerBar;
    
    [Header("UI Elements")]
    public Button buyButton1, buyButton2,buyButton3;
    public Color greyedColor;
    public TextMeshProUGUI currentClickUpgradeCostText;
    public TextMeshProUGUI burgerPlaceCostText;
    public TextMeshProUGUI currentClicksPerSecondText;
    public TextMeshProUGUI movieTheatreCostText;

    [Header("Shop Settings")]
    public int baseClickUpgradeCost = 50;
    public int costIncrement = 30;
    public int burgerPlaceCost = 10;
    public int movieTheatreCost = 20;
    public int currentMovieTheatreUpgradeCost =1000;
    public int movieTheatreUpgradeIncrement = 100;
    public float movieTheatreTimeReduction = 0.5f;
    public Color originalColour;
    public int currentClickUpgradeCost;
    public bool boughtBurgerPlace;
    public bool boughtMovieTheatre;
    public bool isShopOpen;
    public bool isShopBarOpen;
    public bool isManagerBarOpen;

    [Header("Manager Settings")]

    public bool boughtBurgerPlaceManager;

    public int burgerPlaceManagerCost = 10000;

    void Start()
    {
        InitializeShop();
        coinManager.OnCoinsChanged+=UpdateButtonStates;
        burgerPlace.OnCoinsChanged+=UpdateButtonStates;
        movieTheatre.OnCoinsChanged+=UpdateButtonStates;
        movieTheatreCostText.text = movieTheatreCost.ToString();
    }

    void InitializeShop()
    {
        currentClickUpgradeCost =baseClickUpgradeCost;
        originalColour = buyButton1.image.color;
        isShopBarOpen=true;
        isManagerBarOpen=false;
        UpdateCostDisplays();
        UpdateClicksPerSecondDisplay();

        shopMenu.SetActive(false);
    }

    public void ToggleShop(bool state)
    {
        isShopOpen=state;
        shopMenu.SetActive(state);
        if(state)
        {
            clickButton.inputActions.Taps.Disable();
        }
        else
        {
            clickButton.inputActions.Taps.Enable();
        }

        if(state) UpdateButtonStates();
    }
    
    public void OpenManagerBar()
    {
        if(!isManagerBarOpen)
        {
            managerBar.SetActive(true);
            isManagerBarOpen=true;
            shopBar.SetActive(false);
            isShopBarOpen=false;
        }
    }
    public void OpenShopBar()
    {
        if(!isShopBarOpen)
        {
            shopBar.SetActive(true);
            isShopBarOpen=true;
            managerBar.SetActive(false);
            isManagerBarOpen=false;
        }
    }
    void UpdateButtonStates()
    {
        UpdateButtonState(buyButton1,currentClickUpgradeCost);

        if(!boughtBurgerPlace)
        {
            UpdateButtonState(buyButton2,burgerPlaceCost);
        }
        if(!boughtMovieTheatre)
        {
            UpdateButtonState(buyButton3,movieTheatreCost);
        }
    }
    void UpdateButtonState(Button button,int cost)
    {
        button.image.color =coinManager.coins>=cost ? originalColour: greyedColor;
        button.interactable = coinManager.coins>=cost;
    }

    public void BuyMoreClicksPerSecond()
    {
        if(!coinManager.SpendCoins(currentClickUpgradeCost)) return;

        coinManager.coinsPerClick++;
        currentClickUpgradeCost+=costIncrement;

        UpdateCostDisplays();
        UpdateClicksPerSecondDisplay();
    }

    public void BuyBurgerPlace()
    {
        if(!coinManager.SpendCoins(burgerPlaceCost)) return;

        boughtBurgerPlace=true;
        buyButton2.interactable=false;
        buyButton2.image.color = greyedColor;
        burgerPlaceCostText.text = "Owned";

        burgerPlace.burgerPlaceUI.SetActive(true);
        burgerPlace.burgerTimer = Time.time;
    }

    void UpdateCostDisplays()
    {
        currentClickUpgradeCostText.text = coinManager.FormatCoins(currentClickUpgradeCost);
        if(!boughtBurgerPlace) burgerPlaceCostText.text = coinManager.FormatCoins(burgerPlaceCost);
        if(!boughtMovieTheatre) movieTheatreCostText.text = coinManager.FormatCoins(movieTheatreCost);
        
    }

    void UpdateClicksPerSecondDisplay()
    {
        currentClicksPerSecondText.text = $"Current Money Per Click: {coinManager.FormatCoins(coinManager.coinsPerClick)}";
    }

    private void OnDestroy()
    {
        if (coinManager != null)
        {
            coinManager.OnCoinsChanged -= UpdateButtonStates;
        }
    }
    public void BuyMovieTheatre()
    {
        if(!coinManager.SpendCoins(movieTheatreCost)) return;

        boughtMovieTheatre=true;
        buyButton3.interactable=false;
        buyButton3.image.color = greyedColor;
        movieTheatreCostText.text = "Owned";

        movieTheatre.movieTheatreUI.SetActive(true);
        movieTheatre.movieTimer = Time.time;
    }
}
