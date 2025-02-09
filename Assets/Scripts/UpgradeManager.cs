using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("References")]
    public ClickButton clickButton;
    public ShopManager shopManager;
    public BurgerPlace burgerPlace;
    public CoinManager coinManager;
    public MovieTheatre movieTheatre;
    public GameObject upgradeMenu;
    public Button burgerUpgradeButton;
    public TextMeshProUGUI upgradeCostofBurgerPlaceText;
    public TextMeshProUGUI currentEarningsPerBurgerCollectText;
    public Button movieTheatreUpgradeButton;
    public TextMeshProUGUI upgradeCostofMovieTheatreText;
    public TextMeshProUGUI currentEarningsPerMovieTheatreCollectText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI burgerPlaceLevelToNextPhaseLeftText;
    public Image burgerPlaceProgressBarSlider;
    public TextMeshProUGUI movieTheatreLevelToNextPhaseLeftText;
    public Image movieTheatreProgressBarSlider;

    [Header("Settings")]
    public Color greyedColor;
    public int baseBurgerUpgradeCost = 1000;
    public int burgerPlaceUpgradeIncrement = 50;
    public int burgerPlaceUpgradeCostIncrement = 300;
    public int baseMovieTheatreUpgradeCost = 1000;
    public int movieTheatreUpgradeIncrement = 50;
    public int movieTheatreUpgradeCostIncrement = 300;

    public Color _originalColour;
    public int _currentBurgerUpgradeCost;
    public int _currentMovieTheatreUpgradeCost;
    public bool _isInitialized;
    public bool boughtBurgerPlace;

    public bool boughtMovieTheatre;
    public int theatreUpgradeIncrement = 200;
    public float theatreTimeReduction = 0.9f;
    public int burgerPlaceEarnings;
    public int movieTheatreEarnings;

    void Start()
    {
        InitializeUpgradeSystem();
        coinManager.OnCoinsChanged += UpdateUI;
        burgerPlace.OnCoinsChanged +=UpdateUI;
        movieTheatre.OnCoinsChanged +=UpdateUI;
        boughtBurgerPlace= shopManager.boughtBurgerPlace;
        burgerPlaceEarnings = burgerPlace.burgerPlaceEarnings;
        boughtMovieTheatre = shopManager.boughtMovieTheatre;
        movieTheatreEarnings = movieTheatre.movieTheatreEarnings;
    }

    void OnDestroy()
    {
        if (coinManager != null)
        {
            coinManager.OnCoinsChanged -= UpdateUI;
        }
    }

    public void ToggleUpgradeMenu(bool state)
    {
        upgradeMenu.SetActive(state);

        if(state)
        {
            clickButton.inputActions.Taps.Disable();
        }
        else
        {
            clickButton.inputActions.Taps.Enable();
        }
        
        if (state) 
        {
            UpdateUI();
            burgerUpgradeButton.interactable = shopManager.boughtBurgerPlace;
            movieTheatreUpgradeButton.interactable = shopManager.boughtMovieTheatre;
        }
    }

    public void UpgradeBurgerPlace()
    {
        if (!CanUpgradeBurgerPlace()) return;

        PerformBurgerUpgrade();
        //UpdateBurgerPlaceProgress();
        HandleBurgerPhaseTransition();
        UpdateUI();
    }

    public void InitializeUpgradeSystem()
    {
        _originalColour = burgerUpgradeButton.image.color;
        _currentBurgerUpgradeCost = baseBurgerUpgradeCost;
        _currentMovieTheatreUpgradeCost = baseMovieTheatreUpgradeCost;

        UpdateProgressUI();
        _isInitialized = true;
    }

    public void UpdateUI()
    {
        if (!_isInitialized) return;

        coinsText.text = coinManager.FormatCoins(coinManager.coins);
        currentEarningsPerBurgerCollectText.text = $"Current earnings: {coinManager.FormatCoins(burgerPlace.burgerPlaceEarnings)}";
        currentEarningsPerMovieTheatreCollectText.text = $"Current earnings: {coinManager.FormatCoins(movieTheatre.movieTheatreEarnings)}";
        UpdateButtonState();
        UpdateProgressUI();
    }

    public void UpdateButtonState()
    {
        var canAffordBurgerPlace = coinManager.coins >= _currentBurgerUpgradeCost;
        var canAffordMovieTheatre = coinManager.coins >= _currentMovieTheatreUpgradeCost;
        burgerUpgradeButton.image.color = canAffordBurgerPlace ? _originalColour : greyedColor;
        burgerUpgradeButton.interactable = canAffordBurgerPlace && shopManager.boughtBurgerPlace;
        movieTheatreUpgradeButton.image.color = canAffordMovieTheatre ? _originalColour : greyedColor;
        movieTheatreUpgradeButton.interactable = canAffordMovieTheatre && shopManager.boughtMovieTheatre;
    }

    public void UpdateProgressUI()
    {
        burgerPlaceLevelToNextPhaseLeftText.text = $"{burgerPlace.burgerPlaceLevel}/{burgerPlace.burgerPlaceLevelsToNextPhase}";
        burgerPlaceProgressBarSlider.fillAmount = (float)burgerPlace.burgerPlaceLevel / burgerPlace.burgerPlaceLevelsToNextPhase;
        upgradeCostofBurgerPlaceText.text = shopManager.boughtBurgerPlace 
            ? coinManager.FormatCoins(_currentBurgerUpgradeCost) 
            : "Not Owned!";
        movieTheatreLevelToNextPhaseLeftText.text = $"{movieTheatre.movieTheatreLevel}/{movieTheatre.movieTheatreLevelsToNextPhase}";
        movieTheatreProgressBarSlider.fillAmount = (float)movieTheatre.movieTheatreLevel / movieTheatre.movieTheatreLevelsToNextPhase;
        upgradeCostofMovieTheatreText.text = shopManager.boughtMovieTheatre 
            ? coinManager.FormatCoins(_currentMovieTheatreUpgradeCost) 
            : "Not Owned!";
    }

    public bool CanUpgradeBurgerPlace()
    {
        return shopManager.boughtBurgerPlace && 
               coinManager.coins >= _currentBurgerUpgradeCost;
    }
    public bool CanUpgradeMovieTheatre()
    {
        return shopManager.boughtMovieTheatre && 
               coinManager.coins >= _currentMovieTheatreUpgradeCost;
    }

    public void PerformBurgerUpgrade()
    {
        coinManager.SpendCoins(_currentBurgerUpgradeCost);
        burgerPlace.burgerPlaceLevel++;
        burgerPlace.burgerPlaceEarnings += burgerPlaceUpgradeIncrement;
        _currentBurgerUpgradeCost += burgerPlaceUpgradeCostIncrement;
    }
    public void PerformMovieTheatreUpgrade()
    {
        coinManager.SpendCoins(_currentMovieTheatreUpgradeCost);
        movieTheatre.movieTheatreLevel++;
        movieTheatre.movieTheatreEarnings += movieTheatreUpgradeIncrement;
        _currentMovieTheatreUpgradeCost += movieTheatreUpgradeCostIncrement;
    }

    public void HandleBurgerPhaseTransition()
    {
        if (burgerPlace.burgerPlaceLevel >= burgerPlace.burgerPlaceLevelsToNextPhase)
        {
            burgerPlace.burgerPlacePhase++;
            burgerPlace.burgerPlaceLevelsToNextPhase += 50;
            burgerPlace.burgerCollectTime *= 0.5f;
        }
    }
    public void HandleMovieTheatrePhaseTransition()
    {
        if (movieTheatre.movieTheatreLevel >= movieTheatre.movieTheatreLevelsToNextPhase)
        {
            movieTheatre.movieTheatrePhase++;
            movieTheatre.movieTheatreLevelsToNextPhase += 50;
            movieTheatre.movieTheatreCollectTime *= 0.5f;
        }
    }
    public void UpgradeMovieTheatre()
    {
        if (!CanUpgradeMovieTheatre()) return;

        PerformMovieTheatreUpgrade();
        //UpdateBurgerPlaceProgress();
        HandleMovieTheatrePhaseTransition();
        UpdateUI();
    }
}