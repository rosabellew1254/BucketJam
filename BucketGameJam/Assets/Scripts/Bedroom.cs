using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bedroom : MonoBehaviour
{
    public GameObject mirrorReflection;
    public GameObject sisterMenu;
    public GameObject bedMonster;
    public GameObject eldritchFog;
    //public GameObject[] sky;
    //public GameObject[] eldritch;
    public Image skyImage;
    public Sprite[] skyState;
    public Image bushImage;
    public Sprite[] bushStates;
    public Image windowImage;
    public Sprite[] windowStates;
    public Image furnitureImage;
    public Sprite eldritchFurnitureSprite;
    public Image midGround;
    public Sprite eldritchMidGroundSprite;

    public Button[] bedRoomButtons;
    public Sprite[] spriteButtonNormal;
    public Sprite[] spriteButtonEldritch;
    public Image iSisterBed;
    public Sprite[] spriteSisterBed;


    public int randInt;

    GameManager gm;
    PlayerController pc;

    public void Start()
    {
        gm = GameManager.gm;
        pc = PlayerController.pc;

        randomSighting();
        OutsideState();
        for (int i = 0; i < bedRoomButtons.Length; i++)
        {
            bedRoomButtons[i].GetComponent<Image>().alphaHitTestMinimumThreshold = gm.alphaHitMinValue;
        }
        //sister's bed sprite update
        if (gm.mySister == GameManager.sisterStatus.sick)
        {
            switch (gm.worldState)
            {
                case GameManager.state.normal:
                case GameManager.state.smallEvil:
                    iSisterBed.sprite = spriteSisterBed[0];
                    break;
                case GameManager.state.largeEvil:
                    iSisterBed.sprite = spriteSisterBed[1];
                    break;
                case GameManager.state.terminator:
                    break;
                default:
                    break;
            }
        }

        //bed:0, normal: 0, highlight: 1
        gm.UpdateButtonSprite(bedRoomButtons[0], spriteButtonNormal[0], spriteButtonNormal[1], spriteButtonEldritch[0], spriteButtonEldritch[1]);

        //door:1, normal: 2, highlight: 3
        gm.UpdateButtonSprite(bedRoomButtons[1], spriteButtonNormal[2], spriteButtonNormal[3], spriteButtonEldritch[2], spriteButtonEldritch[3]);

        //mirror:2, normal: 4, highlight: 5
        gm.UpdateButtonSprite(bedRoomButtons[2], spriteButtonNormal[4], spriteButtonNormal[5], spriteButtonEldritch[4], spriteButtonEldritch[5]);

        //journal:3, normal: 6, highlight: 7
        gm.UpdateButtonSprite(bedRoomButtons[3], spriteButtonNormal[6], spriteButtonNormal[7], spriteButtonEldritch[6], spriteButtonEldritch[7]);

        // sister(sick:4, cured:5, bear:6) need to be implemented separately
        UpdateSisterButton();


    }

    public void OutsideState()
    {
        switch(gm.worldState)
        {
            case GameManager.state.normal:
                skyImage.sprite = skyState[0];
                windowImage.gameObject.SetActive(false);
                bushImage.sprite = bushStates[0];
                break;
            case GameManager.state.smallEvil:
                randomSighting();
                if (randInt > 2)
                {
                    skyImage.sprite = skyState[0];
                    windowImage.gameObject.SetActive(true);
                    windowImage.sprite = windowStates[0];
                    bushImage.sprite = bushStates[1];
                }
                else
                {
                    skyImage.sprite = skyState[1];
                    windowImage.gameObject.SetActive(false);
                    bushImage.sprite = bushStates[0];
                }
                break;
            case GameManager.state.largeEvil:
                randomSighting();
                bedMonster.SetActive(true);
                eldritchFog.SetActive(true);
                furnitureImage.sprite = eldritchFurnitureSprite;
                midGround.sprite = eldritchMidGroundSprite;
                
                if (randInt == 4)
                {
                    skyImage.sprite = skyState[2];
                    windowImage.gameObject.SetActive(true);
                    windowImage.sprite = windowStates[1];
                    bushImage.sprite = bushStates[2];
                }
                else if (randInt == 3)
                {
                    skyImage.sprite = skyState[3];
                    windowImage.gameObject.SetActive(false);
                    bushImage.sprite = bushStates[3];
                }
                else if (randInt == 2)
                {
                    skyImage.sprite = skyState[4];
                    windowImage.gameObject.SetActive(false);
                    bushImage.sprite = bushStates[3];
                }
                else
                {
                    skyImage.sprite = skyState[2];
                    windowImage.gameObject.SetActive(true);
                    windowImage.sprite = windowStates[2];
                    bushImage.sprite = bushStates[2];
                }
                break;
        }
    }

    public void randomSighting()
    {
        randInt = Random.Range(1, 5);
    }

    public void Mirror()
    {
        //Show reflection of player's current state
        //mirrorReflection = GetComponent<GameManager>
        mirrorReflection = Instantiate(GameManager.gm.mirrorReflection, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    public void Journal()
    {
        Instantiate(gm.pJournal).GetComponent<Journal>();
    }

    public void Advance()
    {
        gm.ConfirmMessage(gm.AdvanceDay, "End the month?");
    }

    public void SisterSpeak()
    {
        //sisterButton = Instantiate(GameManager.gm.sister, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        sisterMenu = Instantiate(GameManager.gm.sister, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        // sister(sick:4, cured:5, bear:6) need to be implemented separately
        GameObject sisterImage = sisterMenu.GetComponent<Sister>().sisterCharacter.gameObject;
        GameObject bearImage = sisterMenu.GetComponent<Sister>().bearCharacter.gameObject;
        sisterImage.gameObject.SetActive(false);
        bearImage.gameObject.SetActive(false);
        switch (gm.mySister)
        {
            case GameManager.sisterStatus.sick:
                sisterImage.SetActive(true);
                sisterImage.GetComponent<Image>().sprite = sisterMenu.GetComponent<Sister>().sisterLook[0];
                break;
            case GameManager.sisterStatus.cured:
                sisterImage.SetActive(true);
                sisterImage.GetComponent<Image>().sprite = sisterMenu.GetComponent<Sister>().sisterLook[1];
                break;
            case GameManager.sisterStatus.dead:
                bearImage.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void UpdateSisterButton()
    {
        for (int i = 0; i < 3; i++)
        {
            bedRoomButtons[4 + i].gameObject.SetActive(false);
        }
        // sister(sick:4, cured:5, bear:6)
        switch (gm.mySister)
        {
            case GameManager.sisterStatus.sick:
                bedRoomButtons[4].gameObject.SetActive(true);
                break;
            case GameManager.sisterStatus.cured:
                bedRoomButtons[5].gameObject.SetActive(true);
                break;
            case GameManager.sisterStatus.dead:
                bedRoomButtons[6].gameObject.SetActive(true);
                break;
            default:
                break;
        }
        //sisterButton.SetActive(false);
    }
}
