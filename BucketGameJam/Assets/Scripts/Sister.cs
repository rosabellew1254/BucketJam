using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sister : MonoBehaviour
{
    public Image textBox;
    public Color[] color;
    public Text textBoxFill;
    public Text textName;
    public Text[] tButtons;
    public GameObject choices;
    public Sprite[] sisterLook;
    public Image sisterCharacter;
    public Image bearCharacter;
    public GameObject panelButton;
    public GameObject panelSubMenuButton;
    public Button[] bPageFlip;
    public Sprite[] spriteDialogBox; // normal, eldritch
    public Sprite[] spriteButtons; // normal, eldritch
    public Sprite[] spritePageFlip; // backNor, NextNor, backEld, NextEld
    bool isReadingSubMenuText;
    public Button[] bDialog;


    [Space]
    [Header("Dialogs")]
    public string[] textMenu;
    public string[] textRoom;
    public string[] textGarden;
    public string[] textTown;
    public string[] textShop;
    public string[] textSister;

    string[] currentArray;


    //create funtions for button dialogs if there is going to be any of that
    //make an array for each set of converstations

    //bool isInDialog;
    int textCount;


    /*
    //Example Text button function
    public void ReadText___()
    {
        textCount = 0;
        textBox.gameObject.SetActive(true);
        textBoxFill.text = ___[textCount];
        currentArray = new string[___.Length];

        for (int i = 0; i < currentArray.Length; i++)
        {
            currentArray[i] = ___[i];
        }
    }
    */

    private void Start()
    {
        //update buttons, page flip, text box and text color base on the world state
        switch (GameManager.gm.worldState)
        {
            case GameManager.state.normal:
            case GameManager.state.smallEvil:
                for (int i = 0; i < bDialog.Length; i++)
                {
                    tButtons[i].color = color[2]; // white
                    bDialog[i].image.sprite = spriteButtons[0];
                }
                bPageFlip[0].image.sprite = spritePageFlip[0];
                bPageFlip[1].image.sprite = spritePageFlip[1];
                textBox.sprite = spriteDialogBox[0];
                textBoxFill.color = color[0]; // brown
                textName.color = color[0]; // brown
                break;
            case GameManager.state.largeEvil: // color all pank
                for (int i = 0; i < bDialog.Length; i++)
                {
                    tButtons[i].color = color[1];
                    bDialog[i].image.sprite = spriteButtons[1];
                }
                bPageFlip[0].image.sprite = spritePageFlip[2];
                bPageFlip[1].image.sprite = spritePageFlip[3];
                textBox.sprite = spriteDialogBox[1];
                textBoxFill.color = color[1];
                textName.color = color[1];
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
        switch (GameManager.gm.mySister)
        {
            case GameManager.sisterStatus.sick:
            case GameManager.sisterStatus.cured:
                textBoxFill.text = textMenu[0]; // sister alive
                break;
            case GameManager.sisterStatus.dead:
                textBoxFill.text = textMenu[1]; // sister dead
                break;
            default:
                break;
        }
        PlayerController.pc.bMainMenu.gameObject.SetActive(false);
        PlayerController.pc.bInventory.gameObject.SetActive(false);
        if (GameManager.gm.mySister == GameManager.sisterStatus.dead)
        {
            textName.text = "Arum";
        }
        else
        {
            textName.text = "Clove";
        }
    }


    public void ReadTextHello()
    {
        string sick0 = "Good morning Arum! You ask how I am feeling? Honestly I’m still not doing very well. Hopefully I can be cured soon, I really want to go outside and play with you!";
        string sick1 = "We need a total of " + GameManager.gm.moneyRequiredToSaveSibling + " Speks and the doctor says we only have "  + (20 - GameManager.gm.curDay) + " months. The medicine costs a lot, but please stay healthy for me.";
        string cured0 = "You still don’t want me to help you plant crops? I feel so much better now after getting the treatment!";
        string cured1 = "I’m excited to move to that city you’ve been telling me about. I wish I could help you earn that " + GameManager.gm.competeMoneyGoal +  " Speks in the last " + (36 - GameManager.gm.curDay) + " days…";
        string dead = "(Memories keep flooding in my head, there is no turning back… The eldritch gods are calling me…)";
        panelButton.gameObject.SetActive(false);
        bPageFlip[1].gameObject.SetActive(true);
        bPageFlip[0].gameObject.SetActive(false);
        textCount = 0;
        //textBox.gameObject.SetActive(true);
        switch (GameManager.gm.mySister)
        {
            case GameManager.sisterStatus.sick:
                currentArray = new string[2];
                currentArray[0] = sick0;
                currentArray[1] = sick1;
                textBoxFill.text = currentArray[textCount];
                break;
            case GameManager.sisterStatus.cured:
                currentArray = new string[2];
                currentArray[0] = cured0;
                currentArray[1] = cured1;
                textBoxFill.text = currentArray[textCount];
                break;
            case GameManager.sisterStatus.dead:
                currentArray = new string[1];
                currentArray[0] = dead;
                textBoxFill.text = currentArray[textCount];
                break;
            default:
                break;
        }

    }

    public void ReadHelp(int _index) // 0: room, 1: garden, 2: town, 3: shop, 4: sister
    {
        isReadingSubMenuText = true;
        panelSubMenuButton.gameObject.SetActive(false);
        bPageFlip[1].gameObject.SetActive(true);
        bPageFlip[0].gameObject.SetActive(false);
        textCount = 0;
        switch (_index)
        {
            case 0:
                textBoxFill.text = textRoom[textCount];
                currentArray = new string[textRoom.Length];

                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = textRoom[i];
                }
                break;
            case 1:
                textBoxFill.text = textGarden[textCount];
                currentArray = new string[textGarden.Length];

                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = textGarden[i];
                }
                break;
            case 2:
                textBoxFill.text = textTown[textCount];
                currentArray = new string[textTown.Length];

                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = textTown[i];
                }
                break;
            case 3:
                textBoxFill.text = textShop[textCount];
                currentArray = new string[textShop.Length];

                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = textShop[i];
                }
                break;
            case 4:
                textBoxFill.text = textSister[textCount];
                currentArray = new string[textSister.Length];

                for (int i = 0; i < currentArray.Length; i++)
                {
                    currentArray[i] = textSister[i];
                }
                break;
            default:
                break;
        }
 
        //textBox.gameObject.SetActive(true);

    }

    public void ReadDialogNext() // 0: menu, 1: submenu
    {
        textCount++;
        if (textCount == currentArray.Length)
        {
            bPageFlip[1].gameObject.SetActive(false);
            bPageFlip[0].gameObject.SetActive(false);
            if (isReadingSubMenuText)
            {
                panelSubMenuButton.gameObject.SetActive(true);
                textBoxFill.text = "";
                isReadingSubMenuText = false;
            }
            else
            {
                textBoxFill.text = textMenu[0]; // sister alive
                panelButton.gameObject.SetActive(true);
            }
        }
        else
        {
            textBoxFill.text = currentArray[textCount];
            bPageFlip[0].gameObject.SetActive(true);
        }

    }

    public void ReadDialogBack()
    {
        textCount--;
        if (textCount == 0)
        {
            bPageFlip[0].gameObject.SetActive(false);
        }
        textBoxFill.text = currentArray[textCount];
    }

    public void GoToSubmenu()
    {
        textBoxFill.text = "";
        bool isPanelActive = panelButton.gameObject.activeSelf;
        panelButton.gameObject.SetActive(!isPanelActive);
        panelSubMenuButton.gameObject.SetActive(isPanelActive);
    }

    public void Back(int _menuNumber) // 0: main menu, 1: sub menu
    {
        switch (_menuNumber)
        {
            case 0:
                PlayerController.pc.bMainMenu.gameObject.SetActive(true);
                PlayerController.pc.bInventory.gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case 1:
                panelSubMenuButton.gameObject.SetActive(false);
                switch (GameManager.gm.mySister)
                {
                    case GameManager.sisterStatus.sick:
                    case GameManager.sisterStatus.cured:
                        textBoxFill.text = textMenu[0]; // sister alive
                        break;
                    case GameManager.sisterStatus.dead:
                        textBoxFill.text = textMenu[1]; // sister dead
                        break;
                    default:
                        break;
                }
                panelButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }


    public void Update()
    {
        if (textBox.gameObject.activeSelf == false) // if no dialog
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back(0);
            }
            if (choices.activeSelf != true)
            {
                choices.SetActive(true);
            }
        }
        else // if dialog
        {
            if (choices.activeSelf != false)
            {
                choices.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                textBox.gameObject.SetActive(false);
            }
        }
        
    }

}
