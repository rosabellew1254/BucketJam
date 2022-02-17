﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    PlayerController pc;
    //public Image[] evils;
    public int[] sanThreshold; // 0: small evil shows, 1: big evil shows
    public int townies;
    public Text nTownies;
    int san;
    public Button[] bTownObjects; // shop, farm
    public Sprite[] spriteButtonNormal; // shopNor, shopNorOutline, farmNor, farmNorOutline
    public Sprite[] spriteButtonEldritch; // shopEld, shopEldOutline, farmEld, farmEldOutline
    void Start()
    {
        pc = PlayerController.pc;
        StartCoroutine(pcSetup());
        for (int i = 0; i < bTownObjects.Length; i++)
        {
            bTownObjects[i].GetComponent<Image>().alphaHitTestMinimumThreshold = GameManager.gm.alphaHitMinValue;
        }
        //(button, normalUnselected, normalHighlighted, eldritchUnselected, eldritchHighlited)
        //(shop, shopNor, shopNorOutline, shopEld, shopEldOutline)
        GameManager.gm.UpdateButtonSprite(bTownObjects[0], spriteButtonNormal[0], spriteButtonNormal[1], spriteButtonEldritch[0], spriteButtonEldritch[1]);
        //(farm, farmNor, farmNorOutline, farmEld, farmEldOutline)
        GameManager.gm.UpdateButtonSprite(bTownObjects[1], spriteButtonNormal[2], spriteButtonNormal[3], spriteButtonEldritch[2], spriteButtonEldritch[3]);
        //AudioManager.am.happyMusicState.setParameterByName("sanity", pc.curSanity);
        //Debug.Log("cur sanity " + pc.curSanity);
        switch (GameManager.gm.worldState)
        {
            case GameManager.state.normal:
                StartCoroutine(AudioManager.am.PlaySound(0));
                break;
            case GameManager.state.smallEvil:
                StartCoroutine(AudioManager.am.PlaySound(1));
                break;
            case GameManager.state.largeEvil:
                StartCoroutine(AudioManager.am.PlaySound(2));
                break;
            case GameManager.state.terminator:
                break;
            default:
                break;
        }
    }

    public IEnumerator pcSetup()
    {
        yield return new WaitUntil(() => pc.curSanSetup == true);
        san = pc.curSanity;
        if (san < sanThreshold[0] && san >= sanThreshold[1])
        {
            //evils[0].gameObject.SetActive(true);
            if (san < sanThreshold[1])
            {
                //evils[1].gameObject.SetActive(true);
            }
        }
        townies = Mathf.RoundToInt(san * 1.5f + 300);
        nTownies.text = townies.ToString();
    }

    public void StopInstance()
    {
        switch (GameManager.gm.worldState)
        {
            case GameManager.state.normal:
                AudioManager.am.StopInstance(0);
                break;
            case GameManager.state.smallEvil:
                AudioManager.am.StopInstance(1);
                break;
            case GameManager.state.largeEvil:
                AudioManager.am.StopInstance(2);
                break;
            case GameManager.state.terminator:
                break;
            default:
                break;
        }
    }

    void Update()
    {

    }
}
