using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public enum plants { turnip, strawberry, eyePomegranite, mouthApple, terminator}
    public enum scenes { frontEnd, town, garden, shop, bedroom, endScene, terminator }
    public enum state { normal, smallEvil, largeEvil, terminator }


    [Space]
    [Header("Menus")]
    public GameObject pMain;
    public GameObject generalHUD;
    public GameObject pJournal;
    public GameObject inventoryMenu;
    public GameObject confirmMessage;
    public GameObject daySummary;

    public GameObject confirmPrefab;
    public GameObject sister;
    public GameObject newGamePopUpPrefab;
    public GameObject[] plantPrefabs;
    public PlantsSO[] plantData;
    public GameObject mirrorReflection;
    public GameObject sliderMask;
    public GameObject sliderFill;
    public Text date;
    public Text daySummaryDayNum;
    public Text daySummaryMoneyGained;
    public Text daySummarySanityGained;
    public Text daySummaryTownStatus;
    public state worldState;

    public bool isSiblingAlive = true;
    public int curDay;
    public int initDay = 0;
    public int maxDays = 36;
    public int moneyRequiredToSaveSibling;
    public int competeMoneyGoal;
    public int initialMoney;
    public int initialSanity;
    public int sanityThreshold;
    public state tempWorldState;

    Action action;


    private void Awake()
    {
        if (gm != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            gm = this;
        }
        worldState = state.normal;

        tempWorldState = gm.worldState;
        curDay = PlayerPrefs.GetInt("turn");
        date.text = "Date: " + curDay + "/36";
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Whatever scene is loaded, instantiates appropriate menu objects
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            case 0:
                Instantiate(pMain).GetComponent<cMainMenu>();
                generalHUD.gameObject.SetActive(false);
                break;
            default:
                generalHUD.gameObject.SetActive(true);
                break;
        }
    }

    // Method to load the scene
    public void LoadScene(int _idx)
    {
        SceneManager.LoadScene(_idx);
    }

    public void CheckSanity()
    {
        if (PlayerController.pc.curSanity < sanityThreshold && PlayerController.pc.curSanity > 0)
        {
            worldState = state.smallEvil;
            Debug.Log("Set Small");
        }
        else if (PlayerController.pc.curSanity <= -1)
        {
            worldState = state.largeEvil;
            Debug.Log("Set Large");
        }
        else
        {
            worldState = state.normal;
            Debug.Log("Passed Through Check for Sanity");
        }
    }
    
    public void AdvanceDay()
    {
        curDay++;
        date.text = "Date: " + curDay + "/36";
        DayCheck();
    }


    public void OpenInventory()
    {
        Instantiate(inventoryMenu);
    }

    public void DayCheck()
    {
        if (isSiblingAlive == false)
        {
            Debug.Log("Your sibling is dead");
        }
        else if (curDay == 20)
        {
            if (Inventory.inventory.money >= GameManager.gm.moneyRequiredToSaveSibling)
            {
                isSiblingAlive = true;
                Inventory.inventory.money -= gm.moneyRequiredToSaveSibling;
                Debug.Log("You have spent your money to save your sibling from illness");
            }
            else
            {
                isSiblingAlive = false;
                Debug.Log("Your sibling has died");
            }
        }
        else if (PlayerController.pc.curSanity < 0)
        {
            isSiblingAlive = false;
            Debug.Log("Your sibling has died");
        }

        if (curDay == maxDays + 1)
        {
            LoadScene(5);
        }
        else
        {
            Garden.garden.Grow();
            ShowDaySummary();
            UpdatePlayerPrefs("san", PlayerController.pc.curSanity, initialSanity);
            UpdatePlayerPrefs("money", Inventory.inventory.money, initialMoney);
            UpdatePlayerPrefs("turn", curDay, initDay);
            for (int i = 0; i < Garden.garden.numHoles; i++)
            {
                UpdatePlayerPrefs("soil" + i, (int)Garden.garden.plants[i], (int)plants.terminator);
                UpdatePlayerPrefs("soilGrowth" + i, Garden.garden.curGrowth[i], 0);
            }
            for (int i = 0; i < (int)plants.terminator; i++)
            {
                UpdatePlayerPrefs("seed" + i, Inventory.inventory.seeds[i], 0);
                UpdatePlayerPrefs("plant" + i, Inventory.inventory.plants[i], 0);
            }
        }
    }

    void ShowDaySummary()
    {
        daySummaryDayNum.text = "Day " + (curDay - 1);
        
        Inventory inventory = Inventory.inventory;
        daySummaryMoneyGained.text = "Money Gained: " + (inventory.money - inventory.dayStartMoney);
        inventory.dayStartMoney = inventory.money;

        PlayerController pc = PlayerController.pc;
        daySummarySanityGained.text = "Sanity Gained: " + (pc.curSanity - pc.dayStartSanity);
        pc.dayStartSanity = pc.curSanity;

        daySummaryTownStatus.text = "Town Status: " + worldState;

        daySummary.SetActive(true);
    }

    public void DaySummaryContinue()
    {
        daySummary.SetActive(false);
    }

    public void SliderMasking()
    {
        sliderFill.transform.SetParent(sliderMask.transform.parent);
        sliderFill.GetComponent<RectTransform>().sizeDelta = new Vector2(110f, 22f);
        sliderFill.GetComponent<RectTransform>().localPosition = Vector2.zero;
        sliderFill.transform.SetParent(sliderMask.transform);
    }

    public void ConfirmMessage(Action _action, string _message) 
    {
        action = _action;
        confirmMessage.GetComponentInChildren<Text>().text = _message; 
        confirmMessage.SetActive(true);
    }

    public void ConfirmMessageConfirm()
    {
        action();
        confirmMessage.SetActive(false);
    }

    public void ConfirmMessageCancel()
    {
        confirmMessage.SetActive(false);
    }

    public void RestartGameGM()
    {
        worldState = state.normal;
        //↓↓↓↓↓need to set the variables to the current playerprefs values↓↓↓↓↓
        UpdatePlayerPrefs("money", initialMoney, initialMoney);
        UpdatePlayerPrefs("san", initialSanity, initialSanity);
        UpdatePlayerPrefs("turn", initDay, initDay);
        for (int i = 0; i < Garden.garden.numHoles; i++)
        {
            UpdatePlayerPrefs("soil" + i, (int)plants.terminator, (int)plants.terminator);
            UpdatePlayerPrefs("soilGrowth" + i, 0, 0);
        }
        for (int i = 0; i < (int)plants.terminator; i++)
        {
            UpdatePlayerPrefs("seed" + i, 0, 0);
            UpdatePlayerPrefs("plant" + i, 0, 0);
        }
        SetupGameValues();
        PlayerController.pc.AdjustSanity(0);
        date.text = "Date: " + initDay + "/36";
        LoadScene(4);
    }

    public void UpdatePlayerPrefs(string _key, int _curAmt, int _initAmt)
    {
        if (!PlayerPrefs.HasKey(_key))
        {
            PlayerPrefs.SetInt(_key, _initAmt);
        }
        else
        {
            PlayerPrefs.SetInt(_key, _curAmt);
        }
    }

    public void SetupGameValues()
    {
        Inventory.inventory.money = PlayerPrefs.GetInt("money");
        Inventory.inventory.dayStartMoney = Inventory.inventory.money;
        Inventory.inventory.txtMoney.text = Inventory.inventory.money.ToString();
        PlayerController.pc.curSanity = PlayerPrefs.GetInt("san");
        PlayerController.pc.dayStartSanity = PlayerController.pc.curSanity;

        curDay = PlayerPrefs.GetInt("turn");
        for (int i = 0; i < Garden.garden.numHoles; i++)
        {
            Garden.garden.plants[i] = (plants)PlayerPrefs.GetInt("soil" + i);
            Garden.garden.curGrowth[i] = PlayerPrefs.GetInt("soilGrowth" + i);
        }
        for (int i = 0; i < (int)plants.terminator; i++)
        {
            Inventory.inventory.seeds[i] = PlayerPrefs.GetInt("seed" + i);
            Inventory.inventory.plants[i] = PlayerPrefs.GetInt("plant" + i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < (int)plants.terminator; i++)
            {
                Debug.Log("seed info: " + PlayerPrefs.GetInt("seed" + i));
            }
        }
    }
}
