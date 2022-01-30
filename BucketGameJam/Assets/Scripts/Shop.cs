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
}
