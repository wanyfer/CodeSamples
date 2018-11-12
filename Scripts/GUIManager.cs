using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RTS;
//using Vexe.FastSave;
//using Vexe.Runtime.Types;
//using Vexe.Runtime.Extensions;

public class GUIManager : MonoBehaviour 
{
	public List<GameObject> saveList, loadList;
	public GameObject saveObject;

	public static GUIManager guiManager;

	public float offsetX, offsetY;
	public string uiState = "Main", uiChange, selectedBuilding, tooltipName, selectedCharacter, marketState, marketTooltip;
	public int moneyCharge;
	public BuildingCost buildingCharge;
	public Slider sliderBar;

	public bool tooltipOn, marketChange;

	//UI buttons and Panels
	public Text txt_sliderTXT, txt_buildingName, tooltip1, tooltip2, tooltip3, tooltip4,  marketWood, marketIron, marketStone, marketGems, marketGrain, workProgress;
	public Text armouryMoney, armouryWorkingPercent, characterName, characterHP, carpenterMoney, characterStatsTxt, characterDetailsTxt;
	public Text armouryMeleeT1Count, armouryMeleeT2Count, armouryMeleeT3Count, armouryRangedT1Count, armouryRangedT2Count, armouryRangedT3Count;
	public Button hireButton, upgradeButton, questButton, armouryHire, characterPanelButton;
	public GameObject tooltip5, buildPanel, mainPanel, buildingSelectedPanel, buildGatheringPanel, buildRefiningPanel, buildCat, tooltipPanel;
	public GameObject heroesPanel, governmentPanel, characterSelPanel, marketPanel, marketMainPanel, marketBuyPanel, marketAutomationPanel, marketInventoryPanel;
	public GameObject armouryPanel, armouryOnPanel, armouryOffPanel, armouryMTier1, armouryMTier2, armouryMTier3, armouryRTier1, armouryRTier2, armouryRTier3;
	public GameObject characterStatsPanel;
	public Button marketBuyIron, marketSellIron, marketBuyWood,marketSellWood, marketBuyStone, marketSellStone, marketBuyGrain, marketSellGrain, marketM1, marketM5, marketM10, marketM50;
	public int marketMultiplier = 1;
	public InputField woodRefiners, ironRefiners, stoneRefiners, grainRefiners, woodSell, ironSell, stoneSell, gemSell;
	public Toggle autoSellRefiners, autoSellMarket;
	public bool canBuild, characterPanelOpen;
	public InputField debugResource;
	public Slider healthBar;
	public Sprite emptyInventory, meleeWT1Sprite, meleeWT2Sprite, meleeWT3Sprite, rangedWT1Sprite, rangedWT2Sprite, rangedWT3Sprite;
	public Image armourIcon, weaponIcon;


	//references
	public MouseHandler mouseRef;
	public GameObject buildingRef, lastBSelected, hireButtonRef, upgradeButtonRef, characterRef, enemyRef;
	MoneyManager moneyRef;
	public MarketHandler mRef;
	public ArmoryHandler aRef;

	//Prices
	public int heroHallPrice, farmPrice, lumberMillPrice, blacksmithPrice, marketPrice, minerGuildPrice, archerGuildPrice, alchemyGuildPrice, innPrice;
	public int quarryPrice, fisheryPrice, labourOfficePrice, masonPrice, governmentBuildingPrice, sheriffPrice, medicalPrice, labourHousePrice, refineryPrice;

	//Building prefabs
	public GameObject  b_HeroHall, b_Casttle, b_LumberMill, b_Blacksmith, b_Farm, b_market, b_MinerGuild, b_Alchemy, b_Archery, b_Bakery, b_Carpenter;
	public GameObject  b_Armory, b_Fishery, b_Government, b_WorkerHouse, b_Inn, b_Masonry, b_Medical, B_Quarry, b_Refinery, b_Sheriff, b_Warrior, b_Witch;

	//building flags
	public bool farmEnabled, lumberMillEnabled, warriorGuildEnabled, marketEnabled, minersGuildEnabled, alchemyLabEnabled, armoryEnabled, fisheryEnabled;
	public bool archeryGuildEnabled, innEnabled, governmentEnabled, workerHouseEnabled, medicalBuildingEnabled, quarryEnabled, sheriffEnabled, witchEnabled;
	public bool marketBuilt = false, marketSetup = false, armouryChange = true;

