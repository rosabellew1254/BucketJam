using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public string[] Journalpages;
    public int currentpage = 0;
    public void Start()
    {
        displayPage(currentpage);
    }
    public void displayPage(int page)
    {
        gameObject.transform.Find("Pages").GetChild(0).GetComponent<Text>().text = Journalpages[page];
    }
    public void LeftPageClick()
    {
        currentpage = Mathf.Clamp(currentpage - 1, 0, Journalpages.Length);
        displayPage(currentpage);
    }
    public void RightPageClick()
    {
        currentpage = Mathf.Clamp(currentpage + 1, 0, Journalpages.Length);
        displayPage(currentpage);
    }

    public void JournalClose()
    {
        Object.Destroy(gameObject);
    }
}
