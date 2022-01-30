using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : MonoBehaviour
{
    public GameObject mirrorReflection;

    GameManager gm;

    public void Start()
    {
        gm = GameManager.gm;
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

    public void ChangeWindowState()
    {

    }
}
