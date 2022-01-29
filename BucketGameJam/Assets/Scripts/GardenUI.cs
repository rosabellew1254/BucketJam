using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenUI : MonoBehaviour
{
    GameManager gm;
    Inventory inventory;
    Garden garden;

    public Dropdown seedSelectDropdown;
    public Transform[] plantingSpots;
    public Text[] growthProgress;
    
    GameManager.plants selectedSeed;
    List<GameManager.plants> dropDownValues;
    bool isHarvestMode;
    public Image harvestButton;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gm;
        seedSelectDropdown.options = new List<Dropdown.OptionData>();
        dropDownValues = new List<GameManager.plants>();
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitUntil(() => Inventory.inventory != null && Inventory.inventory.seeds != null && Inventory.inventory.seeds.Length > 0);
        inventory = Inventory.inventory;
        garden = Garden.garden;
        UpdateDropdown();

        for (int i = 0; i < plantingSpots.Length; i++)
        {
            if (garden.plants[i] != GameManager.plants.terminator)
            {
                SpawnPlant(garden.plants[i], i);
            }
        }
    }

    void UpdateDropdown()
    {
        for (int i = 0; i < inventory.seeds.Length; i++)
        {
            GameManager.plants currentSeed = (GameManager.plants)i;
            if (inventory.seeds[i] > 0)
            {
                if (dropDownValues.Contains(currentSeed) == false)
                {
                    dropDownValues.Add(currentSeed);
                    Dropdown.OptionData optionData = new Dropdown.OptionData();
                    optionData.text = currentSeed.ToString() + " " + inventory.seeds[i];
                    seedSelectDropdown.options.Add(optionData);
                }
                else
                {
                    int index = dropDownValues.IndexOf(currentSeed);
                    seedSelectDropdown.options[index].text = currentSeed.ToString() + " " + inventory.seeds[i];
                }
            }
            else if (dropDownValues.Contains(currentSeed))
            {
                int index = dropDownValues.IndexOf(currentSeed);
                dropDownValues.Remove(currentSeed);
                seedSelectDropdown.options.RemoveAt(index);
            }
        }

        seedSelectDropdown.captionText.text = inventory.seeds[(int)selectedSeed] > 0 ? selectedSeed.ToString() : "";

        if (dropDownValues.Contains(selectedSeed) == false && dropDownValues.Count > 0)
        {
            SelectSeed(dropDownValues[0]);
        }
    }

    public void SelectSeed()
    {
        SelectSeed(dropDownValues[seedSelectDropdown.value]);
    }

    void SelectSeed(GameManager.plants _type)
    {
        selectedSeed = _type;
        seedSelectDropdown.captionText.text = selectedSeed.ToString();
    }

    public void PlantInteract(int _holeIndex)
    {
        GameManager.plants ChosenPlantType = garden.plants[_holeIndex];
        if (isHarvestMode) //destroy unwanted plants
        {
            //do nothing if there is no plant to destroy
            if (ChosenPlantType == GameManager.plants.terminator)
            {
                return;
            }

            //can't destroy fully grown plant
            if (garden.IsFullyGrown(_holeIndex))
            {
                return;
            }

            DestroyPlant(_holeIndex);
        }
        else if (ChosenPlantType == GameManager.plants.terminator && inventory.seeds[(int)selectedSeed] > 0) //plant seeds
        {
            garden.PlantPlant(selectedSeed, _holeIndex);
            inventory.AdjustSeedQuantity(selectedSeed, -1);
            UpdateDropdown();
            SpawnPlant(selectedSeed, _holeIndex);
        }
        else if (ChosenPlantType != GameManager.plants.terminator && garden.IsFullyGrown(_holeIndex)) //harvest fully grown plants
        {
            inventory.AdjustPlantQuantity(ChosenPlantType, 1);
            DestroyPlant(_holeIndex);
        }
    }

    void DestroyPlant(int _holeIndex)
    {
        Destroy(plantingSpots[_holeIndex].GetComponentInChildren<Plant>().gameObject);
        garden.RemovePlant(_holeIndex);
        growthProgress[_holeIndex].gameObject.SetActive(false);
    }

    void SpawnPlant(GameManager.plants _type, int _holeIndex)
    {
        PlantsSO plantData = gm.plantData[(int)_type];
        Instantiate(gm.plantPrefabs[(int)selectedSeed], plantingSpots[_holeIndex]);
        growthProgress[_holeIndex].gameObject.SetActive(true);
        growthProgress[_holeIndex].text = Mathf.Min(garden.curGrowth[_holeIndex], plantData.turnsToGrow) + "/" + plantData.turnsToGrow;
    }

    public void ToggleRemoveSeedMode()
    {
        isHarvestMode = !isHarvestMode;
        harvestButton.color = Color.Lerp(isHarvestMode ? Color.red : Color.grey, Color.white, .5f);
    }
}
