using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameManager gm;
    Inventory inventory;

    public Text txtSelected;
    public Text txtPriceSelected;

    GameManager.plants selectedPlant;
    int selectedPrice;

    bool isBuying;

    private void Start()
    {
        gm = GameManager.gm;
        inventory = Inventory.inventory;
    }

    public void SelectPlant(int _plantIndex)
    {
        selectedPlant = (GameManager.plants)_plantIndex;
        txtSelected.text = "Selected " + (isBuying ? "Seed: " : "Plant: ") + selectedPlant.ToString();
        PlantsSO selectedPlantData = gm.plantData[_plantIndex];
        selectedPrice = isBuying ? selectedPlantData.seedCost : selectedPlantData.sellPrice;
        txtPriceSelected.text = "Price: " + selectedPrice;
    }

    public void SetIsBuying(bool _isBuying)
    {
        isBuying = _isBuying;
    }

    public void BuySeed()
    {
        inventory.AdjustMoney(-selectedPrice);
        inventory.AdjustSeedQuantity(selectedPlant, 1);
    }

    public void SellPlant()
    {
        inventory.AdjustMoney(selectedPrice);
        inventory.AdjustPlantQuantity(selectedPlant, -1);
        PlantsSO plantData = gm.plantData[(int)selectedPlant];
        PlayerController.pc.AdjustSanity(plantData.sanity);
    }
}
