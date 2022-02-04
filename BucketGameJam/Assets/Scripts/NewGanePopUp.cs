using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGanePopUp : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.gm.RestartGameGM();
    }

    public void ResumeGame()
    {
        GameManager.gm.LoadScene(4);
    }

}
