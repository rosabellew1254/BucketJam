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
            //plants[i].text = ((GameManager.plants)i).ToString() + ": " + inventory.plants[i];
            //seeds[i].text = ((GameManager.plants)i).ToString() + ": " + inventory.seeds[i];

            plants[i].text = inventory.plants[i].ToString();
            seeds[i].text = inventory.seeds[i].ToString();
        }
    }
    public void ButtonSound()
    {
        AudioManager.am.PlaySFX("event:/click");
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}