	// Use this for initialization
	void Start () 
	{
		mouseRef = MouseHandler.mouse;
		guiManager = this;
		moneyRef = MoneyManager.moneyManager;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(tooltipOn == true)
		{
			tooltipPanel.SetActive(true);
			tooltip1.enabled = true;
			BuildingValue();
		}
		if(tooltipOn == false)
		{
			tooltip1.enabled = false;
			tooltipPanel.SetActive(false);
		}


		if(uiChange != uiState)
		{
			if(uiState == "Main")
			{
				mainPanel.SetActive(true);
				heroesPanel.SetActive(false);
				governmentPanel.SetActive(false);
				marketPanel.SetActive(false);
				buildPanel.SetActive(false);
				buildCat.SetActive(false);
				buildingSelectedPanel.SetActive(false);
				buildGatheringPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				//hireButton.onClick.RemoveAllListeners();
				//questButton.onClick.RemoveAllListeners();
				characterSelPanel.SetActive(false);
				armouryPanel.SetActive(false);
				tooltipOn = false;
				armouryOnPanel.SetActive(false);
				characterStatsPanel.SetActive(false);

			}


			if(uiState == "Build")
			{
				marketPanel.SetActive(false);
				buildPanel.SetActive(true);
				buildCat.SetActive(true);
				heroesPanel.SetActive(false);
				governmentPanel.SetActive(false);
				mainPanel.SetActive(false);				
				buildGatheringPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				buildingSelectedPanel.SetActive(false);	
				tooltipOn = false;
				armouryPanel.SetActive(false);
				characterStatsPanel.SetActive(false);
			}
			if(uiState == "Building Selected")
			{		

				buildingSelectedPanel.SetActive(true);
				heroesPanel.SetActive(false);
				governmentPanel.SetActive(false);
				buildPanel.SetActive(false);
				mainPanel.SetActive(false);
				buildGatheringPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				buildCat.SetActive(false);
				characterSelPanel.SetActive(false);
				marketPanel.SetActive(false);
				armouryPanel.SetActive(false);
				characterStatsPanel.SetActive(false);

			}
			if(uiState == "Build Gathering")
			{
				marketPanel.SetActive(false);
				//buildPanel.SetActive(false);
				buildGatheringPanel.SetActive(true);
				buildCat.SetActive(false);
				heroesPanel.SetActive(false);
				governmentPanel.SetActive(false);
				mainPanel.SetActive(false);
				buildingSelectedPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				tooltipOn = false;
			}
			if(uiState == "Build Refining")
			{
				marketPanel.SetActive(false);
				//buildPanel.SetActive(false);
				buildRefiningPanel.SetActive(true);
				buildCat.SetActive(false);
				heroesPanel.SetActive(false);
				governmentPanel.SetActive(false);
				mainPanel.SetActive(false);
				buildingSelectedPanel.SetActive(false);
				buildGatheringPanel.SetActive(false);
				tooltipOn = false;
			}
			if(uiState == "Build Heroes")
			{
				marketPanel.SetActive(false);
				//buildPanel.SetActive(false);
				heroesPanel.SetActive(true);
				buildCat.SetActive(false);
				governmentPanel.SetActive(false);
				mainPanel.SetActive(false);
				buildingSelectedPanel.SetActive(false);
				buildGatheringPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				tooltipOn = false;
			}
			if(uiState == "Build Government")
			{
				marketPanel.SetActive(false);
				//buildPanel.SetActive(false);
				governmentPanel.SetActive(true);
				heroesPanel.SetActive(false);
				buildCat.SetActive(false);
				mainPanel.SetActive(false);
				buildingSelectedPanel.SetActive(false);
				buildGatheringPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				tooltipOn = false;
			}
			if(uiState == "CharacterSelected")
			{

				characterSelPanel.SetActive(true);
				buildPanel.SetActive(false);
				governmentPanel.SetActive(false);
				heroesPanel.SetActive(false);
				buildCat.SetActive(false);
				mainPanel.SetActive(false);
				buildingSelectedPanel.SetActive(false);
				buildGatheringPanel.SetActive(false);
				buildRefiningPanel.SetActive(false);
				tooltipOn = false;
				armouryPanel.SetActive(false);
				marketPanel.SetActive(false);
				armouryOnPanel.SetActive(false);
			}

		}
		uiChange = uiState;

		if(uiState == "CharacterSelected")
		{
			characterPanelButton.enabled = false;
			if(characterRef)
			{
				armourIcon.sprite = emptyInventory;
				string name = "No Name", className = "No Class", level = "1";
				CharacterHealthHandler healthRef = characterRef.GetComponent<CharacterHealthHandler>();
				CharacterStats statsRef = characterRef.GetComponent<CharacterStats>();

				healthBar.maxValue = healthRef.maxHealth;
				healthBar.value = healthRef.currentHealth;

				if(characterRef.tag != "Hero")
				{
					questButton.interactable = true;
			    	questButton.onClick.AddListener(() => { QuestDatabase.questDB.AddQuest(characterRef); });
					characterName.text = "Enemy";
					characterHP.text = "Health: " + characterRef.GetComponent<CharacterHealthHandler>().currentHealth;

			 	}
				else
				{
					weaponIcon.color = new Color(1,1,1,0);
					Color noAlpha = new Color(armourIcon.color.r,armourIcon.color.g, armourIcon.color.b, 1);
					questButton.interactable = false;
					characterHP.text = "Health: " + characterRef.GetComponent<CharacterHealthHandler>().currentHealth;
					//hireButton.enabled = false;
					//upgradeButton.enabled = false;
					if(characterRef.GetComponent<WarriorHandler>())
					{
						weaponIcon.color = new Color(1,1,1,0);
						WarriorHandler warRef = characterRef.GetComponent<WarriorHandler>();
						name = characterRef.GetComponent<WarriorHandler>().myName;
						characterName.text = "Name: " + name;
						className = "Warrior";
						characterPanelButton.enabled = true;
						if(warRef.tier1Weapon)
						{
							weaponIcon.sprite = meleeWT1Sprite;
							weaponIcon.color = noAlpha;
						}
						 
					}
					if(characterRef.GetComponent<RangerHandler>())
					{
						RangerHandler rangerRef = characterRef.GetComponent<RangerHandler>();
						name = characterRef.GetComponent<RangerHandler>().myName;
						characterName.text = "Name: " + name;
						className = "Ranger";
						characterPanelButton.enabled = true;

						if(rangerRef.tier1Weapon)
						{
							weaponIcon.sprite = rangedWT1Sprite;
							weaponIcon.color = noAlpha;
						}

					}


				}


				if(characterStatsPanel.activeSelf)
				{
					characterStatsTxt.text = "Strenght:" + statsRef.strenght + "\nAgility:" + statsRef.agility + "\nStamina:" + statsRef.stamina + "\nIntelect:" + statsRef.intelect + "\nArmour:" + statsRef.armor + "\nMoney:" + characterRef.GetComponent<HeroMoney>().currentMoney;
					characterDetailsTxt.text = "Name: " + name + "\nClass: " + className + "\nLevel: " + level;
				}



			}
			//else uiState = "Main";
		}


		//building selected block

		if(uiState == "Building Selected")
		{
			////////////////////////////////////////////Carpentry Block//////////////////////////////////////
			if(selectedBuilding == "Carpenter")
			{
				hireButton.interactable = true;
				CarpentryHandler carpenterRef = buildingRef.GetComponent<CarpentryHandler>();
				
				if(carpenterRef.buildingOn)
				{
					carpenterMoney.text = "Carpenter Available Money: " + (int)carpenterRef.carpentryMoney;
					hireButton.interactable = false;
					workProgress.enabled = true;
				}
				else
				{
					hireButton.interactable = true;
					workProgress.enabled = false;
				}
				
				if(lastBSelected != buildingRef)
				{
					lastBSelected = buildingRef;
					//hireButtonRef.SetActive(false);
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(carpenterRef.HireCarpenter) ;
				}
				if(carpenterRef.buildBed)
				{
					workProgress.text = "Constructing Bed: " + Mathf.RoundToInt(carpenterRef.workProgress) + "%";
				}
				if(carpenterRef.buildTable)
				{
					workProgress.text = "Constructing Table: " + Mathf.RoundToInt(carpenterRef.workProgress) + "%";
				}
				if(carpenterRef.buildChair)
				{
					workProgress.text = "Constructing Chair: " + Mathf.RoundToInt(carpenterRef.workProgress) + "%";
				}

				if(!carpenterRef.buildBed && !carpenterRef.buildTable && !carpenterRef.buildChair)
				{
					workProgress.text = "No money for raw resources";
				}


			}
			else carpenterMoney.text = "";








			//////////////Armoury block////////////////////////////////////
			if(selectedBuilding == "Armoury")
			{

				ArmoryHandler armRef = buildingRef.GetComponent<ArmoryHandler>();

				if(lastBSelected != buildingRef)
				{
					armouryChange = true;
					lastBSelected = buildingRef;
					txt_buildingName.text = buildingRef.tag;
					armouryHire.onClick.RemoveAllListeners();
					buildingSelectedPanel.SetActive(false);
					armouryPanel.SetActive(true);

				}

				armouryMoney.text = "Available Money: " + armRef.armouryMoney;

				if(armRef.working)
				{
					armouryWorkingPercent.text = "Working: " + (int)armRef.workProgress;
				}
				else armouryWorkingPercent.text = "";



				if(armouryChange)
				{
					buildingSelectedPanel.SetActive(false);
					armouryChange = false;
					if(armRef.buildingOn)
					{
						buildingSelectedPanel.SetActive(false);
						armouryPanel.SetActive(false);
						armouryOnPanel.SetActive(true);
						armouryOffPanel.SetActive(false);
						if(armRef.meleeTier1)
						{
							armouryMTier1.SetActive(true);
							armouryMeleeT1Count.text = armRef.tier1MCount.ToString();
						}
						else armouryMTier1.SetActive(false);
						if(armRef.meleeTier2)
						{
							armouryMTier2.SetActive(true);
						}
						else armouryMTier2.SetActive(false);
						if(armRef.meleeTier3)
						{
							armouryMTier3.SetActive(true);
						}
						else armouryMTier3.SetActive(false);

						if(armRef.rangedTier1)
						{
							armouryRTier1.SetActive(true);
							armouryRangedT1Count.text = armRef.tier1RCount.ToString();
						}
						else armouryRTier1.SetActive(false);
						if(armRef.rangedTier2)
						{
							armouryRTier2.SetActive(true);
						}
						else armouryRTier2.SetActive(false);
						if(armRef.rangedTier3)
						{
							armouryRTier3.SetActive(true);
						}
						else armouryRTier3.SetActive(false);

					}
					else
					{
						armouryPanel.SetActive(true);
						armouryOnPanel.SetActive(false);
						armouryOffPanel.SetActive(true);
						armouryHire.onClick.AddListener(armRef.HireSmith) ;
						buildingSelectedPanel.SetActive(false);
					}
				}

			}
			else armouryOnPanel.SetActive(false);


			if(selectedBuilding == "Farmhouse")
			{
				hireButton.interactable = true;
				FarmhouseHandler farmRef = buildingRef.GetComponent<FarmhouseHandler>();

				if(farmRef.buildingOn)
				{
					hireButton.interactable = false;
					workProgress.enabled = true;
				}
				else
				{
					hireButton.interactable = true;
					workProgress.enabled = false;
				}

				if(lastBSelected != buildingRef)
				{
					debugResource.text = farmRef.grainAmmount.ToString();
					lastBSelected = buildingRef;
					//hireButtonRef.SetActive(false);
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(farmRef.HireWorker) ;
				}
				farmRef.grainAmmount = int.Parse(debugResource.text);                           //DEBUG STUFF
				workProgress.text = "Growing Crops: " + (int)farmRef.progressGrain + "%";

			}

			if(selectedBuilding == "Mine")
			{
				hireButton.interactable = true;
				MineHandler mineRef = buildingRef.GetComponent<MineHandler>();

				if(mineRef.buildingOn)
				{
					hireButton.interactable = false;
					workProgress.enabled = true;
				}
				else 
				{
					hireButton.interactable = true;
					workProgress.enabled = false;
				}

				if(lastBSelected != buildingRef)
				{
					debugResource.text = mineRef.ironAmmount.ToString();
					lastBSelected = buildingRef;
					//hireButtonRef.SetActive(false);
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(mineRef.HireMiner) ;
				}
				mineRef.ironAmmount = int.Parse(debugResource.text);  

				if(mineRef.mining)
				{
					workProgress.text = "Mining: " + (int)mineRef.progressMining + "%";
				}
				if(mineRef.processingOre)
				{
					workProgress.text = "Smelting: " + (int)mineRef.progressSmelt + "%";
				}
			}

			if(selectedBuilding == "Quarry")
			{
				QuarryHandler quarryRef = buildingRef.GetComponent<QuarryHandler>();
				if(lastBSelected != buildingRef)
				{
					debugResource.text = quarryRef.stoneAmmount.ToString();
					lastBSelected = buildingRef;
					//hireButtonRef.SetActive(false);
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(quarryRef.HireWorker) ;
				}
				quarryRef.stoneAmmount = int.Parse(debugResource.text); 

				if(quarryRef.buildingOn)
				{
					hireButton.interactable = false;
					workProgress.enabled = true;
				}
				else 
				{
					hireButton.interactable = true;
					workProgress.enabled = false;
				}

				workProgress.text = "Cutting Stone: " + (int)quarryRef.progressStone + "%";

			}

			if(selectedBuilding == "Lumber Mill")
			{
				LumberMillHandler lMRef = buildingRef.GetComponent<LumberMillHandler>();
				if(lMRef.buildingOn)
				{
					hireButton.interactable = false;
					workProgress.enabled = true;
				}
				else 
				{
					hireButton.interactable = true;
					workProgress.enabled = false;
				}
				workProgress.text = "Processing Wood: " + (int)lMRef.processingPercent + "%";


				if(moneyRef.currentMoney > lMRef.upgradeCost)
				{
					upgradeButton.interactable = true;
					upgradeButton.onClick.RemoveAllListeners();
					upgradeButton.onClick.AddListener(lMRef.UpgradeBuilding);
				}
				else upgradeButton.interactable = false;

				if(lastBSelected != buildingRef)
				{
					debugResource.text = lMRef.woodAmmount.ToString();
					lastBSelected = buildingRef;
					hireButton.enabled = true;
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(lMRef.HireLumberjack) ;
				}
				lMRef.woodAmmount = int.Parse(debugResource.text); 
			}
			//else workProgress.text = "";

			if(selectedBuilding == "Warrior Guild")
			{
				hireButton.interactable = true;

				if(lastBSelected != buildingRef)
				{
					workProgress.enabled = false;
					lastBSelected = buildingRef;
		    		txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(buildingRef.GetComponent<HeroHallHandler>().CreateWarrior) ;
				}
			}

			if(selectedBuilding == "Archery Guild")
			{
				hireButton.interactable = true;

				if(lastBSelected != buildingRef)
				{
					
					workProgress.enabled = false;
					lastBSelected = buildingRef;
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();
					hireButton.onClick.AddListener(buildingRef.GetComponent<HeroHallHandler>().CreateRanger) ;
				}
			}

			////////////////////////Market block/////////////////////////////////
			if(selectedBuilding == "Market")
			{
				MarketHandler marketRef = buildingRef.GetComponent<MarketHandler>();

				if(lastBSelected != buildingRef)
				{
					mRef = buildingRef.GetComponent<MarketHandler>();
					marketState = "Main";
					marketChange = true;
					lastBSelected = buildingRef;
					txt_buildingName.text = buildingRef.tag;
					hireButton.onClick.RemoveAllListeners();

				}
				if(marketState == "Main" && marketChange)
				{
					marketChange = false;
					marketMainPanel.SetActive(true);
					marketPanel.SetActive(true);
					marketBuyPanel.SetActive(false);
					buildingSelectedPanel.SetActive(false);
					marketAutomationPanel.SetActive(false);
					marketInventoryPanel.SetActive(false);
					return;
				}
				if(marketState == "Buy")
				{
					if(marketChange)
					{
						marketBuyPanel.SetActive(true);
						marketChange = false;
						marketMainPanel.SetActive(false);
						marketAutomationPanel.SetActive(false);
						marketInventoryPanel.SetActive(false);
						if(!marketSetup)
						{

							marketSetup = true;
							marketM1.interactable = false;
							marketM1.onClick.AddListener(marketRef.marketMultiplierX1);
							marketM5.onClick.AddListener(marketRef.marketMultiplierX5);
							marketM10.onClick.AddListener(marketRef.marketMultiplierX10);
							marketM50.onClick.AddListener(marketRef.marketMultiplierX50);
							marketBuyIron.onClick.AddListener(marketRef.BuyIron);
							marketSellIron.onClick.AddListener(marketRef.SellIron);
							marketBuyWood.onClick.AddListener(marketRef.BuyWood);
							marketSellWood.onClick.AddListener(marketRef.SellWood);
							marketBuyStone.onClick.AddListener(marketRef.BuyStone);
							marketSellStone.onClick.AddListener(marketRef.SellStone);
							marketBuyGrain.onClick.AddListener(marketRef.BuyGrain);
							marketSellGrain.onClick.AddListener(marketRef.SellGrain);
						}


					}
					if(tooltipOn)
					{
						marketRef.AddTooltip(marketTooltip);
					}
					return;
				}
				if(marketState == "Automate")
				{
					if(marketChange)
					{
						marketAutomationPanel.SetActive(true);
						marketChange = false;
						marketMainPanel.SetActive(false);
						marketBuyPanel.SetActive(false);
						marketInventoryPanel.SetActive(false);
						return;
					}
				}


				if(marketState == "Inventory" && marketChange)
				{
					marketInventoryPanel.SetActive(true);
					marketChange = false;
					marketMainPanel.SetActive(false);
					marketBuyPanel.SetActive(false);
					marketAutomationPanel.SetActive(false);

				}
				if(marketState == "Inventory")
				{
					marketIron.text = "Iron: " + moneyRef.currentIron;
					marketWood.text = "Wood: " + moneyRef.currentWood;
					marketStone.text = "Stone: " + moneyRef.currentStone;
					marketGems.text = "Gems: " + moneyRef.currentGems;
				}
				//////////////////////////////////////////////////////////////////////////////////////////////////

			}
			else 
			{
				if(marketState != "")
				{
					//marketMainPanel.SetActive(false);
					marketPanel.SetActive(false);
					marketBuyPanel.SetActive(false);
			    	marketState = "";
					marketInventoryPanel.SetActive(false);
					marketAutomationPanel.SetActive(false);
					//buildingSelectedPanel.SetActive(true);
				}
			}
		}

		///////////////////////////////////////building selected block end//////////////////////////////////////////////////////////////////////////////

	}





