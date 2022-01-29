using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public enum seeds { seed0, seed1, seed2, seed3, seed4, seed5, terminator}
    public enum plants { plant0, plant1, plant2, plant3, plant4, plant5, terminator}

    public enum scenes { frontEnd, town, garden, shop, bedroom, terminator }

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
        }
    }
}
