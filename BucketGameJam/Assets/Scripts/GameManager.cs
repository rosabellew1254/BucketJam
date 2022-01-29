using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public enum seeds { seed0, seed1, seed2, seed3, seed4, seed5, terminator}
    public enum plants { plant0, plant1, plant2, plant3, plant4, plant5, terminator}
    public enum scenes { frontEnd, town, garden, shop, bedroom, terminator }
    public GameObject sproutPrefab;

    [Space]
    [Header("Menus")]
    public GameObject pMain;

    public GameObject mirrorReflection;

    private void Awake()
    {
        if (gm != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            gm = this;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Whatever scene is loaded, instantiates appropriate menu objects
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            case 0:
                Instantiate(pMain).GetComponent<cMainMenu>();
                break;
            case 1:
                break;
        }
    }

    // Method to load the scene
    public void LoadScene(int _idx)
    {
        SceneManager.LoadScene(_idx);
    }
}