	//debug
	public void ReloadLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
		//Application.LoadLevel(Application.loadedLevel);
	}
	//debug end

//	public void SliderValue()
//	{
//		txt_sliderTXT.text = sliderBar.value.ToString(); //block not in use
//	}

	//market button section
	public void MarketBuy()
	{
		marketChange = true;
		marketState = "Buy";
	}
	public void MarketAutomate()
	{
		marketChange = true;
		marketState = "Automate";
	}
	public void MarketInventory()
	{
		marketChange = true;
		marketState = "Inventory";
	}



	// build buttons section

	public void BuildButton()
	{
		uiState = "Build";
		mainPanel.SetActive(false);
	}
	
	public void BuildingGathering()
	{
		uiState = "Build Gathering";
	}

	public void BuildRefining()
	{
		uiState = "Build Refining";
	}
	public void BuildHeroes()
	{
		uiState = "Build Heroes";
	}
	public void BuildGovernment()
	{
		uiState = "Build Government";
	}


	//debug/testing
	public void SpawnEnemy()
	{
		buildingCharge = new BuildingCost();
		uiState = "Building";
		mouseRef.BuildingRef = enemyRef;
		mouseRef.Build();
		MouseHandler.mouse.building = true;
	}
	public void Add1000Gold()
	{
		moneyRef.currentMoney += 1000;
	}

	//Build buttons action
	public void BuildFarmHouse()
	{
		if(MoneyManager.moneyManager.availableResources > b_Farm.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Farm;
			mouseRef.Build();
			buildingCharge = b_Farm.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}

	}

	public void BuildCarpenter()
	{
		if(MoneyManager.moneyManager.availableResources > b_Carpenter.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Carpenter;
			mouseRef.Build();
			buildingCharge = b_Carpenter.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
		
	}

	public void BuildLumberMill ()
	{
		if(MoneyManager.moneyManager.availableResources > b_LumberMill.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_LumberMill;
			mouseRef.Build();
			buildingCharge = b_LumberMill.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildHeroHall ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Warrior.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Warrior;
			mouseRef.Build();
			buildingCharge = b_Warrior.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}



	public void BuildMinersGuild ()
	{
		if(MoneyManager.moneyManager.availableResources > b_MinerGuild.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_MinerGuild;
			mouseRef.Build();
			buildingCharge = b_MinerGuild.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildAlchemy ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Alchemy.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Alchemy;
			mouseRef.Build();
			buildingCharge = b_Alchemy.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}


	public void BuildArchery ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Archery.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Archery;
			mouseRef.Build();
			buildingCharge = b_Archery.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildArmory ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Armory.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Armory;
			mouseRef.Build();
			buildingCharge = b_Armory.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildFishery ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Fishery.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Fishery;
			mouseRef.Build();
			buildingCharge = b_Fishery.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildGovernmentBuilding ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Government.GetComponent<BuildingCost>())
		{
	    	tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Government;
			mouseRef.Build();
			buildingCharge = b_Government.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildWorkerHouse ()
	{
		if(MoneyManager.moneyManager.availableResources > b_WorkerHouse.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_WorkerHouse;
			mouseRef.Build();
			buildingCharge = b_WorkerHouse.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void Buildinn ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Inn.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Inn;
			mouseRef.Build();
			buildingCharge = b_Inn.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildMarket ()
	{
		if(MoneyManager.moneyManager.availableResources > b_market.GetComponent<BuildingCost>() && !marketBuilt)		
		{
			marketBuilt = true;
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_market;
			mouseRef.Build();
			buildingCharge = b_market.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;

		}
	}

	public void BuildMasonry ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Masonry.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Masonry;
			mouseRef.Build();
			buildingCharge = b_Masonry.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildMedical ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Medical.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Medical;
			mouseRef.Build();
	    	buildingCharge = b_Medical.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildSheriff ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Sheriff.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Sheriff;
			mouseRef.Build();
			buildingCharge = b_Sheriff.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildRefinery ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Refinery.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Refinery;
			mouseRef.Build();
			buildingCharge = b_Refinery.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildBakery ()
	{
		if(MoneyManager.moneyManager.availableResources > b_Bakery.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = b_Bakery;
			mouseRef.Build();
			buildingCharge = b_Bakery.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	public void BuildQuarry ()
	{
		if(MoneyManager.moneyManager.availableResources > B_Quarry.GetComponent<BuildingCost>())
		{
			tooltipOn = false;
			uiState = "Building";
			buildPanel.SetActive(false);
			mouseRef.BuildingRef = B_Quarry;
			mouseRef.Build();
			buildingCharge = B_Quarry.GetComponent<BuildingCost>();
			MouseHandler.mouse.building = true;
		}
	}

	//setting the tootip on or off
	public void tooltipEnter (string buttonName)
	{
		tooltipOn = true;
		tooltipName = buttonName;
	}
	public void tooltipExit ()
	{
		tooltipOn = false;
	}

	//tootip Values
	public void BuildingValue()
	{
		tooltip1.enabled = false;
		tooltip2.enabled = false;
		tooltip3.enabled = false;
		tooltip4.enabled = false;
		tooltip5.SetActive(false);

		if (tooltipName == "FarmHouse")
		{
			BuildingCostTooltip(b_Farm);
			return;
//			tooltip1.enabled = true;
//			tooltip2.enabled = true;
//			tooltip3.enabled = true;
//			tooltip4.enabled = true;
//
//			if(MoneyManager.moneyManager.availableResources > b_Farm.GetComponent<BuildingCost>())
//			{
//				tooltip5.SetActive(false);
//			}
//			else tooltip5.SetActive(true);
//
//			BuildingCost bPrice = b_Farm.GetComponent<BuildingCost>();
//
//			tooltip1.text = "Gold price: " + bPrice.moneyCost;
//			tooltip2.text = "Wood Cost: " + bPrice.woodCost;
//			tooltip3.text = "Stone Cost: " + bPrice.stoneCost;
//			tooltip4.text = "Iron Cost: " + bPrice.ironCost;
//			return;
		}
		if (tooltipName == "LumberMill")
		{
			BuildingCostTooltip(b_LumberMill);
			return;
		}
		if (tooltipName == "WarriorGuild")
		{
			BuildingCostTooltip(b_Warrior);
			return;
		}
		if (tooltipName == "Bakery")
		{
			BuildingCostTooltip(b_Bakery);
			return;
		}
		if (tooltipName == "Carpenter")
		{
			BuildingCostTooltip(b_Carpenter);
			return;
		}
		if (tooltipName == "MinersGuild")
		{
			BuildingCostTooltip(b_MinerGuild);
			return;
		}
		if (tooltipName == "ArcherGuild")
		{
			BuildingCostTooltip(b_Archery);
			return;
		}
		if (tooltipName == "Refinery")
		{BuildingCostTooltip(b_Refinery);
			return;
		}
		if (tooltipName == "Alchemy Lab")
		{
			BuildingCostTooltip(b_Alchemy);
			return;
		}
		if (tooltipName == "Armory")
		{
			BuildingCostTooltip(b_Armory);
			return;
		}
		if (tooltipName == "Fishery")
		{
			BuildingCostTooltip(b_Fishery);
			return;
		}
		if (tooltipName == "GovermentBuilding")
		{
			BuildingCostTooltip(b_Government);
			return;
		}
		if (tooltipName == "House")
		{
			BuildingCostTooltip(b_WorkerHouse);
			return;
		}
		if (tooltipName == "Inn")
		{
			BuildingCostTooltip(b_Inn);
			return;
		}
		if (tooltipName == "Masonry")
		{
			BuildingCostTooltip(b_Masonry);
			return;
		}
		if (tooltipName == "Medical")
		{
			BuildingCostTooltip(b_Medical);
			return;
		}
		if (tooltipName == "Sherif")
		{
			BuildingCostTooltip(b_Sheriff);
			return;
		}

		if (tooltipName == "Quarry")
		{
			BuildingCostTooltip(B_Quarry);
			return;
		}


		if (tooltipName == "Market" && !marketBuilt)
		{			
			BuildingCostTooltip(b_market);
			return;
		}
		if (tooltipName == "Market" && marketBuilt)
		{
			tooltip1.enabled = true;
			tooltip2.text = "Only 1 allowed";
			tooltip2.enabled = true;
			tooltip1.text =  "Canot Build";
		}
		else tooltip2.enabled = false;
		if(tooltipName == "BuyWood")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "SellWood")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "BuyIron")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "SellIron")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "BuyStone")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "SellStone")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "BuyGrain")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;
		}
		if(tooltipName == "SellGrain")
		{
			marketTooltip = tooltipName;
			tooltip2.enabled = true;

		}


		//tooltip1.text =  "nothing";
	}

	public void SaveGame()
	{
		SaveHandler.SaveGame("wantuhy");
	}
	public void LoadGame()
	{
        //LoadManager.LoadGame("wantuhy", SceneManager.GetActiveScene().name);
        SaveManager.saveManager.loading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public bool pause = false;
	public void PauseGame()
	{
		pause = !pause;

		if(pause)
		{
			Time.timeScale = 0;
		}
		else Time.timeScale = 1;
	}


	public void BuildingCostTooltip (GameObject objectRef)
	{
			tooltip1.enabled = true;
			tooltip2.enabled = true;
			tooltip3.enabled = true;
			tooltip4.enabled = true;
        
			
			if(moneyRef.availableResources > objectRef.GetComponent<BuildingCost>())
			{
				tooltip5.SetActive(false);
			}
			else tooltip5.SetActive(true);
			
			BuildingCost bPrice = objectRef.GetComponent<BuildingCost>();
			
			tooltip1.text = "Gold price: " + bPrice.moneyCost;
			tooltip2.text = "Wood Cost: " + bPrice.woodCost;
			tooltip3.text = "Stone Cost: " + bPrice.stoneCost;
			tooltip4.text = "Iron Cost: " + bPrice.ironCost;
			return;
	}

	public void OpenCharacterPanel()
	{
		characterStatsPanel.SetActive(true);
	}
}
