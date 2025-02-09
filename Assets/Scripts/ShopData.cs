using UnityEngine;

public class ShopData
{   
    public bool boughtBurgerPlace;
    
    public int costOfMoreClicksPerSecond;

    public ShopData(ShopManager shopManager)
    {
        boughtBurgerPlace = shopManager.boughtBurgerPlace;
        costOfMoreClicksPerSecond = shopManager.currentClickUpgradeCost;
       
    }    public ShopData()
    {
        boughtBurgerPlace = false;
        costOfMoreClicksPerSecond = 100;
    }
}