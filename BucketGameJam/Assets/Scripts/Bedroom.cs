using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : MonoBehaviour
{
    public GameObject mirrorReflection;
    GameManager gm;

    private void Start()
    {
        Debug.Log(GameManager.gm);
        mirrorReflection = FindObjectOfType<GameManager>().gameObject.GetComponent<GameManager>().mirrorReflection;
        //Debug.Log(GameManager.gm.gameObject);
    }

    public void Mirror()
    {
        //Show reflection of player's current state
        GameObject mirror = Instantiate(mirrorReflection, gameObject.transform.position, Quaternion.identity, gameObject.transform);
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
