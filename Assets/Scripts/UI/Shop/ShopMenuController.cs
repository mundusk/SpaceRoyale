using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] AudioClip itemPurchaseAudio;
    [SerializeField] [Range(0,1)] float itemPurchaseClipVolume = 0.7f;

    GameSessionController gameSession;
    ShopCoins shopCoins;
    ShopFeedback shopFeedback;
    int lifePlusCost = 3;
    int reloadRocketCost = 1;
    int rocketPlusCost = 2;
    int shieldTimeCost = 2;
    int maxShieldCost = 4;

    void Start()
    {
        gameSession = FindObjectOfType<GameSessionController>();
        shopCoins = FindObjectOfType<ShopCoins>();
        shopFeedback = FindObjectOfType<ShopFeedback>();
        
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

    private void PurchaseSuccessful()
    {
        AudioSource.PlayClipAtPoint(
            itemPurchaseAudio,
            Camera.main.transform.position,
            itemPurchaseClipVolume);
    }

    public void PurchaseLifePlus()
    {
        gameSession.Coins -= lifePlusCost;
        gameSession.Lives += 1;
        
        PurchaseSuccessful();
        UpdateShopCoins();
        DisableLifePlus();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Life increased + 1!";
    }

    public void PurchaseReloadRocket()
    {
        gameSession.Coins -= reloadRocketCost;
        gameSession.Rockets = gameSession.MaxRockets;

        PurchaseSuccessful();
        UpdateShopCoins();
        DisableReloadRocket();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Rockets fully reloaded!";
    }

    public void PurchaseRocketPlus()
    {
        gameSession.Coins -= rocketPlusCost;
        gameSession.MaxRockets += 1;
        gameSession.Rockets = gameSession.MaxRockets;

        PurchaseSuccessful();
        UpdateShopCoins();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Rockets reloaded and max capacity + 1!";
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

        PurchaseSuccessful();
        UpdateShopCoins();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Shield recharge time decreased by 0.5!";
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

        PurchaseSuccessful();
        UpdateShopCoins();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Max shield + 10!";
    }
}
