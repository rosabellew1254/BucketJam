using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sister : MonoBehaviour
{
    public Image textBox;
    public Text textBoxFill;
    public GameObject choices;



    [Space]
    [Header("Dialogs")]
    public string[] textDialog;
    public string[] textHello;

    string[] currentArray;


    //create funtions for button dialogs if there is going to be any of that
    //make an array for each set of converstations

    //bool isInDialog;
    int textCount;


    /*
    //Example Text button function
    public void ReadText___()
    {
        textCount = 0;
        textBox.gameObject.SetActive(true);
        textBoxFill.text = ___[textCount];
        currentArray = new string[___.Length];

        for (int i = 0; i < currentArray.Length; i++)
        {
            currentArray[i] = ___[i];
        }
    }
    */

    public void ReadTextHello()
    {
        textCount = 0;
        textBox.gameObject.SetActive(true);
        textBoxFill.text = textHello[textCount];
        currentArray = new string[textHello.Length];

        for (int i = 0; i < currentArray.Length; i++)
        {
            currentArray[i] = textHello[i];
        }
    }

    public void ReadTextDialog()
    {
        textCount = 0;
        textBox.gameObject.SetActive(true);
        textBoxFill.text = textDialog[textCount];
        currentArray = new string[textDialog.Length];

        for (int i = 0; i < currentArray.Length; i++)
        {
            currentArray[i] = textDialog[i];
        }
    }

    public void ReadDialog() // Reads the dialog, don't touch
    {
        textCount++;
        if (textCount == currentArray.Length)
        {
            textBox.gameObject.SetActive(false);
        }
        else
        {
            textBoxFill.text = currentArray[textCount];
        }
    }

    public void Back()
    {
        Destroy(gameObject);
    }


    public void Update()
    {
        if (textBox.gameObject.activeSelf == false) // if no dialog
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
            if (choices.activeSelf != true)
            {
                choices.SetActive(true);
            }
        }
        else // if dialog
        {
            if (choices.activeSelf != false)
            {
                choices.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                textBox.gameObject.SetActive(false);
            }
        }
        
    }

}
