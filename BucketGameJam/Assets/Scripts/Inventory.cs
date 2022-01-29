using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;

    public int[] seeds;
    int[] plants;
    int money;

    private void Start()
    {
        inventory = this;
        seeds = new int[(int)GameManager.seeds.terminator];
        plants = new int[(int)GameManager.plants.terminator];
    }

    public void AdjustMoney(int _adjustAmount)
    {
        money += _adjustAmount;
    }

    public void AdjustSeedQuantity(GameManager.seeds _seed, int _adjustAmount)
    {
        seeds[(int)_seed] += _adjustAmount;
    }

    public void AdjustPlantQuantity(GameManager.plants _plant, int _adjustAmount)
    {
        plants[(int)_plant] += _adjustAmount;
    }

    public void ShowInventory()
    {
        Debug.Log("You have " + money + " money.");
        for (int i = 0; i < seeds.Length; i++)
        {
            Debug.Log("You have " + seeds[i] + " seeds of type " + i);
        }

        for (int i = 0; i < plants.Length; i++)
        {
            Debug.Log("You have " + plants[i] + " plants of type " + i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < seeds.Length; i++)
            {
                AdjustSeedQuantity((GameManager.seeds)i, 1);
            }
        }
    }
}
