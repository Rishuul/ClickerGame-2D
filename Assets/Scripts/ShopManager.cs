using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public bool isShopOpen = false;

    public ClickButton clickButton;

    public BurgerPlace burgerPlace;

    public CoinManager coinManager;
    public GameObject shopMenu;

    public bool boughtBurgerPlace;

    public Button buyButton1,buyButton2;
    public Color greyedColor;

    private Color originalColour;

    public int costOfMoreClicksPerSecond;

    public int costofBurgerPlace;

    public TextMeshProUGUI costofMoreClicksPerSecondText;

    public TextMeshProUGUI costofBurgerPlaceText;

    public TextMeshProUGUI currentClicksPerSecondText;
    void Start()
    {
        boughtBurgerPlace=false;
        originalColour = buyButton1.image.color;
        costOfMoreClicksPerSecond = 50;
        costofBurgerPlace = 1000;
        costofMoreClicksPerSecondText.text = coinManager.FormatCoins(costOfMoreClicksPerSecond);
        costofBurgerPlaceText.text = costofBurgerPlace.ToString();
        currentClicksPerSecondText.text= "Current Money Per Click: " + coinManager.FormatCoins(coinManager.coinsPerClick);
    }

    void Update()
    {
        if(isShopOpen)
        {
            if(coinManager.coins<costOfMoreClicksPerSecond)
            {
                buyButton1.image.color = greyedColor;
            }
            else if(coinManager.coins>=costOfMoreClicksPerSecond)
            {
                buyButton1.image.color = originalColour;
            }
            if(!boughtBurgerPlace)
            {
                if(coinManager.coins<costofBurgerPlace)
                {
                    buyButton2.image.color = greyedColor;
                }
                else if(coinManager.coins>=costofBurgerPlace)
                {
                    buyButton2.image.color = originalColour;
                }
            }
            
            
        }
    }

    public void OpenShop()
    {
        isShopOpen=true;
        clickButton.inputActions.Taps.Disable();
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        isShopOpen=false;
        clickButton.inputActions.Taps.Enable();
        shopMenu.SetActive(false);
    }

    public void buyMoreClicksPerSecond()
    {
        if(coinManager.coins<costOfMoreClicksPerSecond)
        {
            Debug.Log("Not enough coins!");
        }
        else
        {
            coinManager.coins-=costOfMoreClicksPerSecond;
            coinManager.coinsPerClick++;
            costOfMoreClicksPerSecond+=30;
            costofMoreClicksPerSecondText.text = coinManager.FormatCoins(costOfMoreClicksPerSecond);
            currentClicksPerSecondText.text = "Current Money Per Click: " + coinManager.FormatCoins(coinManager.coinsPerClick);
            // GameObject particles = Instantiate(clickButton.moneyParticleSystem,Camera.main.WorldToScreenPoint(buyButton1.transform.position),Quaternion.identity);
            // ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            // if(particleSystem!=null)
            // {
            //     particleSystem.Play();
            //     Debug.Log("Particle system instantiated!");
            // }
            // Destroy(particles,1f);
        }
        
    }

    public void buyBurgerPlace()
    {
        if(coinManager.coins<costofBurgerPlace)
        {
            Debug.Log("Not enough coins!");
        }
        else
        {
            boughtBurgerPlace=true;
            coinManager.coins-=costofBurgerPlace;
            costofBurgerPlaceText.text = "Owned";
            buyButton2.image.color = greyedColor;
            buyButton2.interactable = false;
            burgerPlace.burgerPlaceUI.SetActive(true);
            burgerPlace.burgerTimer = Time.time;
            // GameObject particles = Instantiate(clickButton.moneyParticleSystem,Camera.main.WorldToScreenPoint(buyButton2.transform.position),Quaternion.identity);
            // ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            // if(particleSystem!=null)
            // {
            //     particleSystem.Play();
            //     Debug.Log("Particle system instantiated!");
            // }
            // Destroy(particles,1f);
        }
    }

    
}
