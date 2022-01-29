using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public enum plants { turnip, strawberry, eyePomegranite, mouthApple, terminator}
    public enum scenes { frontEnd, town, garden, shop, bedroom, terminator }
    public GameObject[] plantPrefabs;
    public PlantsSO[] plantData;
    public GameObject mirrorReflection;
    public Text date;

    int curDay = 0;
    int maxDays = 36;
    int moneyRequiredToSaveSibling = 5000;
    int competeMoneyGoal = 12000;

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

    void AdvanceDay()
    {
        curDay++;
        date.text = "Date: " + curDay;
    }
}
