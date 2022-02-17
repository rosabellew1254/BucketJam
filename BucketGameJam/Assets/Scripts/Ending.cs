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
    public Button[] bPageFlip;
    public Color[] colorText;
    public string[] stringEnding;
    string[] currentArray;
    public Image dialogBox;
    public Sprite[] spriteDialogBox;
    public Sprite[] spritePageFlip; //0: backNormal, 1: nextNormal, 2: backEld, 3: nextEld


    public string[] endName;
    public string[] endFill;
    public string[] endingPeaceful;
    public string[] endingDouble;
    public string[] endingdeserted;
    public string[] endingEvil;
    public Sprite[] endPicture;
    int textCount;


    public int currentEnding = 0;


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

        switch (GameManager.gm.worldState)
        {
            case GameManager.state.normal:
            case GameManager.state.smallEvil:

                bPageFlip[0].image.sprite = spritePageFlip[0];
                bPageFlip[1].image.sprite = spritePageFlip[1];
                dialogBox.sprite = spriteDialogBox[0];
                endingFillerText.color = colorText[0]; // brown
                endNameText.color = colorText[0]; // brown
                break;
            case GameManager.state.largeEvil: // color all pank
                bPageFlip[0].image.sprite = spritePageFlip[2];
                bPageFlip[1].image.sprite = spritePageFlip[3];
                dialogBox.sprite = spriteDialogBox[1];
                endingFillerText.color = colorText[1];
                endNameText.color = colorText[1];
                break;
            case GameManager.state.terminator:
                Debug.Log("not gonna work");
                break;
            default:
                break;
        }
        //page flip buttons are enabled by default
        for (int i = 0; i < bPageFlip.Length; i++)
        {
            bPageFlip[i].gameObject.SetActive(false);
        }
        endNameText.text = endName[currentEnding];
        //endingFillerText.text = endFill[currentEnding];
        backgroundImage.sprite = endPicture[currentEnding];

        GameManager.gm.generalHUD.gameObject.SetActive(false);
        TextInitialization();
        if (currentArray.Length >= 2)
        {
            bPageFlip[1].gameObject.SetActive(true);
        }

    }

    /*public void HideText()
    {
        endingFillerText.gameObject.SetActive(false);
        hideTextButton.gameObject.SetActive(false);
    }*/

    void TextInitialization()
    {
        textCount = 0;
        switch (currentEnding)
        {
            case 0:
                endingFillerText.text = endingPeaceful[0];
                currentArray = new string[endingPeaceful.Length];
                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = endingPeaceful[i];
                }
                break;
            case 1:
                endingFillerText.text = endingDouble[0];
                currentArray = new string[endingDouble.Length];
                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = endingDouble[i];
                }
                break;
            case 2:
                endingFillerText.text = endingdeserted[0];
                currentArray = new string[endingdeserted.Length];
                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = endingdeserted[i];
                }
                break;
            case 3:
                endingFillerText.text = endingEvil[0];
                currentArray = new string[endingEvil.Length];
                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = endingEvil[i];
                }
                break;
            default:
                break;
        }
    }

    public void ReadDialogBack()
    {
        textCount--;
        Debug.Log("textcount: " + textCount);
        if (textCount == 0)
        {
            bPageFlip[0].gameObject.SetActive(false);
        }
        bPageFlip[1].gameObject.SetActive(true);
        endingFillerText.text = currentArray[textCount];
    }
    public void ReadDialogNext() // 0: menu, 1: submenu
    {
        textCount++;
        Debug.Log("textcount: " + textCount);
        if (textCount == currentArray.Length - 1)
        {
            bPageFlip[1].gameObject.SetActive(false);
            endingFillerText.text = currentArray[textCount];
        }
        else
        {
            bPageFlip[0].gameObject.SetActive(true);
            endingFillerText.text = currentArray[textCount];
        }

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
