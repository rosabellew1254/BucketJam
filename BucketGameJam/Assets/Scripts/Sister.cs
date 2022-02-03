using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sister : MonoBehaviour
{
    public Image textBox;
    public Text textBoxFill;

    [Space]
    [Header("Dialogs")]
    public string[] textDialog;

    //create funtions for button dialogs if there is going to be any of that
    //make an array for each set of converstations



    public void TextDialog()
    {





    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

}
