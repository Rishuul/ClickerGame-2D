using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public int coins=0;

    public int coinsPerClick=1;

    public int diamonds=0;

    public TextMeshProUGUI coinsText;

    public TextMeshProUGUI coinsText1;

    public TextMeshProUGUI diamondsText;

    void Awake()
    {
        Application.targetFrameRate=60;
    }
    void Update()
    {
        
        coinsText.text = FormatCoins(coins);
        coinsText1.text = FormatCoins(coins);

        
        
    }

     public string FormatCoins(int amount)
    {
        if (amount >= 1_000_000_000) // Billions
            return (amount / 1_000_000_000f).ToString("0.##") + "B";
        if (amount >= 1_000_000) // Millions
            return (amount / 1_000_000f).ToString("0.##") + "M";
        if (amount >= 1_000) // Thousands
            return (amount / 1_000f).ToString("0.##") + "K";

        return amount.ToString(); // Less than 1000
    }
}
