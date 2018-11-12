using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour 
{
    //=============================================shop=============================
    public Vector2 tickStartRect;
    public RectTransform tickRef, tick2Ref;
    public List<UIId> skins, backgrounds;
    public List<bool> unlockedSkins, unlockedBackgrounds;
    public Text skinPurchaseText, skinPurchaseMathTxt, backgroundPurchaseTxt, backgroundPurchaseMathTxt;
    public List<string> purchaseSkinSTR;
    public List<int> skinPurchasePrice, backgroundsPurchasePrice;
    public List<GameObject> skinsPadlocks, backgroundsPadlock;
    public GameObject skinPurchasePanel;
    public int skinpurchaseCost, backgroundPurchaseCost, skinToUnlock, backgroundToUnlock;
    public bool canPurchaseSkin, purchaseSkin, purchaseBackground;
    public Button skinConfirmButton;
   

    //==============================================================================
    public AudioSource audioSourceRef;
    public static AddsHandler adHandlerRef;
    public SaveLoad saveLoadRef;
    public SaveClass saveRef;
    public bool playerSet, editSettings;
    public int resetId = 1, purchasePrice;
    public GameObject mainPanel, firstMenuPanel, upgradesPanel, upgradesConfirmPanel, shopPanel, settingsPanel, coinShopPanel;
    public string playerName;
    public InputField playerInputField;
    public Text bestDistanceTxt, totalCoinsTxt, upgradesTxt, upgradesEffectTxt, currentCoinsTxt, notEnoughCoinsTxt;
    public Button confirmButton;
    public List<Sprite> upgradeLevels;
    public Image magnetPullImgRef, magnetDurationImgRef, speedBoostImgRef, invincDurationImgRef, sCoinSpawnChanceImgRef, sCoinTresholdImgRef, sCoinMaxSpawnImgRef, gCoinSpawnChanceImgRef,  gCoinTresholdImgRef, gCoinMaxSpawnImgRef;
    public List<int> magnetPullPrices, magnetDurationPrices, speedBoostDurationPrices, invincibilityDurationPrices, sCoinSpawnChancePrices, sCoinDistanceTresholdPrices, sCoinMaxSpawnChancePrices;
    public List<int> gCoinSpawnChancePrices, gCoinDistanceTresholdPrices, gCoinMaxSpawnChancePrices;
    public string upgradeName;
    bool increaseMagnetPull;

    /////////////////////////////////////////////////////////settings 
    public Dropdown controlModeDropdown;
	public Toggle tutorialToggle;



    ////////////////////////////////////////////////////////shop screen





    /// <summary>
    /// /////////////////////upgrades screens
    ///
    public Text magnetPullPriceTxt, magnetDurationPriceTxt, speedBoosDurationPriceTxt, invincibilityDurationPricesTxt, sCoinSpawnChancePriceTxt, sCoinTresholdPriceTxt, sCoinMaxSpawnChancePriceTxt, gCoinSpawnChancePriceTxt, gCoinTresholdPriceTxt, gCoinMaxSpawnChancePriceTxt;
    public GameObject MagnetPullInfoPanel, magnetDurationInfoPanel, speedBoostDurationInfoPanel, invincDurationInfoPanel, sCoinSpawnInfoPanel, sCoinTresholdInfoPanel, sCoinMaxSpawnInfoPanel, gCoinSpawnInfoPanel, gCoinTresholdInfoPanel, gCoinMaxSpawnInfoPanel;

    /// <summary>
    /// coin shop area
    /// </summary>
    public Text purchaseCompletedTxt;
    public GameObject coinpurchaseConfirmPanel;
    public Purchaser purchaserRef;
    public Text buyTier1CoinTxt, buyTier2CoinTxt, buyTier3CoinTxt, buyTier4CoinTxt, buyTier5CoinTxt;




    public Slider musicSlider, soundSlider;
    public Text musicValueTxt, soundsValueTxt;

	// Use this for initialization
	void Start () 
	{
        tickStartRect = tickRef.anchoredPosition;

        
        mainPanel.GetComponent<Animator>().SetBool("Open", true);
        //mainPanel.GetComponent<Animator>().SetBool("Started", true);
        audioSourceRef = GetComponent<AudioSource>();
        saveLoadRef = SaveLoad.saveLoad;
        saveRef = saveLoadRef.savedData;
        adHandlerRef = AddsHandler.adHandler;

        if (saveRef.resetId != resetId)
        {
            saveRef.resetId = resetId;
            ResetPlayer();
            saveLoadRef.Save();
        }

        //Loading skins
        unlockedSkins = saveRef.unlockedSkins;
        unlockedSkins[0] = true;
        unlockedBackgrounds = saveRef.unlockedBackgrounds;
        unlockedBackgrounds[0] = true;


        bestDistanceTxt.text = "" + (int)saveRef.bestDistance;
        totalCoinsTxt.text = "" + saveRef.coinsCollected;
        playerSet = true;
        mainPanel.SetActive(true);
        firstMenuPanel.SetActive(false);



        audioSourceRef.volume = saveRef.musicVolume / 100;
        musicSlider.value = saveRef.musicVolume;
        soundSlider.value = saveRef.soundVolume;
        controlModeDropdown.value = saveRef.controlMode;
#if UNITY_ANDROID
        adHandlerRef.RequestBanner();
#endif




    }
	
	// Update is called once per frame
	void Update () 
	{

        if (playerSet == false)
        {
            playerName = playerInputField.text;
        }

        musicValueTxt.text = "" + musicSlider.value;
        soundsValueTxt.text = "" + soundSlider.value;
        saveRef.musicVolume = (int)musicSlider.value;
        saveRef.soundVolume = (int)soundSlider.value;
        audioSourceRef.volume = musicSlider.value / 100;
        saveRef.controlMode = controlModeDropdown.value;


    }

    public void SetPlayerName()
    {
        playerSet = true;
        saveRef.playerName = playerName;
        mainPanel.SetActive(true);
        firstMenuPanel.SetActive(false);
        saveLoadRef.Save();
    }

    public void StartGame()
    {
#if UNITY_ANDROID
        AddsHandler.adHandler.CloseBanner();
#endif
        SceneManager.LoadScene("LoadingScene");
    }

    

    public void ResetPlayer()
    {
        saveRef.playerName = "";
        saveRef.invincibilityDurationTier = 0;
        saveRef.bestDistance = 0;
        saveRef.speedBoostDurationTier = 0;
        saveRef.coinsCollected = 0;
        saveRef.gCoinChanceTier = 0;
        saveRef.gCoinTresholdTier = 0;
        saveRef.magnetDurationTier = 0;
        saveRef.magnetPullTier = 0;
        saveRef.sCoinChanceTier = 0;
        saveRef.sCoinTresholdTier = 0;
        saveRef.sCoinMaxMultiTiers = 0;
        saveRef.gCoinMaxMultiTiers = 0;
    }

    public void OpenUpgradesMenu()
    {
        //mainPanel.SetActive(false);
        mainPanel.GetComponent<Animator>().SetBool("Started", true);
        mainPanel.GetComponent<Animator>().SetBool("Open", false);
        upgradesPanel.GetComponent<Animator>().SetBool("Open", true);
        upgradesPanel.GetComponent<Animator>().SetBool("Started", true);


        magnetPullImgRef.sprite = upgradeLevels[saveRef.magnetPullTier];
        magnetPullPriceTxt.text = "" + magnetPullPrices[saveRef.magnetPullTier];

        magnetDurationImgRef.sprite = upgradeLevels[saveRef.magnetDurationTier];
        magnetDurationPriceTxt.text = "" + magnetDurationPrices[saveRef.magnetDurationTier];

        speedBoostImgRef.sprite = upgradeLevels[saveRef.speedBoostDurationTier];
        speedBoosDurationPriceTxt.text = "" + speedBoostDurationPrices[saveRef.speedBoostDurationTier];

        invincDurationImgRef.sprite = upgradeLevels[saveRef.invincibilityDurationTier];
        invincibilityDurationPricesTxt.text = "" + invincibilityDurationPrices[saveRef.invincibilityDurationTier];

        sCoinSpawnChanceImgRef.sprite = upgradeLevels[saveRef.sCoinChanceTier];
        sCoinSpawnChancePriceTxt.text = "" + sCoinSpawnChancePrices[saveRef.sCoinChanceTier];

        sCoinTresholdImgRef.sprite = upgradeLevels[saveRef.sCoinTresholdTier];
        sCoinTresholdPriceTxt.text = "" + sCoinDistanceTresholdPrices[saveRef.sCoinTresholdTier];

        sCoinMaxSpawnImgRef.sprite = upgradeLevels[saveRef.sCoinMaxMultiTiers];
        sCoinMaxSpawnChancePriceTxt.text = "" + sCoinMaxSpawnChancePrices[saveRef.sCoinMaxMultiTiers];

        gCoinSpawnChanceImgRef.sprite = upgradeLevels[saveRef.gCoinChanceTier];
        gCoinSpawnChancePriceTxt.text = "" + gCoinSpawnChancePrices[saveRef.gCoinChanceTier];

        gCoinTresholdImgRef.sprite = upgradeLevels[saveRef.gCoinTresholdTier];
        gCoinTresholdPriceTxt.text = "" + gCoinDistanceTresholdPrices[saveRef.gCoinTresholdTier];

        gCoinMaxSpawnImgRef.sprite = upgradeLevels[saveRef.gCoinMaxMultiTiers];
        gCoinMaxSpawnChancePriceTxt.text = "" + gCoinMaxSpawnChancePrices[saveRef.gCoinMaxMultiTiers];
    }

    public void CloseUpgradesPanel()
    {
        //mainPanel.SetActive(true);
        //upgradesPanel.SetActive(false);
        totalCoinsTxt.text = "" + saveRef.coinsCollected;

        mainPanel.GetComponent<Animator>().SetBool("Open", true);
        upgradesPanel.GetComponent<Animator>().SetBool("Open", false);
    }

    public void ConfirmPurchase()
    {
        if (purchasePrice < saveRef.coinsCollected)
        {
            saveRef.coinsCollected -= purchasePrice;
        }
        else
        {
            CancelPurchase();
        }

        switch (upgradeName)
        {
            case "MagnetPull":
                saveRef.magnetPullTier++;
                magnetPullImgRef.sprite = upgradeLevels[saveRef.magnetPullTier];
                magnetPullPriceTxt.text = "" + magnetPullPrices[saveRef.magnetPullTier];
                break;
            case "MagnetDuration":
                saveRef.magnetDurationTier++;
                magnetDurationImgRef.sprite = upgradeLevels[saveRef.magnetDurationTier];
                magnetDurationPriceTxt.text = "" + magnetDurationPrices[saveRef.magnetDurationTier];
                break;
            case "SpeedBoostDuration":
                saveRef.speedBoostDurationTier++;
                speedBoostImgRef.sprite = upgradeLevels[saveRef.speedBoostDurationTier];
                speedBoosDurationPriceTxt.text = "" + speedBoostDurationPrices[saveRef.speedBoostDurationTier];
                break;
            case "InvincibilityDuration":
                saveRef.invincibilityDurationTier++;
                invincDurationImgRef.sprite = upgradeLevels[saveRef.invincibilityDurationTier];
                invincibilityDurationPricesTxt.text = "" + invincibilityDurationPrices[saveRef.invincibilityDurationTier];
                break;
            case "sCoinSpawnChance":
                saveRef.sCoinChanceTier++;
                sCoinSpawnChanceImgRef.sprite = upgradeLevels[saveRef.sCoinChanceTier];
                sCoinSpawnChancePriceTxt.text = "" + sCoinSpawnChancePrices[saveRef.sCoinChanceTier];
                break;
            case "sCoinTreshold":
                saveRef.sCoinTresholdTier++;
                sCoinTresholdImgRef.sprite = upgradeLevels[saveRef.sCoinTresholdTier];
                sCoinTresholdPriceTxt.text = "" + sCoinDistanceTresholdPrices[saveRef.sCoinTresholdTier];
                break;
            case "sCoinMaxSpawnChance":
                saveRef.sCoinMaxMultiTiers++;
                sCoinMaxSpawnImgRef.sprite = upgradeLevels[saveRef.sCoinMaxMultiTiers];
                sCoinMaxSpawnChancePriceTxt.text = "" + sCoinMaxSpawnChancePrices[saveRef.sCoinMaxMultiTiers];
                break;
            case "gCoinSpawnChance":
                saveRef.gCoinChanceTier++;
                gCoinSpawnChanceImgRef.sprite = upgradeLevels[saveRef.gCoinChanceTier];
                gCoinSpawnChancePriceTxt.text = "" + gCoinSpawnChancePrices[saveRef.gCoinChanceTier];
                break;
            case "gCoinTreshold":
                saveRef.gCoinTresholdTier++;
                gCoinTresholdImgRef.sprite = upgradeLevels[saveRef.gCoinTresholdTier];
                gCoinTresholdPriceTxt.text = "" + gCoinDistanceTresholdPrices[saveRef.gCoinTresholdTier];
                break;            
            case "gCoinMaxSpawnChance":
                saveRef.gCoinMaxMultiTiers++;
                gCoinMaxSpawnImgRef.sprite = upgradeLevels[saveRef.gCoinMaxMultiTiers];
                gCoinMaxSpawnChancePriceTxt.text = "" + gCoinMaxSpawnChancePrices[saveRef.gCoinMaxMultiTiers];
                break;



        }
        saveLoadRef.Save();
        upgradesConfirmPanel.SetActive(false);
    }
    public void CancelPurchase()
    {
        upgradeName = "";
        purchasePrice = 0;
        upgradesConfirmPanel.SetActive(false);
    }

    public void BuyMagnetPullDistance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.magnetPullTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }        
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.magnetPullTiers[saveRef.magnetPullTier] + " Upgrade: " + saveLoadRef.magnetPullTiers[saveRef.magnetPullTier + 1];
        upgradesTxt.text = "Magnet Pull Distance for: " + magnetPullPrices[saveRef.magnetPullTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = magnetPullPrices[saveRef.magnetPullTier] ; ;
        upgradeName = "MagnetPull";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
        
    }
    public void BuyMagnetDuration()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.magnetDurationTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.magnetDurationTiers[saveRef.magnetDurationTier] + " Upgrade: " + saveLoadRef.magnetDurationTiers[saveRef.magnetDurationTier + 1];
        upgradesTxt.text = "Magnet Duration for: " + magnetDurationPrices[saveRef.magnetDurationTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = magnetDurationPrices[saveRef.magnetDurationTier]; ;
        upgradeName = "MagnetDuration";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }
    public void BuySpeedBoostDuration()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.speedBoostDurationTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.speedBoostDurationTiers[saveRef.speedBoostDurationTier] + " Upgrade: " + saveLoadRef.speedBoostDurationTiers[saveRef.speedBoostDurationTier + 1];
        upgradesTxt.text = "Speed Boost Duration for: " + speedBoostDurationPrices[saveRef.speedBoostDurationTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = speedBoostDurationPrices[saveRef.speedBoostDurationTier] ; ;
        upgradeName = "SpeedBoostDuration";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuyInvincibilityDuration()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.invincibilityDurationTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.invincibilityDurationTiers[saveRef.invincibilityDurationTier] + " Upgrade: " + saveLoadRef.invincibilityDurationTiers[saveRef.invincibilityDurationTier + 1];
        upgradesTxt.text = "Invincibility Duration for: " + invincibilityDurationPrices[saveRef.invincibilityDurationTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = invincibilityDurationPrices[saveRef.invincibilityDurationTier]; ;
        upgradeName = "InvincibilityDuration";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuySCoinSpawnChance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.sCoinChanceTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.sCoinChanceTiers[saveRef.sCoinChanceTier] + " Upgrade: " + saveLoadRef.sCoinChanceTiers[saveRef.sCoinChanceTier + 1];
        upgradesTxt.text = "Silver Coin Spawn Chance for: " + sCoinSpawnChancePrices[saveRef.sCoinChanceTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = sCoinSpawnChancePrices[saveRef.sCoinChanceTier]; ;
        upgradeName = "sCoinSpawnChance";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuyScoinTresholdDistance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.sCoinTresholdTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.sCoinTresholdTiers[saveRef.sCoinTresholdTier] + " Upgrade: " + saveLoadRef.sCoinTresholdTiers[saveRef.sCoinTresholdTier + 1];
        upgradesTxt.text = "Silver Coin Treshold Distance for: " + sCoinDistanceTresholdPrices[saveRef.sCoinTresholdTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = sCoinDistanceTresholdPrices[saveRef.sCoinTresholdTier]; ;
        upgradeName = "sCoinTreshold";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuyScoinMaxSpawnChance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.sCoinMaxMultiTiers >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.sCoinMaxMultiTiers[saveRef.sCoinMaxMultiTiers] + " Upgrade: " + saveLoadRef.sCoinMaxMultiTiers[saveRef.sCoinMaxMultiTiers + 1];
        upgradesTxt.text = "Silver Coin Max Spawn Chance for: " + sCoinMaxSpawnChancePrices[saveRef.sCoinMaxMultiTiers] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = sCoinMaxSpawnChancePrices[saveRef.sCoinMaxMultiTiers]; ;
        upgradeName = "sCoinMaxSpawnChance";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuyGCoinSpawnChance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.gCoinChanceTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.gCoinChanceTiers[saveRef.gCoinChanceTier] + " Upgrade: " + saveLoadRef.gCoinChanceTiers[saveRef.gCoinChanceTier + 1];
        upgradesTxt.text = "Gold Coin Spawn Chance for: " + gCoinSpawnChancePrices[saveRef.gCoinChanceTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = gCoinSpawnChancePrices[saveRef.gCoinChanceTier]; ;
        upgradeName = "gCoinSpawnChance";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuyGcoinTresholdDistance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.gCoinTresholdTier >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.gCoinTresholdTiers[saveRef.gCoinTresholdTier] + " Upgrade: " + saveLoadRef.gCoinTresholdTiers[saveRef.gCoinTresholdTier + 1];
        upgradesTxt.text = "Gold Coin Treshold Distance for: " + gCoinDistanceTresholdPrices[saveRef.gCoinTresholdTier] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = gCoinDistanceTresholdPrices[saveRef.gCoinTresholdTier]; ;
        upgradeName = "gCoinTreshold";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void BuyGcoinMaxSpawnChance()
    {
        upgradesConfirmPanel.SetActive(true);
        if (saveRef.gCoinMaxMultiTiers >= 5)
        {
            upgradesEffectTxt.text = "You are at max Rank";
            upgradesTxt.text = "";
            currentCoinsTxt.text = "";
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = false;
            return;
        }
        upgradesEffectTxt.text = "Current Effect: " + saveLoadRef.gCoinMaxMultiTiers[saveRef.gCoinMaxMultiTiers] + " Upgrade: " + saveLoadRef.gCoinMaxMultiTiers[saveRef.gCoinMaxMultiTiers + 1];
        upgradesTxt.text = "gold Coin Max Spawn Chance for: " + gCoinMaxSpawnChancePrices[saveRef.gCoinMaxMultiTiers] + " Coins";
        currentCoinsTxt.text = "Current Coins: " + saveRef.coinsCollected;
        purchasePrice = gCoinMaxSpawnChancePrices[saveRef.gCoinMaxMultiTiers]; ;
        upgradeName = "gCoinMaxSpawnChance";
        if (purchasePrice > saveRef.coinsCollected)
        {
            notEnoughCoinsTxt.text = "Not Enough Coins Your Need: " + (purchasePrice - saveRef.coinsCollected);
            confirmButton.interactable = false;
        }
        else
        {
            notEnoughCoinsTxt.text = "";
            confirmButton.interactable = true;
        }
    }

    public void AddCoins()
    {
        saveRef.coinsCollected += 10000;
        totalCoinsTxt.text = "" + saveRef.coinsCollected;
    }

    public void ResetEverything()
    {
        ResetPlayer();
        totalCoinsTxt.text = "" + saveRef.coinsCollected;
        bestDistanceTxt.text = "" + (int)saveRef.bestDistance;
        saveLoadRef.Save();
    }

    //=================================================================Shop panel=================================================

    public void OpenShopPanel()
    {
        shopPanel.GetComponent<Animator>().SetBool("Started", true);
        shopPanel.GetComponent<Animator>().SetBool("Open", true);
        mainPanel.GetComponent<Animator>().SetBool("Started", true);
        mainPanel.GetComponent<Animator>().SetBool("Open", false);

        tickRef.SetParent(skins[saveRef.skinSelected].transform);
        tickRef.anchoredPosition = tickStartRect;

        tick2Ref.SetParent(backgrounds[saveRef.skyboxSet].transform);
        tick2Ref.anchoredPosition = tickStartRect;

        for(int i = 0; i<skinsPadlocks.Count; i++)
        {
            if(unlockedSkins[i])
            {
                skinsPadlocks[i].SetActive(false);
            }
        }
        for(int i = 0; i<backgroundsPadlock.Count; i++)
        {
            if(unlockedBackgrounds[i])
            {
                backgroundsPadlock[i].SetActive(false);
            }
        }
    }
    public void CloseShopPanel()
    {
        shopPanel.GetComponent<Animator>().SetBool("Open", false);
        mainPanel.GetComponent<Animator>().SetBool("Open", true);
        currentCoinsTxt.text = "" + saveRef.coinsCollected;
    }


    public void SetBackground(UIId backgroundRef)
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            if (backgroundRef.myId == i)
            {
                if (unlockedBackgrounds[backgroundRef.myId])
                {
                    saveRef.skyboxSet = i;
                    saveLoadRef.Save();
                    tick2Ref.SetParent(backgroundRef.transform);
                    tick2Ref.anchoredPosition = tickStartRect;
                    return;
                }
                else
                {
                    PurchaseBackground(backgroundRef.myId);
                    purchaseSkin = false;
                    purchaseBackground = true;
                }
            }
        }
    }

    public void PurchaseBackground(int backgroundID)
    {
        skinPurchaseText.text = "Background " + backgroundID + " For " + backgroundsPurchasePrice[backgroundID] + " Coins";
        skinPurchasePanel.SetActive(true);
        if (saveRef.coinsCollected >= backgroundsPurchasePrice[backgroundID])
        {
            skinPurchaseMathTxt.color = Color.black;
            skinPurchaseMathTxt.text = "You Have " + saveRef.coinsCollected + " After purchase you will have: " + (saveRef.coinsCollected - backgroundsPurchasePrice[backgroundID]) + " Coins";
            skinConfirmButton.interactable = true;
            backgroundToUnlock = backgroundID;
        }
        else
        {
            skinConfirmButton.interactable = false;
            skinPurchaseMathTxt.color = Color.red;
            skinPurchaseMathTxt.text = "You Dont have enough Coins You need " + (backgroundsPurchasePrice[backgroundID] - saveRef.coinsCollected);
        }
        backgroundPurchaseCost = backgroundsPurchasePrice[backgroundID];
        backgroundToUnlock = backgroundID;
    }

    public void SetBackground1(GameObject parentObj)
    {
        tick2Ref.SetParent(parentObj.transform);
        tick2Ref.anchoredPosition = tickStartRect;
        saveRef.skyboxSet = 0;
        saveLoadRef.Save();
    }
    public void SetBackground2(GameObject parentObj)
    {
        tick2Ref.SetParent(parentObj.transform);
        tick2Ref.anchoredPosition = tickStartRect;
        saveRef.skyboxSet = 1;
        saveLoadRef.Save();
    }
    public void SetBackground3(GameObject parentObj)
    {
        tick2Ref.SetParent(parentObj.transform);
        tick2Ref.anchoredPosition = tickStartRect;
        saveRef.skyboxSet = 2;
        saveLoadRef.Save();
    }

    public void SetSelectedSkin(UIId skinRef)
    {
        for(int i = 0; i<skins.Count; i++)
        {
            if(skinRef.myId == i)
            {
                if (unlockedSkins[skinRef.myId])
                {
                    saveRef.skinSelected = i;
                    saveLoadRef.Save();
                    tickRef.SetParent(skinRef.transform);
                    tickRef.anchoredPosition = tickStartRect;
                    return;
                }
                else
                {
                    PurchaseSkin(skinRef.myId);
                    purchaseSkin = true;
                    purchaseBackground = false;
                }
            }
        }
    }

    public void PurchaseSkin(int skinId)
    {
        skinPurchaseText.text = "Skin " + skinId + " For " + skinPurchasePrice[skinId] + " Coins";
        skinPurchasePanel.SetActive(true);
        if (saveRef.coinsCollected >= skinPurchasePrice[skinId])
        {
            skinPurchaseMathTxt.color = Color.black;
            skinPurchaseMathTxt.text = "You Have " + saveRef.coinsCollected + " After purchase you will have: " + (saveRef.coinsCollected - skinPurchasePrice[skinId]) + " Coins";
            skinConfirmButton.interactable = true;
            skinToUnlock = skinId;
        }
        else
        {
            skinConfirmButton.interactable = false;
            skinPurchaseMathTxt.color = Color.red;
            skinPurchaseMathTxt.text = "You Dont have enough Coins You need " + (skinPurchasePrice[skinId] - saveRef.coinsCollected);
        }
        skinpurchaseCost = skinPurchasePrice[skinId];
        skinToUnlock = skinId;
    }

    public void BackgroundShopConfirm()
    {
        saveRef.coinsCollected -= backgroundPurchaseCost;
        skinPurchasePanel.SetActive(false);
        unlockedBackgrounds[backgroundToUnlock] = true;
        saveRef.unlockedBackgrounds[backgroundToUnlock] = true;
        saveLoadRef.Save();
    }
    public void SkinShopConfirm()
    {
        if (purchaseSkin)
        {
            saveRef.coinsCollected -= skinpurchaseCost;
            skinPurchasePanel.SetActive(false);
            unlockedSkins[skinToUnlock] = true;
            saveRef.unlockedSkins[skinToUnlock] = true;
            skinsPadlocks[skinToUnlock].SetActive(false);
            saveLoadRef.Save();
        }
        if(purchaseBackground)
        {
            saveRef.coinsCollected -= backgroundPurchaseCost;
            skinPurchasePanel.SetActive(false);
            unlockedBackgrounds[backgroundToUnlock] = true;
            saveRef.unlockedBackgrounds[backgroundToUnlock] = true;
            backgroundsPadlock[backgroundToUnlock].SetActive(false);
            saveLoadRef.Save();
        }
    }
    public void SkinShopCancel()
    {
        skinpurchaseCost = 0;
        skinPurchasePanel.SetActive(false);
    }

    //----------------------------------------------------------------Settings panel-------------------------------------------------------

    public void OpenSettingPanel()
    {
        settingsPanel.GetComponent<Animator>().SetBool("Started", true);
        settingsPanel.GetComponent<Animator>().SetBool("Open", true);
        mainPanel.GetComponent<Animator>().SetBool("Started", true);
        mainPanel.GetComponent<Animator>().SetBool("Open", false);
        editSettings = true;
		tutorialToggle.isOn = !saveRef.tutorialDone;
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.GetComponent<Animator>().SetBool("Open", false);
        mainPanel.GetComponent<Animator>().SetBool("Open", true);
        editSettings = false;
        saveLoadRef.Save();
    }

	public void ToggleTutorial()
	{
		tutorialToggle.isOn = !tutorialToggle.isOn;
		saveRef.tutorialDone = !tutorialToggle.isOn;
	}


	//=================================================================Coin shop=========================================================
    public void OpenCoinShop()
    {
        coinShopPanel.GetComponent<Animator>().SetBool("Started", true);
        coinShopPanel.GetComponent<Animator>().SetBool("Open", true);
        mainPanel.GetComponent<Animator>().SetBool("Started", true);
        mainPanel.GetComponent<Animator>().SetBool("Open", false);
        buyTier1CoinTxt.text = "Buy 5,000 Coin Pack For " + purchaserRef.GetProductPrice("5,000 Coins Pack (Pipe Runner)");
        buyTier2CoinTxt.text = "Buy 12,000 Coin Pack For " + purchaserRef.GetProductPrice("12,000 Coins Pack (Pipe Runner)");
        buyTier3CoinTxt.text = "Buy 30,000 Coin Pack For " + purchaserRef.GetProductPrice("30,000 Coins Pack (Pipe Runner)");
        buyTier4CoinTxt.text = "Buy 65,000 Coin Pack For " + purchaserRef.GetProductPrice("65,000 Coins Pack (Pipe Runner)");
        buyTier5CoinTxt.text = "Buy 140,000 Coin Pack For " + purchaserRef.GetProductPrice("140,000 Coins Pack (Pipe Runner)");
    }

    public void CloseCoinShop()
    {
        mainPanel.GetComponent<Animator>().SetBool("Open", true);
        coinShopPanel.GetComponent<Animator>().SetBool("Open", false);
        saveLoadRef.Save();
        totalCoinsTxt.text = "" + saveRef.coinsCollected;
        bestDistanceTxt.text = "" + (int)saveRef.bestDistance;
    }

    public void PurchaseCompleted (string purchase)
    {
        purchaseCompletedTxt.text = purchase;
        coinpurchaseConfirmPanel.SetActive(true);
    }

    public void PurchaseCompletedOkConfirmation()
    {
        coinpurchaseConfirmPanel.SetActive(false);
    }

    public void ToggleMagnetPullInfoPanel()
    {
        MagnetPullInfoPanel.SetActive(!MagnetPullInfoPanel.activeSelf);
    }

    public void ToggleMagnetDurationInfoPanel()
    {
        magnetDurationInfoPanel.SetActive(!magnetDurationInfoPanel.activeSelf);
    }

    public void ToggleSpeedBoostDurationPanel()
    {
        speedBoostDurationInfoPanel.SetActive(!speedBoostDurationInfoPanel.activeSelf);
    }

    public void ToggleInvincibilityDurationPanel()
    {
        invincDurationInfoPanel.SetActive(!invincDurationInfoPanel.activeSelf);
    }

    public void ToggleSilverCoinSpawnChancePanel()
    {
        sCoinSpawnInfoPanel.SetActive(!sCoinSpawnInfoPanel.activeSelf);
    }
    public void ToggleSilverCoinTresholdPanel()
    {
        sCoinTresholdInfoPanel.SetActive(!sCoinTresholdInfoPanel.activeSelf);
    }

    public void ToggleSilverCoinMaxSpawnPanel()
    {
        sCoinMaxSpawnInfoPanel.SetActive(!sCoinMaxSpawnInfoPanel.activeSelf);
    }

    public void ToggleGoldCoinSpawnChancePanel()
    {
        gCoinSpawnInfoPanel.SetActive(!gCoinSpawnInfoPanel.activeSelf);
    }

    public void ToggleGoldCoinTresholdPanel()
    {
        gCoinTresholdInfoPanel.SetActive(!gCoinTresholdInfoPanel.activeSelf);
    }

    public void ToggleGoldCoinMaxSpawnChancePanel()
    {
        gCoinMaxSpawnInfoPanel.SetActive(!gCoinMaxSpawnInfoPanel.activeSelf);
    }

    
}
