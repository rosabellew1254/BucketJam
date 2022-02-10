using UnityEngine.UI;
using UnityEngine;
using System;

public class Journal : MonoBehaviour
{
    public string[] Journalpages;
    string[] JournalPagesNormal;
    string[] JournalPagesSmallEvil;
    string[] JournalpagesLargeEvil;
    public int currentpage = 0;
    public Button TurnLeftPage;
    public Button TurnRightPage;
    GameManager.state[] stateHistory;

    public Sprite[] bookState;
    public Image journal;
    public Text entry;
    public TextAsset textAssetData;

    GameManager gm;

    public void Start()
    {
        gm = GameManager.gm;
        
        CurBookState();
        
        //******temporary stand in array until player prefs for state history is set up*********(start)
        stateHistory = new GameManager.state[gm.maxDays];
        for (int i = 0; i < currentpage; i++)
        {
            stateHistory[i] = GameManager.state.normal;
        }
        stateHistory[currentpage] = gm.worldState;
        //******temporary stand in array until player prefs for state history is set up*********(end)

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

    public void DisplayPage(int _page)
    {
        currentpage = _page;
        GameManager.state state = stateHistory[_page];
        int stateCount = 0;
        for (int i = 0; i < _page; i++)
        {
            if (stateHistory[i] == state)
            {
                stateCount++;
            }
        }

        switch (state)
        {
            case GameManager.state.smallEvil:
                entry.text = JournalPagesSmallEvil[stateCount];
                break;
            case GameManager.state.largeEvil:
                entry.text = JournalpagesLargeEvil[stateCount];
                break;
            default:
                entry.text = JournalPagesNormal[stateCount];
                break;
        }

        TurnLeftPage.gameObject.SetActive(currentpage > 0);
        TurnRightPage.gameObject.SetActive(currentpage < gm.curDay);

        Debug.Log("Day: " + _page);
        Debug.Log("Sanity: " + state);
        Debug.Log("Journal Entry Index: " + stateCount);
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
