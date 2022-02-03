using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bedroom : MonoBehaviour
{
    public GameObject mirrorReflection;
    public GameObject sister;
    public GameObject[] sky;
    public GameObject[] eldritch;
    public Image bushImage;
    public Sprite[] bushStates;
    public Image window;
    public Sprite[] windowStates;

    public int randInt;

    GameManager gm;
    PlayerController pc;

    public void Start()
    {
        gm = GameManager.gm;
        pc = PlayerController.pc;

        randomSighting();
        OutsideState();
    }

    public void OutsideState()
    {
        if(pc.curSanity < 0)
        {
            sky[0].gameObject.SetActive(false);
            sky[1].gameObject.SetActive(true);
            if (randInt == 4)
            {
                eldritch[0].gameObject.SetActive(true);
                eldritch[1].gameObject.SetActive(false);
                window.gameObject.SetActive(true);
                window.sprite = windowStates[1];
            }
            else if (randInt == 3)
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(true);
                window.gameObject.SetActive(false);
            }
            else if (randInt == 2)
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(false);
                window.gameObject.SetActive(true);
                window.sprite = windowStates[1];
            }
            else
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(true);
                window.gameObject.SetActive(true);
                window.sprite = windowStates[2];
            }
            eldritch[2].gameObject.SetActive(false);
            bushImage.sprite = bushStates[1];
        }
        else if(pc.curSanity < 20 && pc.curSanity > -1)
        {
            sky[0].gameObject.SetActive(true);
            sky[1].gameObject.SetActive(false);
            if (randInt == 4)
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(false);
                eldritch[2].gameObject.SetActive(true);
                window.gameObject.SetActive(false);
                bushImage.sprite = bushStates[1];
            }
            else if (randInt == 3)
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(false);
                eldritch[2].gameObject.SetActive(false);
                window.gameObject.SetActive(false);
                bushImage.sprite = bushStates[1];
            }
            else if (randInt == 2)
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(false);
                eldritch[2].gameObject.SetActive(false);
                window.gameObject.SetActive(true);
                window.sprite = windowStates[0];
                bushImage.sprite = bushStates[0];
            }
            else
            {
                eldritch[0].gameObject.SetActive(false);
                eldritch[1].gameObject.SetActive(false);
                eldritch[2].gameObject.SetActive(false);
                window.gameObject.SetActive(false);
                bushImage.sprite = bushStates[0];
            }
        }
        else
        {
            sky[0].gameObject.SetActive(true);
            sky[1].gameObject.SetActive(false);
            eldritch[0].gameObject.SetActive(false);
            eldritch[1].gameObject.SetActive(false);
            eldritch[2].gameObject.SetActive(false);
            bushImage.sprite = bushStates[0];
            window.gameObject.SetActive(false);
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
        //Advance the month
        GameManager.gm.AdvanceDay();

        if (GameManager.gm.curDay == GameManager.gm.maxDays +1)
        {
            GameManager.gm.LoadScene(5);
        }

        Garden.garden.Grow();
    }

    public void SisterSpeak()
    {
        sister = Instantiate(GameManager.gm.mirrorReflection, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }
}
