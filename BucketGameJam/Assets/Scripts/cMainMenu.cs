using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cMainMenu : MonoBehaviour
{
    GameManager gm;
    GameObject popUp;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads the into the game's first scene
    public void OnPlayGame()
    {
        if (gm.curDay != 0)
        {
            popUp = Instantiate(gm.newGamePopUpPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        }
        else
        {
            gm.RestartGameGM();
            gm.LoadScene(4);
        } 
    }

    public void OnCredits()
    {
        gm.generalHUD.SetActive(false);
        gm.LoadScene(6);
    }

    // Method to exit the game
    public void OnExitGame()
    {
        Application.Quit();
    }
}
