using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BurgerPlace : MonoBehaviour
{
    public CoinManager coinManager;

    public GameObject burgerPlaceUI;

    public GameObject burgerPlaceCollectIconUI;

    public TextMeshProUGUI burgerCollectCooldownTimeText;
    public ShopManager shopManager;
    public int burgerPlaceEarnings;

    private Button burgerPlaceButton;
    public bool isBurgerPlaceReadyToCollect;

    public int burgerPlaceLevel;
    
    public int burgerPlacePhase;
    public int burgerCollectTime = 15;

    public float burgerTimer;
    void Start()
    {
        burgerPlaceEarnings = 200;
        burgerPlaceLevel=1;
        isBurgerPlaceReadyToCollect=false;
        burgerPlaceButton = GetComponent<Button>();
        burgerPlaceButton.interactable=false;
        
    }

    void Update()
    {

        if(shopManager.boughtBurgerPlace)
        {
            if(isBurgerPlaceReadyToCollect)
            {
                burgerCollectCooldownTimeText.text=  "Ready to collect!";
                burgerPlaceButton.interactable=true;
                burgerPlaceCollectIconUI.SetActive(true);
            }
            else
            {
                float timeRemaining = burgerCollectTime-(Time.time-burgerTimer);
                if(timeRemaining<=0)
                {
                    burgerCollectCooldownTimeText.text=  "Ready to collect!";
                    isBurgerPlaceReadyToCollect=true;
                    burgerPlaceCollectIconUI.SetActive(true);
                    
                }
                else
                {
                    burgerCollectCooldownTimeText.text = "Collect in: "+Mathf.CeilToInt(timeRemaining);
                }
                
            }
        }
        
    }

    public void CollectBurgerMoney()
    {
        coinManager.coins+= burgerPlaceEarnings;
        isBurgerPlaceReadyToCollect=false;
        burgerTimer=Time.time;
        burgerPlaceCollectIconUI.SetActive(false);
        burgerPlaceButton.interactable=false;
    }
}
