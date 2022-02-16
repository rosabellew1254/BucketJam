using UnityEngine.UI;
using UnityEngine;
using System;

public class Journal : MonoBehaviour
{
    public string[] Journalpages;
    public string[] JournalPagesNormal;
    public string[] JournalPagesSmallEvil;
    public string[] JournalpagesLargeEvil;
    [Space]
    [Space]
    public Sprite[] jSketch_Norm;
    public Sprite[] jSketch_Low;
    public Sprite[] jSketch_High;
    public Sprite[] jSketch_Day20;
    [Space]
    public Sprite[] jSketch_SisDead;
    [Space]
    [Space]
    public int currentpage = 0;
    public Button TurnLeftPage;
    public Button TurnRightPage;
    public static GameManager.state[] stateHistory;

    public Sprite[] bookState;
    public Image sketch;
    public Image journal;
    public Text entry;
    public TextAsset textAssetData;

    GameManager gm;

    public void Start()
    {
        gm = GameManager.gm;
        CurBookState();

        JournalPagesNormal = new string[gm.maxDays];
        JournalPagesSmallEvil = new string[gm.maxDays];
        JournalpagesLargeEvil = new string[gm.maxDays];
        ReadCSV();

        DisplayPage(gm.curDay);
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int numColumns = 5;
        int tableSize = data.Length / numColumns - 1;

        for (int i = 0; i < tableSize; i++)
        {
            Debug.Log(i);
            JournalPagesNormal[i] = TextFixer(data[numColumns * (i + 1)]);
            JournalPagesSmallEvil[i] = TextFixer(data[numColumns * (i + 1) + 1]);
            JournalpagesLargeEvil[i] = TextFixer(data[numColumns * (i + 1) + 2]);
        }
    }

    string TextFixer(string _string)
    {
        string returnString = _string;

        while (returnString.Contains("`"))
        {
            returnString = returnString.Replace('`', ',');
        }

        while (returnString.Contains("�"))
        {
            int errIndex = returnString.IndexOf('�');

            if (errIndex == 0)
            {
                Debug.LogError("string in excel starts with period or apostrophy! Which one is it?");
                return returnString;
            }

            string leftPart = returnString.Substring(0, errIndex);
            string rightPart = returnString.Substring(errIndex);

            if (rightPart.Length == 1)
            {
                rightPart = ".";
            }
            else if (rightPart[1] == ' ')
            {
                rightPart = "." + rightPart.Substring(1);
            }
            else
            {
                rightPart = "'" + rightPart.Substring(1);
            }

            returnString = leftPart + rightPart;
        }

        return returnString;
    }

    public void CurBookState()
    {
        if (GameManager.gm.worldState == GameManager.state.largeEvil)
        {
            journal.sprite = bookState[1];
        }
        else
        {
            journal.sprite = bookState[0];
        }
    }


    public void SetDay20(GameManager.state _state)
    {
        switch (gm.mySister)
        {
            case GameManager.sisterStatus.cured:
                gm.journalSketch[currentpage] = jSketch_Low[0];
                break;
            case GameManager.sisterStatus.dead:
                gm.journalSketch[currentpage] = jSketch_Low[1];
                break;
            default:
                break;
        }
    }

    public void DisplayPage(int _page)
    {
        currentpage = _page;
        GameManager.state state = stateHistory[_page];
        switch (state)
        {
            case GameManager.state.smallEvil:
                entry.text = JournalPagesSmallEvil[_page];
                gm.journalSketch[_page] = jSketch_Low[_page];
                if (gm.mySister == GameManager.sisterStatus.dead)
                {
                    Debug.Log("SIS DEAD");
                    if (jSketch_SisDead[_page] != null)
                    {
                        gm.journalSketch[_page] = jSketch_SisDead[_page];
                    }
                }
                break;
            case GameManager.state.largeEvil:
                entry.text = JournalpagesLargeEvil[_page]; 
                gm.journalSketch[_page] = jSketch_High[_page];
                if (gm.mySister == GameManager.sisterStatus.dead)
                {
                    Debug.Log("SIS DEAD");
                    if (jSketch_SisDead[_page] != null)
                    {
                        gm.journalSketch[_page] = jSketch_SisDead[_page];
                    }
                }
                break;
            default:
                entry.text = JournalPagesNormal[_page];
                gm.journalSketch[_page] = jSketch_Norm[_page];
                if (gm.mySister == GameManager.sisterStatus.dead)
                {
                    Debug.Log("SIS DEAD");
                    if (jSketch_SisDead[_page] != null)
                    {
                        gm.journalSketch[_page] = jSketch_SisDead[_page];
                    }
                }
                break;
        }

        sketch.sprite = gm.journalSketch[currentpage];

        TurnLeftPage.gameObject.SetActive(currentpage > 0);
        TurnRightPage.gameObject.SetActive(currentpage < gm.curDay);

        Debug.Log("Day: " + _page);
        Debug.Log("Sanity: " + state);
        Debug.Log("Journal Entry Index: " + _page);
    }

    public void PageFlip(int _change)
    {
        DisplayPage(Mathf.Clamp(currentpage + _change, 0, GameManager.gm.curDay));
    }

    public void JournalClose()
    {
        Destroy(gameObject);
    }
}
