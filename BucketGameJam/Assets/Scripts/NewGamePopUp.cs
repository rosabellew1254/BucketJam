using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePopUp : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.gm.RestartGameGM();
    }

    public void ResumeGame()
    {
        GameManager.gm.SetupGameValues();
        GameManager.gm.LoadScene(4);
    }

}
