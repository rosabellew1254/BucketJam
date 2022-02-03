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

    public GameObject sister;
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
    public int curDay = 0;
    public int maxDays = 36;
    public int moneyRequiredToSaveSibling = 5000;
    public int competeMoneyGoal = 12000;
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
        if (PlayerController.pc.curSanity < 20 && PlayerController.pc.curSanity > 0)
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
}
