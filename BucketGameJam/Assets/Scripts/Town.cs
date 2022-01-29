﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    PlayerController pc;
    public Image[] evils;
    public int[] sanThreshold; // 0: small evil shows, 1: big evil shows
    public int townies;
    public Text nTownies;
    int san;

    // Start is called before the first frame update
    void Start()
    {
        pc = PlayerController.pc;
        StartCoroutine(pcSetup());
        if (san < sanThreshold[0] && san >= sanThreshold[1])
        {
            evils[0].gameObject.SetActive(true);
            if (san < sanThreshold[1])
            {
                evils[1].gameObject.SetActive(true);
            }
        }
        townies = Mathf.RoundToInt(san * 1.5f + 300);
        nTownies.text = townies.ToString();
    }

    IEnumerator pcSetup()
    {
        yield return new WaitUntil(() => pc != null);
        int san = pc.curSanity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
