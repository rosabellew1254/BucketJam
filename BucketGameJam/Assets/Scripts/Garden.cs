using UnityEngine;

public class Garden : MonoBehaviour
{
    GameManager gm;

    public static Garden garden;
    public GameManager.plants[] plants;
    public int[] curGrowth;
    int numHoles = 15;

    private void Start()
    {
        gm = GameManager.gm;
        garden = this;
        plants = new GameManager.plants[numHoles];
        curGrowth = new int[numHoles];
        for (int i = 0; i < plants.Length; i++)
        {
            plants[i] = GameManager.plants.terminator;
        }
    }

    public void PlantPlant(GameManager.plants _plantType, int _position)
    {
        plants[_position] = _plantType;
    }

    public void RemovePlant(int _position)
    {
        plants[_position] = GameManager.plants.terminator;
        curGrowth[_position] = 0;
    }

    public bool IsFullyGrown(int _position)
    {
        if (plants[_position] == GameManager.plants.terminator)
        {
            return false;
        }

        PlantsSO plantData = gm.plantData[(int)plants[_position]];
        return curGrowth[_position] >= plantData.turnsToGrow;
    }
}
