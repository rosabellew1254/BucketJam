using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : MonoBehaviour
{
    public GameObject mirrorReflection;


    public void Mirror()
    {
        //Show reflection of player's current state
        //mirrorReflection = GetComponent<GameManager>
        mirrorReflection = Instantiate(GameManager.gm.mirrorReflection, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    public void Journal()
    {
        //Show character's notes / thoughts
    }

    public void Save()
    {
        //Save game
    }

}
