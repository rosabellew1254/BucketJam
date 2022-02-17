using System.Collections;
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
    public Button door;
    public Sprite[] spriteDoor;

    public Color[] textColor;


    GameManager.plants selectedSeed;
    public GameObject[] availableSeeds;
    public GameObject[] dropdownDisplays;
    public GameObject dropdownList;
    bool isHarvestMode;
    public Image harvestButton;
    bool clickedDropDown;

    public Sprite[] spriteEldrichAvailableSeeds;
    public Sprite spriteEldrichDropdownList;

    public Sprite[] spriteNormalDropdownDisplaysUp;
    public Sprite[] spriteEldrichDropdownDisplaysUp;
    public Sprite[] spriteNormalDropdownDisplaysDown;
    public Sprite[] spriteEldrichDropdownDisplaysDown;

    public Image background;
    public Sprite eldritchBackground;
    public Image grassMain;
    public Sprite eldritchGrassMain;
    public Image grassBack;
    public Sprite eldritchGrassBack;
    public Image grassFront;
    public Sprite eldritchGrassFront;
    public GameObject fog;
    public Image goToTownButton;
    public Sprite generalEldritchButtonSprite;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gm;
        int numPlants = (int)GameManager.plants.terminator;
        
        if (gm.worldState == GameManager.state.largeEvil)
        {
            background.sprite = eldritchBackground;
            grassMain.sprite = eldritchGrassMain;
            grassBack.sprite = eldritchGrassBack;
            grassFront.sprite = eldritchGrassFront;
            goToTownButton.sprite = generalEldritchButtonSprite;
            goToTownButton.transform.GetChild(0).GetComponent<Text>().color = textColor[1];
            harvestButton.sprite = generalEldritchButtonSprite;
            harvestButton.transform.GetChild(0).GetComponent<Text>().color = textColor[1];
            fog.SetActive(true);
            for (int i = 0; i < numPlants; i++)
            {
                availableSeeds[i].GetComponent<Image>().sprite = spriteEldrichAvailableSeeds[i];
            }
            dropdownDisplays[numPlants].GetComponent<Image>().sprite = spriteEldrichDropdownDisplaysDown[numPlants];
            dropdownList.GetComponent<Image>().sprite = spriteEldrichDropdownList;
        }
        door.GetComponent<Image>().alphaHitTestMinimumThreshold = gm.alphaHitMinValue;
        gm.UpdateButtonSprite(door, spriteDoor[0], spriteDoor[1], spriteDoor[2], spriteDoor[3]);
        StartCoroutine(Setup());
        switch (gm.worldState)
        {
            case GameManager.state.normal:
                StartCoroutine(AudioManager.am.PlayMusic(10));
                break;
            case GameManager.state.smallEvil:
            case GameManager.state.largeEvil:
                StartCoroutine(AudioManager.am.PlayMusic(11));
                break;
            case GameManager.state.terminator:
                break;
            default:
                break;
        }
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
        dropdownDisplays[(int)GameManager.plants.terminator].SetActive(false);

        for (int i = 0; i < inventory.seeds.Length; i++)
        {
            bool isSelected = i == (int)selectedSeed;
            availableSeeds[i].SetActive(inventory.seeds[i] > 0);
            dropdownDisplays[i].SetActive(isSelected);
        }

        UpdateDropdownSprite();

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

    void UpdateDropdownSprite()
    {
        Image image = dropdownDisplays[(int)selectedSeed].GetComponent<Image>();
        if (gm.worldState == GameManager.state.largeEvil)
        {
            if (dropdownList.activeSelf)
            {
                image.sprite = spriteEldrichDropdownDisplaysUp[(int)selectedSeed];
            }
            else
            {
                image.sprite = spriteEldrichDropdownDisplaysDown[(int)selectedSeed];
            }
        }
        else
        {
            if (dropdownList.activeSelf)
            {
                image.sprite = spriteNormalDropdownDisplaysUp[(int)selectedSeed];
            }
            else
            {
                image.sprite = spriteNormalDropdownDisplaysDown[(int)selectedSeed];
            }
        }
    }

    public void ButtonSound()
    {
        AudioManager.am.PlaySFX("event:/click");
    }

    public void DoorSound()
    {
        AudioManager.am.PlaySFX("event:/door");

    }

    public void PlantSound()
    {
        AudioManager.am.PlaySFX("event:/set_plant");
    }

    public void ClickDropdown()
    {
        dropdownList.SetActive(dropdownList.activeSelf == false);
        UpdateDropdownSprite();
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
        else if (plantInHole == GameManager.plants.terminator && selectedSeed != GameManager.plants.terminator && inventory.seeds[(int)selectedSeed] > 0) //plant seeds
        {
            garden.PlantPlant(selectedSeed, _holeIndex);
            inventory.AdjustSeedQuantity(selectedSeed, -1); // reduces seed count
            SpawnPlant(selectedSeed, _holeIndex, false, false);
            UpdateDropdown(); // updates seed count
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
        harvestButton.color = Color.Lerp(Color.red , Color.white, isHarvestMode ? .5f : 1f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && dropdownList.activeSelf && clickedDropDown == false)
        {
            Invoke("ClickDropdown", Time.deltaTime);
        }
        clickedDropDown = false;
    }

    public void StopInstance()
    {
        switch (gm.worldState)
        {
            case GameManager.state.normal:
                AudioManager.am.StopInstance(10);
                break;
            case GameManager.state.smallEvil:
            case GameManager.state.largeEvil:
                AudioManager.am.StopInstance(11);
                break;
            case GameManager.state.terminator:
                break;
            default:
                break;
        }
    }
}
