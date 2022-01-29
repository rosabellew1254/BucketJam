using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameManager.plants type;
    PlantsSO data;
    public int curGrowth;

    private void Start()
    {
        data = GameManager.gm.plantData[(int)type];
    }

    public bool IsFullyGrown()
    {
        return curGrowth >= data.turnsToGrow;
    }
}
