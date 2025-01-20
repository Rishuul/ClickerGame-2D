using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;

public class UpgradeManager : MonoBehaviour
{
    public bool isUpgradeMenuOpen = false;


    public ClickButton clickButton;

    public ShopManager shopManager;

    public BurgerPlace burgerPlace;

    public CoinManager coinManager;
    public GameObject upgradeMenu;

    public bool boughtBurgerPlace;

    public Button burgerUpgradeButton;
    public Color greyedColor;

    private Color originalColour;

    //public int costOfMoreClicksPerSecond;

    public int upgradeCostofBurgerPlace;

    public int burgerPlaceUpgradeIncrement;

    public int burgerPlaceUpgradeCostIncrement;

    //public TextMeshProUGUI costofMoreClicksPerSecondText;

    public TextMeshProUGUI upgradeCostofBurgerPlaceText;

    public TextMeshProUGUI currentEarningsPerBurgerCollectText;

    public TextMeshProUGUI coinsText;

    //public TextMeshProUGUI currentClicksPerSecondText;
    void Start()
    {
        originalColour = burgerUpgradeButton.image.color;
        upgradeCostofBurgerPlace = 1000;
        burgerPlaceUpgradeIncrement=50;
        burgerPlaceUpgradeCostIncrement=300;
        upgradeCostofBurgerPlaceText.text = upgradeCostofBurgerPlace.ToString();
        currentEarningsPerBurgerCollectText.text = "Current Earnings: " + burgerPlace.burgerPlaceEarnings;
    }

    void Update()
    {
        if(isUpgradeMenuOpen)
        {
            if(shopManager.boughtBurgerPlace)
            {
                burgerUpgradeButton.interactable=true;
                upgradeCostofBurgerPlaceText.text = coinManager.FormatCoins(upgradeCostofBurgerPlace);
                if(coinManager.coins>=upgradeCostofBurgerPlace)
                {
                    burgerUpgradeButton.image.color = originalColour;
                }
                else
                {
                    burgerUpgradeButton.image.color = greyedColor;
                }
            }
            else
            {
                burgerUpgradeButton.image.color = greyedColor;
                upgradeCostofBurgerPlaceText.text = "Not Owned!";
                burgerUpgradeButton.interactable=false;
            }
            
        }
        coinsText.text = coinManager.coins.ToString();
        currentEarningsPerBurgerCollectText.text = "Current earnings: "+coinManager.FormatCoins(burgerPlace.burgerPlaceEarnings);
        
    }

    public void OpenUpgradeMenu()
    {
        isUpgradeMenuOpen=true;
        clickButton.inputActions.Taps.Disable();
        upgradeMenu.SetActive(true);
    }

    public void CloseUpgradeMenu()
    {
        isUpgradeMenuOpen=false;
        clickButton.inputActions.Taps.Enable();
        upgradeMenu.SetActive(false);
    }

    public void UpgradeBurgerPlace()
    {
        if(shopManager.boughtBurgerPlace&&coinManager.coins>=upgradeCostofBurgerPlace)
        {
            coinManager.coins-=upgradeCostofBurgerPlace;
            burgerPlace.burgerPlaceLevel+=1;
            burgerPlace.burgerPlaceEarnings+=burgerPlaceUpgradeIncrement;
            upgradeCostofBurgerPlace+=burgerPlaceUpgradeCostIncrement;
            upgradeCostofBurgerPlaceText.text = coinManager.FormatCoins(upgradeCostofBurgerPlace);
            // GameObject particles = Instantiate(clickButton.moneyParticleSystem,Camera.main.WorldToScreenPoint(burgerUpgradeButton.transform.position),Quaternion.identity);
            // ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            // if(particleSystem!=null)
            // {
            //     particleSystem.Play();
            //     Debug.Log("Particle system instantiated!");
            // }
            // Destroy(particles,1f);
            Debug.Log("Burger Place Upgraded!");
        }
    }
}
