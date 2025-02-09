using UnityEngine;

public class UpgradeData
{
    public bool boughtBurgerPlace;
    public int upgradeCostofBurgerPlace;
    public int currentPPS;
    public int burgerPlaceUpgradeIncrement;
    public int burgerPlaceUpgradeCostIncrement;

    public UpgradeData(UpgradeManager upgradeManager)
    {
        boughtBurgerPlace = upgradeManager.boughtBurgerPlace;
        upgradeCostofBurgerPlace = upgradeManager._currentBurgerUpgradeCost;
        currentPPS = upgradeManager.burgerPlaceEarnings;
        burgerPlaceUpgradeIncrement = upgradeManager.burgerPlaceUpgradeIncrement;
        burgerPlaceUpgradeCostIncrement = upgradeManager.burgerPlaceUpgradeCostIncrement;
    }
}