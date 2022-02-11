using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bedroom : MonoBehaviour
{
    public GameObject mirrorReflection;
    public GameObject sister;
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
    public Button[] bedRoomButtons;
    public Image furnitureImage;
    public Sprite eldritchFurnitureSprite;
    public Image midGround;
    public Sprite eldritchMidGroundSprite;

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
        sister = Instantiate(GameManager.gm.sister, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    public void SetSisterFalse()
    {
        sister.SetActive(false);
    }
}
