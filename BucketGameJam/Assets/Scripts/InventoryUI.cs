using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public Text[] plants;
    public Text[] seeds;

    void Start()
    {
        inventory = Inventory.inventory;
        for (int i = 0; i < (int)GameManager.plants.terminator; i++)
        {
            plants[i].text = ((GameManager.plants)i).ToString() + ": " + inventory.plants[i];
            seeds[i].text = ((GameManager.plants)i).ToString() + ": " + inventory.seeds[i];
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
