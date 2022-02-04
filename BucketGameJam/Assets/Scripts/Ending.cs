using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text endNameText;
    public Text endingFillerText;
    public Image backgroundImage;
    public Button hideTextButton;


    public string[] endName;
    public string[] endFill;
    public Sprite[] endPicture;


    int currentEnding = 0;


    private void Start()
    {
        currentEnding = 0;
        //doublefaced = money goal + sibling healed
        //peaceful = sibling alive + full sanatiy 
        //deserted = sanity normal, sibling dies
        //evil = sibling dies, sanity 0

        if (GameManager.gm.isSiblingAlive == true && Inventory.inventory.money < GameManager.gm.competeMoneyGoal)
        {
            //peaceful end
            currentEnding = 0;
        }
        else if (GameManager.gm.isSiblingAlive == true && Inventory.inventory.money >= GameManager.gm.competeMoneyGoal)
        {
            //double end
            currentEnding = 1;
        }
        else if (GameManager.gm.isSiblingAlive == false && Inventory.inventory.money < GameManager.gm.competeMoneyGoal )
        {
            //deserter end
            currentEnding = 2;
        }
        else if (GameManager.gm.isSiblingAlive == false && Inventory.inventory.money >= GameManager.gm.competeMoneyGoal)
        {
            //evil end
            currentEnding = 3;
            Debug.Log(Inventory.inventory.money);
        }
        else
        {
            Debug.Log("Oof");
        }

        endNameText.text = endName[currentEnding];
        endingFillerText.text = endFill[currentEnding];
        backgroundImage.sprite = endPicture[currentEnding];

        GameManager.gm.generalHUD.gameObject.SetActive(false);


    }

    public void HideText()
    {
        endingFillerText.gameObject.SetActive(false);
        hideTextButton.gameObject.SetActive(false);
    }


    public void RestartGame()
    {
        GameManager.gm.worldState = GameManager.state.normal;
        GameManager.gm.curDay = 0;
        Inventory.inventory.money = 200;
        GameManager.gm.date.text = "Date: " + GameManager.gm.curDay + "/36";
        Inventory.inventory.seeds = new int[(int)GameManager.plants.terminator];
        Inventory.inventory.plants = new int[(int)GameManager.plants.terminator];
        PlayerPrefs.SetInt("san", 25);
        StartCoroutine("pcSetup");

        GameManager.gm.LoadScene(0);
        //GameManager.gm.RestartGameGM();
    }
}
