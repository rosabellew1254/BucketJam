using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public enum plants { turnip, strawberry, eyePomegranite, mouthApple, terminator}
    public enum scenes { frontEnd, town, garden, shop, bedroom, endScene, terminator }
    public GameObject sproutPrefab;

    [Space]
    [Header("Menus")]
    public GameObject pMain;
    public GameObject generalHUD;

    public GameObject[] plantPrefabs;
    public PlantsSO[] plantData;
    public GameObject mirrorReflection;
    public Text date;

    public int curDay = 0;
    public int maxDays = 36;
    public int moneyRequiredToSaveSibling = 5000;
    public int competeMoneyGoal = 12000;

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
                generalHUD.gameObject.SetActive(false);
                break;
            default:
                generalHUD.gameObject.SetActive(true);
                break;
        }
    }

    // Method to load the scene
    public void LoadScene(int _idx)
    {
        SceneManager.LoadScene(_idx);
    }
    
    public void AdvanceDay()
    {
        curDay++;
        date.text = "Date: " + curDay;
    }
}
