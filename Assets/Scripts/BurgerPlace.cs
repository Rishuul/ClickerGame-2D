using System;
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
    public int burgerPlaceLevelsToNextPhase;
    
    public int burgerPlacePhase;
    public float burgerCollectTime = 15f;

    public UnityEngine.UI.Image timeLeftSlider;

    public float burgerTimer;
    void Start()
    {
        burgerPlaceEarnings = 200;
        burgerPlaceLevel=1;
        burgerPlacePhase=1;
        burgerPlaceLevelsToNextPhase=10;
        isBurgerPlaceReadyToCollect=false;
        burgerPlaceButton = GetComponent<Button>();
        burgerPlaceButton.interactable=false;
        timeLeftSlider.fillAmount=0f;
        
    }

    void Update()
    {

        if(shopManager.boughtBurgerPlace)
        {
            if(isBurgerPlaceReadyToCollect)
            {
                burgerCollectCooldownTimeText.text=  "Ready!";
                burgerPlaceButton.interactable=true;
                burgerPlaceCollectIconUI.SetActive(true);
                timeLeftSlider.fillAmount=1f;
            }
            else
            {
                float timeRemaining = burgerCollectTime-(Time.time-burgerTimer);
                if(timeRemaining<=0)
                {
                    burgerCollectCooldownTimeText.text=  "Ready to collect!";
                    isBurgerPlaceReadyToCollect=true;
                    burgerPlaceCollectIconUI.SetActive(true);
                    timeLeftSlider.fillAmount=1f;
                    
                }
                else
                {
                    burgerCollectCooldownTimeText.text = Mathf.CeilToInt(timeRemaining).ToString();
                    timeLeftSlider.fillAmount=1-(timeRemaining/burgerCollectTime);
                }
                
            }
        }
        
    }
    public event Action OnCoinsChanged;
    public void CollectBurgerMoney()
    {
        coinManager.coins+= burgerPlaceEarnings;
        isBurgerPlaceReadyToCollect=false;
        burgerTimer=Time.time;
        burgerPlaceCollectIconUI.SetActive(false);
        burgerPlaceButton.interactable=false;
        OnCoinsChanged?.Invoke();
    }
}
