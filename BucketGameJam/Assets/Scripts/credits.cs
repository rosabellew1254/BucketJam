using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public float waitTime;

    private void Start()
    {
        GameManager.gm.generalHUD.SetActive(false);
        AudioManager.am.PlayMusic(12);
        if (waitTime != 0)
        {
            Invoke("ExitCredits", waitTime);
        }
    }

    

    public void ExitCredits()
    {
        GameManager.gm.LoadScene(0);

    }
}
