using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    public GameObject achievementsMenu;
    public ClickButton clickButton;
    public CoinManager coinManager;

    public ShopManager shopManager;
    public bool isAchievementsMenuOpen;
    public bool tapsAchievement;

    public int tapsAchievementTaps;

    public Button buyButton1;

    public int tapsAchievementReward;

    public UnityEngine.UI.Image tapsAchievementProgressBar;

    public TextMeshProUGUI tapsAchievementProgressBarText;

    public TextMeshProUGUI tapsAchievementDescriptionText;
    public TextMeshProUGUI tapsAchievementRewardText;
    void Start()
    {
        isAchievementsMenuOpen = false;
        tapsAchievement=false;
        tapsAchievementTaps=100;
        tapsAchievementReward=5;
        tapsAchievementDescriptionText.text = "Tap "+ tapsAchievementTaps.ToString() + " times!";
        tapsAchievementRewardText.text = tapsAchievementReward.ToString();     
        
    }

    void Update()
    {
        if(clickButton.clicks>=tapsAchievementTaps)
        {
            tapsAchievement=true;
            buyButton1.image.color = shopManager.originalColour;
            buyButton1.interactable=true;
            tapsAchievementProgressBar.fillAmount =1f;
            tapsAchievementProgressBarText.text = "Collect!";
        }
        else
        {
            buyButton1.image.color = shopManager.greyedColor;
            buyButton1.interactable=false;
        }
        tapsAchievementProgressBar.fillAmount = (float)clickButton.clicks/(float)tapsAchievementTaps;
        tapsAchievementProgressBarText.text = clickButton.clicks + "/" + tapsAchievementTaps;
    }

    public void OpenAchivements()
    {
        
        achievementsMenu.SetActive(true);
        isAchievementsMenuOpen=true;
        clickButton.inputActions.Taps.Disable();
        
    }

    public void CloseAchievements()
    {
        achievementsMenu.SetActive(false);
        isAchievementsMenuOpen = false;
        clickButton.inputActions.Taps.Enable();
    }

    public void CollectTapsAchievement()
    {
        coinManager.diamonds+=tapsAchievementReward;
        tapsAchievement=false;
        tapsAchievementTaps+=500;
        tapsAchievementReward+=15;
        tapsAchievementDescriptionText.text = "Tap "+ tapsAchievementTaps.ToString() + " times!";
        tapsAchievementRewardText.text = tapsAchievementReward.ToString();

    }

    public void AchievementNotification()
    {
        Debug.Log("Collect Taps Achievement");
        return;
    }
}
