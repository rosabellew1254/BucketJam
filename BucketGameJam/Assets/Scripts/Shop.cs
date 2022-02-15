using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameManager gm;
    Inventory inventory;

    public Text txtSelected;
    public Text txtPriceSelected;
    public Button buttonSellPlants;
    public Button buttonBuySeeds;
    public string[] plantNames;

    GameManager.plants selectedPlant;
    int selectedPrice;

    bool isBuying;
    public GameObject sellPlantsMenu;

    public Image backgroundImage;
    public Sprite eldritchBackgroundSprite;
    public Sprite normalBackgroundSprite;
    public Image shopkeeperImage;
    public Sprite eldritchShopkeeperSprite;
    public Sprite normalShopkeeperSprite;

    public Sprite eldritchSellCropsSprite;
    public Sprite normalSellCropsSprite;
    public Sprite eldritchBuySeedsSprite;
    public Sprite normalBuySeedsSprite;
    public Image goToTownButtonImage;
    public Sprite eldritchGeneralButton;
    public Sprite normalGoToTownButton;




    private void Start()
    {
        gm = GameManager.gm;
        inventory = Inventory.inventory;
        ShowSellPlantsButton();
        selectedPlant = GameManager.plants.terminator;
        ShowBuySeedsButton();
    }

    public void SelectPlant(int _plantIndex)
    {
        
        selectedPlant = (GameManager.plants)_plantIndex;
        txtSelected.text = "Selected " + (isBuying ? "Seed: " : "Plant: ") + plantNames[(int)selectedPlant];
        PlantsSO selectedPlantData = gm.plantData[_plantIndex];
        selectedPrice = isBuying ? selectedPlantData.seedCost : selectedPlantData.sellPrice;
        txtPriceSelected.text = "Price: " + selectedPrice;
        ShowBuySeedsButton();
    }

    public void SetIsBuying(bool _isBuying)
    {
        isBuying = _isBuying;
    }

    public void BuySeed()
    {
        inventory.AdjustMoney(-selectedPrice);
        inventory.AdjustSeedQuantity(selectedPlant, 1);
        ShowBuySeedsButton();
    }

    public void SellPlant()
    {
        Instantiate(sellPlantsMenu);
    }

    public void ShowSellPlantsButton()
    {
        int totalPlants = 0;

        for (int i = 0; i < (int)GameManager.plants.terminator; i++)
        {
            totalPlants += inventory.plants[i];
        }

        buttonSellPlants.gameObject.SetActive(totalPlants > 0);
    }

    void ShowBuySeedsButton()
    {
        buttonBuySeeds.gameObject.SetActive(selectedPlant != GameManager.plants.terminator && inventory.money >= selectedPrice);
    }

    public void UpdateUiForSanity()
    {
        Image sellPlants = buttonSellPlants.GetComponent<Image>();
        Image buySeeds = buttonBuySeeds.GetComponent<Image>();
        bool isEvil = PlayerController.pc.curSanity < 0;

        if (isEvil)
        {
            backgroundImage.sprite = eldritchBackgroundSprite;
            shopkeeperImage.sprite = eldritchShopkeeperSprite;
            sellPlants.sprite = eldritchSellCropsSprite;
            buySeeds.sprite = eldritchBuySeedsSprite;
            goToTownButtonImage.sprite = eldritchGeneralButton;
        }
        else
        {
            backgroundImage.sprite = normalBackgroundSprite;
            shopkeeperImage.sprite = normalShopkeeperSprite;
            sellPlants.sprite = normalSellCropsSprite;
            buySeeds.sprite = normalBuySeedsSprite;
            goToTownButtonImage.sprite = normalGoToTownButton;
        }

        goToTownButtonImage.transform.GetChild(0).gameObject.SetActive(isEvil);
    }
}
