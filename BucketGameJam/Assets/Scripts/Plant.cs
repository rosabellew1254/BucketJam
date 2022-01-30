using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public GameManager.plants type;
    PlantsSO data;
    public GameObject[] goPhase;

    private void Start()
    {
        data = GameManager.gm.plantData[(int)type];

    }

}
