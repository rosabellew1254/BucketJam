using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    private void Start()
    {
        GameManager.gm.generalHUD.SetActive(false);
    }

    public void ExitCredits()
    {
        GameManager.gm.LoadScene(0);

    }
}
