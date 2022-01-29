using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text endNameText;
    public Text endingFillerText;


    public string[] endName;
    public string[] endFill;


    private void Awake()
    {
        GameManager.gm.generalHUD.gameObject.SetActive(false);


    }
}
