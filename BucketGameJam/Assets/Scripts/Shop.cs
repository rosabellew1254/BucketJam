using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Inventory inventory;

    public Text txtSelected;
    public Text txtPriceSelected;

    GameManager.seeds selectedSeed;
    GameManager.plants selectedPlant;
    int selectedPrice;

    public int[] seedPurchaseCosts;
    public int[] plantSalePrices;

    private void Start()
    {
        inventory = Inventory.inventory;
    }

    public void SelectSeed(int _seedIndex)
    {
        selectedSeed = (GameManager.seeds)_seedIndex;
        txtSelected.text = "Selected Seed: " + selectedSeed.ToString();
        SelectPrice(seedPurchaseCosts[(int)selectedSeed]);
    }

    void SelectPlant(int _plantIndex)
    {
        selectedPlant = (GameManager.plants)_plantIndex;
        txtSelected.text = "Selected Plant: " + selectedPlant.ToString();
        SelectPrice(plantSalePrices[(int)selectedPlant]);
    }

    void SelectPrice(int _price)
    {
        selectedPrice = _price;
        txtPriceSelected.text = "Price: " + selectedPrice;
    }

    public void BuySeed()
    {
        inventory.AdjustMoney(-selectedPrice);
        inventory.AdjustSeedQuantity(selectedSeed, 1);
    }

    public void SellPlant()
    {
        inventory.AdjustMoney(selectedPrice);
        inventory.AdjustPlantQuantity(selectedPlant, -1);
    }

    public void GoToTown()
    {

    }
}
