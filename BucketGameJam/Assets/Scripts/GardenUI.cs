﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenUI : MonoBehaviour
{
    GameManager gm;
    Inventory inventory;
    Garden garden;

    public Transform[] plantingSpots;
    public Text[] growthProgress;
    GameObject[] plantInSoil;
    
    GameManager.plants selectedSeed;
    public GameObject[] availableSeeds;
    public GameObject[] dropdownDisplays;
    public GameObject dropdownList;
    bool isHarvestMode;
    public Image harvestButton;
    bool clickedDropDown;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gm;
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitUntil(() => Inventory.inventory != null && Inventory.inventory.seeds != null && Inventory.inventory.seeds.Length > 0);
        inventory = Inventory.inventory;
        garden = Garden.garden;
        plantInSoil = new GameObject[garden.numHoles];
        UpdateDropdown();

        for (int i = 0; i < plantingSpots.Length; i++)
        {
            if (garden.plants[i] != GameManager.plants.terminator)
            {
                SpawnPlant(garden.plants[i], i, garden.IsGrowing(i), garden.IsFullyGrown(i));
            }
        }
    }

    void UpdateDropdown()
    {
        for (int i = 0; i < inventory.seeds.Length; i++)
        {
            availableSeeds[i].SetActive(inventory.seeds[i] > 0);
            dropdownDisplays[i].SetActive(i == (int)selectedSeed);
        }

        //if no seed is selected then show default select seed button
        if (selectedSeed == GameManager.plants.terminator)
        {
            dropdownDisplays[(int)GameManager.plants.terminator].SetActive(true);
            return;
        }

        //if seed is selected and you still have seeds of that type, then we're good
        if (inventory.seeds[(int)selectedSeed] > 0)
        {
            return;
        }

        //a seed is selected but we are out of that seed so select another type
        GameManager.plants firstAvailable = GameManager.plants.terminator;
        for (int i = 0; i < inventory.seeds.Length; i++)
        {
            if (inventory.seeds[i] > 0)
            {
                firstAvailable = (GameManager.plants)i;
                break;
            }
        }

        selectedSeed = firstAvailable;
        UpdateDropdown();
    }

    public void ClickDropdown()
    {
        dropdownList.SetActive(dropdownList.activeSelf == false);
        clickedDropDown = true;
    }

    public void SelectSeed(int _seedIndex)
    {
        selectedSeed = (GameManager.plants)_seedIndex;
        UpdateDropdown();
    }

    public void PlantInteract(int _holeIndex)
    {
        GameManager.plants plantInHole = garden.plants[_holeIndex];
        if (isHarvestMode) //destroy unwanted plants
        {
            //do nothing if there is no plant to destroy
            if (plantInHole == GameManager.plants.terminator)
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
        else if (plantInHole == GameManager.plants.terminator && inventory.seeds[(int)selectedSeed] > 0) //plant seeds
        {
            garden.PlantPlant(selectedSeed, _holeIndex);
            inventory.AdjustSeedQuantity(selectedSeed, -1);
            SpawnPlant(selectedSeed, _holeIndex, false, false);
            UpdateDropdown();
        }
        else if (plantInHole != GameManager.plants.terminator && garden.IsFullyGrown(_holeIndex)) //harvest fully grown plants
        {
            inventory.AdjustPlantQuantity(plantInHole, 1);
            DestroyPlant(_holeIndex);
        }
    }

    void DestroyPlant(int _holeIndex)
    {
        Destroy(plantingSpots[_holeIndex].GetComponentInChildren<Plant>().gameObject);
        garden.RemovePlant(_holeIndex);
        growthProgress[_holeIndex].gameObject.SetActive(false);
    }

    void SpawnPlant(GameManager.plants _type, int _holeIndex, bool _isGrowing, bool _isGrown) 
    {
        PlantsSO plantData = gm.plantData[(int)_type];
        GameObject goPlant = Instantiate(gm.plantPrefabs[(int)_type], plantingSpots[_holeIndex]);
        plantInSoil[_holeIndex] = goPlant;
        if (_isGrowing)
        {
            goPlant.GetComponent<Image>().sprite = goPlant.GetComponent<Plant>().sPhase[1];
        }
        else if (_isGrown)
        {
            goPlant.GetComponent<Image>().sprite = goPlant.GetComponent<Plant>().sPhase[2];
        }
        else
        {
            goPlant.GetComponent<Image>().sprite = goPlant.GetComponent<Plant>().sPhase[0];
        }

        growthProgress[_holeIndex].gameObject.SetActive(true);
        growthProgress[_holeIndex].text = Mathf.Min(garden.curGrowth[_holeIndex], plantData.turnsToGrow) + "/" + plantData.turnsToGrow;

    }

    public void ToggleRemoveSeedMode()
    {
        isHarvestMode = !isHarvestMode;
        harvestButton.color = Color.Lerp(isHarvestMode ? Color.red : Color.grey, Color.white, .5f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && dropdownList.activeSelf && clickedDropDown == false)
        {
            Invoke("ClickDropdown", Time.deltaTime);
        }
        clickedDropDown = false;
    }
}
