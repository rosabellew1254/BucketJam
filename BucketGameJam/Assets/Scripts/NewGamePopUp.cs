using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePopUp : MonoBehaviour
{
    public void NewGame()
    {
        AudioManager.am.StopInstance(9);
        GameManager.gm.RestartGameGM();
    }

    public void ResumeGame()
    {
        AudioManager.am.StopInstance(9);
        GameManager.gm.SetupGameValues();
        GameManager.gm.LoadScene(4);
    }

    public void ButtonSound()
    {
        AudioManager.am.PlaySFX("event:/click");
    }

    public void Back()
    {
        Destroy(gameObject);
    }

}
