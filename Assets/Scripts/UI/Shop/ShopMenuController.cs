using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    GameSessionController gameSession;
    ShopCoins shopCoins;
    int lifePlusCost = 3;
    int reloadRocketCost = 1;
    int rocketPlusCost = 2;
    int shieldTimeCost = 2;
    int maxShieldCost = 4;

    void Start()
    {
        gameSession = FindObjectOfType<GameSessionController>();
        shopCoins = FindObjectOfType<ShopCoins>();
        
        UpdateShopCoins();
        SetupShopMenu();
        CheckCoinAvailability();
    }

    private void SetupShopMenu()
    {
        if(gameSession.Lives == 3)
            DisableLifePlus();

        if(gameSession.Rockets == gameSession.MaxRockets)
            DisableReloadRocket();

        if(gameSession.ShieldRechargeTime <= 1f)
            DisableShieldTime();

        if(gameSession.Shield == 100)
            DisableMaxShield();
    }

    private void UpdateShopCoins()
    {
        shopCoins.CurrentCoins = gameSession.Coins;
    }

    private void CheckCoinAvailability()
    {
        if(gameSession.Coins < lifePlusCost)
            DisableLifePlus();
        
        if(gameSession.Coins < reloadRocketCost)
            DisableReloadRocket();
        
        if(gameSession.Coins < rocketPlusCost)
            DisableRocketPlus();
        
        if(gameSession.Coins < shieldTimeCost)
            DisableShieldTime();
        
        if(gameSession.Coins < maxShieldCost)
            DisableMaxShield();
    }

    private void DisableLifePlus()
    {
        GameObject lifeMenuItem = GameObject.Find("MenuItemLifePlus");
        DisablePurchase(lifeMenuItem);
    }

    private void DisableReloadRocket()
    {
        GameObject reloadRocketMenuItem = GameObject.Find("MenuItemReloadRocket");
        DisablePurchase(reloadRocketMenuItem);
    }

    private void DisableRocketPlus()
    {
        GameObject rocketPlusMenuItem = GameObject.Find("MenuItemRocketPlus");
        DisablePurchase(rocketPlusMenuItem);
    }

    private void DisableShieldTime()
    {
        GameObject shieldTimeMenuItem = GameObject.Find("MenuItemShieldTime");
        DisablePurchase(shieldTimeMenuItem);
    }

    private void DisableMaxShield()
    {
        GameObject maxShieldMenuItem = GameObject.Find("MenuItemMaxShield");
        DisablePurchase(maxShieldMenuItem);
    }

    private void DisablePurchase(GameObject menuItemToDisable)
    {
        Button menuItemButton = menuItemToDisable.GetComponent<Button>();
        Image menuItemImage = menuItemButton.GetComponent<Image>();
        menuItemButton.enabled = false;
        menuItemImage.color = Color.gray;   
    }

    public void PurchaseLifePlus()
    {
        gameSession.Coins -= lifePlusCost;
        gameSession.Lives += 1;
        
        UpdateShopCoins();
        DisableLifePlus();
        CheckCoinAvailability();
    }

    public void PurchaseReloadRocket()
    {
        gameSession.Coins -= reloadRocketCost;
        gameSession.Rockets = gameSession.MaxRockets;

        UpdateShopCoins();
        DisableReloadRocket();
        CheckCoinAvailability();
        
    }

    public void PurchaseRocketPlus()
    {
        gameSession.Coins -= rocketPlusCost;
        gameSession.MaxRockets += 1;
        gameSession.Rockets = gameSession.MaxRockets;

        UpdateShopCoins();
        CheckCoinAvailability();
    }

    public void PurchaseShieldTime()
    {
        gameSession.Coins -= shieldTimeCost;
        gameSession.ShieldRechargeTime -= 0.5f;

        if(gameSession.ShieldRechargeTime <= 1f)
        {
            gameSession.ShieldRechargeTime = 1f;
            DisableShieldTime();
        }

        UpdateShopCoins();
        CheckCoinAvailability();
    }

    public void PurchaseMaxShield()
    {
        gameSession.Coins -= maxShieldCost;
        gameSession.Shield += 10;

        if(gameSession.Shield >= 100)
        {
            gameSession.Shield = 100;
            DisableMaxShield();
        }

        UpdateShopCoins();
        CheckCoinAvailability();
    }
}
