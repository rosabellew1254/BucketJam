using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cMainMenu : MonoBehaviour
{
    GameManager gm;

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
        gm.LoadScene(1);
    }

    // Method to exit the game
    public void OnExitGame()
    {
        Application.Quit();
    }
}
