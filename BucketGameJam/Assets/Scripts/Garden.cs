using UnityEngine;

public class Garden : MonoBehaviour
{
    GameManager gm;

    public static Garden garden;
    public GameManager.plants[] plants;
    public int[] curGrowth;
    public int numHoles = 8;

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/set_plant");
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

    public bool IsGrowing(int _position)
    {
        return curGrowth[_position] > 0 && curGrowth[_position] < gm.plantData[(int)plants[_position]].turnsToGrow;
    }

    public void Grow()
    {
        for (int i = 0; i < numHoles; i++)
        {
            if (plants[i] == GameManager.plants.terminator)
            {
                continue;
            }

            curGrowth[i]++;
        }
    }


}
