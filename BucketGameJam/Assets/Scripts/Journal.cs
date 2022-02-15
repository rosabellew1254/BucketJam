using UnityEngine.UI;
using UnityEngine;
using System;

public class Journal : MonoBehaviour
{
    public string[] Journalpages;
    string[] JournalPagesNormal;
    string[] JournalPagesSmallEvil;
    string[] JournalpagesLargeEvil;
    [Space]
    [Space]
    public Sprite[] jSketch_Norm;
    public Sprite[] jSketch_Low;
    public Sprite[] jSketch_High;
    [Space]
    public Sprite[] jSketch_Case1;
    public Sprite[] jSketch_Case2;
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


    public void SetPicture(int _day)
    {
        //grab from array of pictures
        //depending on the state of the world, pick from according array
        // If sister dies or lives draw from another table for specific days
        //
        //draw from norm
        //  if state diff - change to another array (maybe leave spots as empty in other arrays?)
        //  if sister dead or alive change array
        //  if nothing in any array, default to normal



        switch (gm.worldState)
        {
            case GameManager.state.normal:
                gm.journalSketch[_day] = jSketch_Norm[_day];
                break;
            case GameManager.state.smallEvil:
                gm.journalSketch[_day] = jSketch_Low[_day];
                if (jSketch_Low[_day] == null)
                {
                    gm.journalSketch[_day] = jSketch_Norm[_day];
                }
                break;
            case GameManager.state.largeEvil:
                gm.journalSketch[_day] = jSketch_High[_day];
                if (jSketch_High[_day] == null)
                {
                    gm.journalSketch[_day] = jSketch_Norm[_day];
                }
                break;
            default:
                break;
        }

        switch (gm.isSiblingAlive)
        {
            case true:
                gm.journalSketch[_day] = jSketch_Norm[_day];
                break;
            case false:
                gm.journalSketch[_day] = jSketch_Case2[_day];
                break;
        }
    }

    public void DisplayPage(int _page)
    {
        currentpage = _page;
        GameManager.state state = stateHistory[_page];
        //int stateCount = 0;
        if (gm.journalSketch[currentpage] == null)
        {
            SetPicture(currentpage);
        }
        sketch.sprite = gm.journalSketch[currentpage];
        /*for (int i = 0; i < _page; i++)
        {
            if (stateHistory[i] == state)
            {
                stateCount++;
            }
        }*/

        switch (state)
        {
            case GameManager.state.smallEvil:
                entry.text = JournalPagesSmallEvil[_page];
                break;
            case GameManager.state.largeEvil:
                entry.text = JournalpagesLargeEvil[_page];
                break;
            default:
                entry.text = JournalPagesNormal[_page];
                break;
        }

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
