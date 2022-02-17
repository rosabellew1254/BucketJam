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


        //endings: 
        //  - 0: peaceful
        //  - 1: double-faced
        //  - 2: deserted
        //  - 3: evil
        switch (GameManager.gm.mySister)
        {
            case GameManager.sisterStatus.sick:
                Debug.Log("sister can't remain sick until the end");
                break;
            case GameManager.sisterStatus.cured:
                // have enough money to reach extended goal and the sister is alive
                if (Inventory.inventory.money >= GameManager.gm.competeMoneyGoal)
                {
                    //sanity < 0
                    if (GameManager.gm.worldState == GameManager.state.largeEvil)
                    {
                        // trigger evil ending
                        currentEnding = 3;
                    }
                    //sanity >= 0
                    else
                    {
                        // trigger double-faced ending
                        currentEnding = 1;
                    }
                }
                // not enough money to reach extended goal and sister is alive
                else
                {
                    //sanity < 0
                    if (GameManager.gm.worldState == GameManager.state.largeEvil)
                    {
                        // trigger deserted ending
                        currentEnding = 2;
                    }
                    //sanity >= 0
                    else
                    {
                        // trigger peaceful ending
                        currentEnding = 0;
                    }
                }
                break;
            case GameManager.sisterStatus.dead:
                // enough money to reach extended goal and sister is dead
                if (Inventory.inventory.money >= GameManager.gm.competeMoneyGoal)
                {
                    //sanity < 0
                    if (GameManager.gm.worldState == GameManager.state.largeEvil)
                    {
                        // trigger evil ending
                        currentEnding = 3;
                    }
                    //sanity >= 0
                    else
                    {
                        // trigger deserted ending
                        currentEnding = 2;
                    }
                }
                // not enough money to reach extended goal and sister is dead
                else
                {
                    // trigger deserted ending no matter what sanity level the player is in
                    currentEnding = 2;
                }
                break;
            default:
                break;
        }
        /*if (GameManager.gm.isSiblingAlive == true && Inventory.inventory.money < GameManager.gm.competeMoneyGoal)
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
        }*/

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
        GameManager.gm.intWorldState = (int)GameManager.gm.worldState;
        GameManager.gm.curDay = 0;
        Inventory.inventory.money = 200;
        GameManager.gm.date.text = "Date: " + GameManager.gm.curDay + "/36";
        Inventory.inventory.seeds = new int[(int)GameManager.plants.terminator];
        Inventory.inventory.plants = new int[(int)GameManager.plants.terminator];
        PlayerPrefs.SetInt("san", 25);
        //StartCoroutine("pcSetup");

        GameManager.gm.LoadScene(0);
        //GameManager.gm.RestartGameGM();
    }
}
