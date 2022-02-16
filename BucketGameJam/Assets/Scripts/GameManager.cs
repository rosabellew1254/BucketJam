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
    public enum sisterStatus { sick, cured, dead}
    public enum months { month0, month1, month2, month3, month4, month5, month6, month7, month8, month9, month10, month11, month12, month13, month14, month15, month16, month17, month18, month19, month20, month21, month22, month23, month24, month25, month26, month27, month28, month29, month30, month31, month32, month33, month34, month35, month36}

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
    public JournalEntriesSO[] journalData;
    public GameObject mirrorReflection;
    public GameObject sliderMask;
    public GameObject sliderFill;
    public Text tSanity;
    public Text date;
    public Text daySummaryDayNum;
    public Text daySummaryMoneyGained;
    public Text daySummarySanityGained;
    public Text daySummaryTownStatus;
    public state worldState;

    public bool isSiblingAlive = true;
    public sisterStatus mySister;
    public int curDay;
    public int initDay = 0;
    public int maxDays = 36;
    public int moneyRequiredToSaveSibling;
    public int competeMoneyGoal;
    public int initialMoney;
    public int initialSanity;
    public int sanityThreshold;
    public state tempWorldState;

    public float alphaHitMinValue;
    Action action;
    public Sprite[] journalSketch;
    public bool isNewGame;

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
        curDay = PlayerPrefs.GetInt("turn");
        worldState = (state)PlayerPrefs.GetInt("stateHistory" + curDay, 0);

        tempWorldState = gm.worldState;
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
        if (curDay == 20)
        {
            if (Inventory.inventory.money >= moneyRequiredToSaveSibling)
            {
                Inventory.inventory.AdjustMoney(-moneyRequiredToSaveSibling);

                mySister = sisterStatus.cured;
                Debug.Log("You have spent your money to save your sibling from illness");
            }
            else
            {
                mySister = sisterStatus.dead;
                Debug.Log("Your sibling has died");
            }
            UpdatePlayerPrefs("sisterStatus", (int)mySister, (int)sisterStatus.sick);
            ChangeSisterButton();
        }
        /*else if (PlayerController.pc.curSanity < 0 && curDay > 20)
        {
            isSiblingAlive = false;
            HideSister();
            Debug.Log("Your sibling has died");
        }*/

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
            UpdatePlayerPrefs("stateHistory" + curDay, (int)worldState, (int)worldState);
            Journal.stateHistory[curDay] = worldState;
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
            UpdatePlayerPrefs("sisterStatus", (int)mySister, (int)sisterStatus.sick);
        }
    }

    void ChangeSisterButton()
    {
        Bedroom Broom = FindObjectOfType<Bedroom>().GetComponent<Bedroom>();
        Broom.UpdateSisterButton();
    }

    void ShowDaySummary()
    {
        daySummaryDayNum.text = "Month " + (curDay - 1);
        
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
        UpdatePlayerPrefs("sisterStatus", (int)mySister, (int)sisterStatus.sick);
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
        isSiblingAlive = true;
        UpdatePlayerPrefs("money", initialMoney, initialMoney);
        UpdatePlayerPrefs("san", initialSanity, initialSanity);
        UpdatePlayerPrefs("turn", initDay, initDay);
        Array.Clear(journalSketch, 0, journalSketch.Length);
        for (int i = 0; i < maxDays + 1; i++)
        {
            // resets the world state to normal for all days
            UpdatePlayerPrefs("stateHistory" + i, (int)state.normal, (int)state.normal);
        }
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
        UpdatePlayerPrefs("sisterStatus", (int)sisterStatus.sick, (int)sisterStatus.sick);
        
        SetupGameValues();
        PlayerController.pc.AdjustSanity(0);
        date.text = "Date: " + initDay + "/36";
        isNewGame = true;
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

    public void SetupGameValues() // write PlayerPrefs values to the game variables
    {
        Inventory.inventory.money = PlayerPrefs.GetInt("money");
        Inventory.inventory.dayStartMoney = Inventory.inventory.money;
        Inventory.inventory.txtMoney.text = Inventory.inventory.money.ToString();
        PlayerController.pc.curSanity = PlayerPrefs.GetInt("san");
        PlayerController.pc.dayStartSanity = PlayerController.pc.curSanity;
        tSanity.text = PlayerController.pc.curSanity.ToString();

        curDay = PlayerPrefs.GetInt("turn");
        Journal.stateHistory = new state[gm.maxDays + 1];
        for (int i = 0; i < maxDays + 1; i++)
        {
            Journal.stateHistory[i] = (state)PlayerPrefs.GetInt("stateHistory" + i, 0);
        }
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
        mySister = (sisterStatus)PlayerPrefs.GetInt("sisterStatus");
    }

    public void UpdateButtonSprite(Button _button, Sprite _normalUnselected, Sprite _normalHighlighted, Sprite _eldritchUnselected, Sprite _eldritchHighlighted)
    {
        SpriteState bState = new SpriteState();
        switch (worldState)
        {
            case state.normal:
            case state.smallEvil:
                _button.image.sprite = _normalUnselected;
                _button.transition = Selectable.Transition.SpriteSwap;
                bState.highlightedSprite = _normalHighlighted;
                _button.spriteState = bState;
                break;
            case state.largeEvil:
                _button.image.sprite = _eldritchUnselected;
                _button.transition = Selectable.Transition.SpriteSwap;
                bState.highlightedSprite = _eldritchHighlighted;
                _button.spriteState = bState;
                break;
            case state.terminator:
                break;
            default:
                break;
        }
    }
}
