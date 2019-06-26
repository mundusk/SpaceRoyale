using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    [SerializeField] AudioClip itemPurchaseAudio;
    [SerializeField] [Range(0,1)] float itemPurchaseClipVolume = 0.7f;

    ShopCoins shopCoins;
    ShopFeedback shopFeedback;
    int lifePlusCost = 3;
    int reloadRocketCost = 1;
    int rocketPlusCost = 2;
    int shieldTimeCost = 2;
    int maxShieldCost = 4;

    void Start()
    {
        shopCoins = FindObjectOfType<ShopCoins>();
        shopFeedback = FindObjectOfType<ShopFeedback>();
        
        UpdateShopCoins();
        SetupShopMenu();
        CheckCoinAvailability();
    }

    private void SetupShopMenu()
    {
        if(GameSessionController.Instance.Lives == 3)
            DisableLifePlus();

        if(GameSessionController.Instance.Rockets == GameSessionController.Instance.MaxRockets)
            DisableReloadRocket();

        if(GameSessionController.Instance.ShieldRechargeTime <= 1f)
            DisableShieldTime();

        if(GameSessionController.Instance.Shield == 100)
            DisableMaxShield();
    }

    private void UpdateShopCoins()
    {
        shopCoins.CurrentCoins = GameSessionController.Instance.Coins;
    }

    private void CheckCoinAvailability()
    {
        if(GameSessionController.Instance.Coins < lifePlusCost)
            DisableLifePlus();
        
        if(GameSessionController.Instance.Coins < reloadRocketCost)
            DisableReloadRocket();
        
        if(GameSessionController.Instance.Coins < rocketPlusCost)
            DisableRocketPlus();
        
        if(GameSessionController.Instance.Coins < shieldTimeCost)
            DisableShieldTime();
        
        if(GameSessionController.Instance.Coins < maxShieldCost)
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
        GameSessionController.Instance.Coins -= lifePlusCost;
        GameSessionController.Instance.Lives += 1;
        
        PurchaseSuccessful();
        UpdateShopCoins();
        DisableLifePlus();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Life increased + 1!";
    }

    public void PurchaseReloadRocket()
    {
        GameSessionController.Instance.Coins -= reloadRocketCost;
        GameSessionController.Instance.Rockets = GameSessionController.Instance.MaxRockets;

        PurchaseSuccessful();
        UpdateShopCoins();
        DisableReloadRocket();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Rockets fully reloaded!";
    }

    public void PurchaseRocketPlus()
    {
        GameSessionController.Instance.Coins -= rocketPlusCost;
        GameSessionController.Instance.MaxRockets += 1;
        GameSessionController.Instance.Rockets = GameSessionController.Instance.MaxRockets;

        PurchaseSuccessful();
        UpdateShopCoins();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Rockets reloaded and max capacity + 1!";
    }

    public void PurchaseShieldTime()
    {
        GameSessionController.Instance.Coins -= shieldTimeCost;
        GameSessionController.Instance.ShieldRechargeTime -= 0.5f;

        if(GameSessionController.Instance.ShieldRechargeTime <= 1f)
        {
            GameSessionController.Instance.ShieldRechargeTime = 1f;
            DisableShieldTime();
        }

        PurchaseSuccessful();
        UpdateShopCoins();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Shield recharge time decreased by 0.5!";
    }

    public void PurchaseMaxShield()
    {
        GameSessionController.Instance.Coins -= maxShieldCost;
        GameSessionController.Instance.Shield += 10;

        if(GameSessionController.Instance.Shield >= 100)
        {
            GameSessionController.Instance.Shield = 100;
            DisableMaxShield();
        }

        PurchaseSuccessful();
        UpdateShopCoins();
        CheckCoinAvailability();

        shopFeedback.PurchaseInfo = "Max shield + 10!";
    }
}
