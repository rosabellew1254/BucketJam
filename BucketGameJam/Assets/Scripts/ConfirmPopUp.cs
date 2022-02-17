using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmPopUp : MonoBehaviour
{
    

    public void YesConfirm()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex((int)GameManager.scenes.garden) || SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex((int)GameManager.scenes.shop) || SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex((int)GameManager.scenes.bedroom))
        {
            switch (GameManager.gm.worldState)
            {
                case GameManager.state.normal:
                    AudioManager.am.StopInstance(3);
                    break;
                case GameManager.state.smallEvil:
                case GameManager.state.largeEvil:
                    AudioManager.am.StopInstance(4);
                    break;
                case GameManager.state.terminator:
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (GameManager.gm.worldState)
            {
                case GameManager.state.normal:
                    AudioManager.am.StopInstance(0);
                    break;
                case GameManager.state.smallEvil:
                    AudioManager.am.StopInstance(1);
                    break;
                case GameManager.state.largeEvil:
                    AudioManager.am.StopInstance(2);
                    break;
                case GameManager.state.terminator:
                    break;
                default:
                    break;
            }
        }
        GameManager.gm.LoadScene(0);
        Destroy(gameObject);
    }
    public void ButtonSound()
    {
        AudioManager.am.PlaySFX("event:/click");
    }

    public void NoConfirm()
    {
        Destroy(gameObject);
    }

}
