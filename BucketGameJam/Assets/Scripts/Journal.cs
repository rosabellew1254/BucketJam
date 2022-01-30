using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public string[] Journalpages;
    public int currentpage = 0;
    public Button TurnLeftPage;
    public Button TurnRightPage;
    public void Start()
    {
        TurnLeftPage.gameObject.SetActive(false);
        displayPage(currentpage);
    }
    public void displayPage(int page)
    {
        if (Journalpages.Length>0)
        {
            gameObject.transform.Find("Pages").GetChild(0).GetComponent<Text>().text = Journalpages[page];
        }
        if (currentpage > 0)
        {
            TurnLeftPage.gameObject.SetActive(true);
        }
        else
        {
            TurnLeftPage.gameObject.SetActive(false);
        }
        if (currentpage < Journalpages.Length - 1)
        {
            TurnRightPage.gameObject.SetActive(true);
        }
        else
        {
            TurnRightPage.gameObject.SetActive(false);
        }

    }
    public void LeftPageClick()
    {
        
        currentpage = Mathf.Clamp(currentpage - 1, 0, Journalpages.Length - 1);
        displayPage(currentpage);
    }
    public void RightPageClick()
    {
        if (currentpage != GameManager.gm.curDay)
        {
            currentpage = Mathf.Clamp(currentpage + 1, 0, Journalpages.Length - 1);
        }
        displayPage(currentpage);
    }

    public void JournalClose()
    {
        Object.Destroy(gameObject);
    }
}
