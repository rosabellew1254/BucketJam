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


    int currentEnding;


    private void Start()
    {




        //endNameText.text = endName[]



        GameManager.gm.generalHUD.gameObject.SetActive(false);


    }
}
