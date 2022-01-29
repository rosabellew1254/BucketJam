using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    public Text txtMoney;

    public int[] seeds;
    int[] plants;
    int money;

    private void Start()
    {
        inventory = this;
        seeds = new int[(int)GameManager.plants.terminator];
        plants = new int[(int)GameManager.plants.terminator];
        AdjustMoney(200);
    }

    public void AdjustMoney(int _adjustAmount)
    {
        money += _adjustAmount;
        txtMoney.text = "Money: " + money;
    }

    public void AdjustSeedQuantity(GameManager.plants _plant, int _adjustAmount)
    {
        seeds[(int)_plant] += _adjustAmount;
    }

    public void AdjustPlantQuantity(GameManager.plants _plant, int _adjustAmount)
    {
        plants[(int)_plant] += _adjustAmount;
    }

    public void ShowInventory()
    {
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
                AdjustSeedQuantity((GameManager.plants)i, 1);
            }
        }
    }
}
