using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovieTheatre : MonoBehaviour
{
    public CoinManager coinManager;

    public GameObject movieTheatreUI;

    public GameObject moviePlaceCollectIconUI;

    public TextMeshProUGUI movieCollectCooldownTimeText;
    public ShopManager shopManager;
    public int movieTheatreEarnings;

    private Button movieTheatreButton;
    public bool isMovieTheatreReadyToCollect;

    public int movieTheatreLevel;
    public int movieTheatreLevelsToNextPhase;
    
    public int movieTheatrePhase;
    public float movieTheatreCollectTime = 15f;

    public UnityEngine.UI.Image timeLeftSlider;

    public float movieTimer;
    void Start()
    {
        movieTheatreEarnings = 500;
        movieTheatreLevel=1;
        movieTheatrePhase=1;
        movieTheatreLevelsToNextPhase=10;
        isMovieTheatreReadyToCollect=false;
        movieTheatreButton = GetComponent<Button>();
        movieTheatreButton.interactable=false;
        timeLeftSlider.fillAmount=0f;
        
    }

    void Update()
    {

        if(shopManager.boughtMovieTheatre)
        {
            if(isMovieTheatreReadyToCollect)
            {
                movieCollectCooldownTimeText.text=  "Ready!";
                movieTheatreButton.interactable=true;
                moviePlaceCollectIconUI.SetActive(true);
                timeLeftSlider.fillAmount=1f;
            }
            else
            {
                float timeRemaining = movieTheatreCollectTime-(Time.time-movieTimer);
                if(timeRemaining<=0)
                {
                    movieCollectCooldownTimeText.text=  "Ready to collect!";
                    isMovieTheatreReadyToCollect=true;
                    moviePlaceCollectIconUI.SetActive(true);
                    timeLeftSlider.fillAmount=1f;
                    
                }
                else
                {
                    movieCollectCooldownTimeText.text = Mathf.CeilToInt(timeRemaining).ToString();
                    timeLeftSlider.fillAmount=1-(timeRemaining/movieTheatreCollectTime);
                }
                
            }
        }
        
    }
    public event Action OnCoinsChanged;
    public void CollectMovieMoney()
    {
        coinManager.coins+= movieTheatreEarnings;
        isMovieTheatreReadyToCollect=false;
        movieTimer=Time.time;
        moviePlaceCollectIconUI.SetActive(false);
        movieTheatreButton.interactable=false;
        OnCoinsChanged?.Invoke();
    }
}
