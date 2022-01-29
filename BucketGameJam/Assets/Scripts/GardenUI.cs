using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenUI : MonoBehaviour
{
    GameManager gm;
    Inventory inventory;

    public Dropdown seedSelectDropdown;
    public Transform[] plantingSpots;
    
    GameManager.seeds selectedSeed;
    List<GameManager.seeds> dropDownValues;
    bool isHarvestMode;
    Image harvestButton;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gm;
        seedSelectDropdown.options = new List<Dropdown.OptionData>();
        dropDownValues = new List<GameManager.seeds>();
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitUntil(() => Inventory.inventory != null && Inventory.inventory.seeds != null && Inventory.inventory.seeds.Length > 0);
        inventory = Inventory.inventory;
        UpdateDropdown();
    }

    void UpdateDropdown()
    {
        for (int i = 0; i < inventory.seeds.Length; i++)
        {
            GameManager.seeds currentSeed = (GameManager.seeds)i;
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
    }

    public void SelectSeed()
    {
        selectedSeed = dropDownValues[seedSelectDropdown.value];
        seedSelectDropdown.captionText.text = selectedSeed.ToString();
    }

    public void PlantSeed(int _holeIndex)
    {
        if (inventory.seeds[(int)selectedSeed] > 0)
        {
            inventory.AdjustSeedQuantity(selectedSeed, -1);
            Instantiate(gm.sproutPrefab, plantingSpots[_holeIndex]);
            UpdateDropdown();
        }
    }

    public void Harvest()
    {
        isHarvestMode = !isHarvestMode;
        harvestButton.color = Color.Lerp(isHarvestMode ? Color.red : Color.grey, Color.white, .5f);
    }
}
