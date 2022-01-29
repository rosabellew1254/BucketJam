using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController pc;
    public int curSanity;
    public Slider sliderSanity;
    public bool curSanSetup;

    GameManager gm;

    private void Start()
    {
        gm = GameManager.gm;
        pc = this;
        if (!PlayerPrefs.HasKey("san"))
        {
            PlayerPrefs.SetInt("san", 25);
        }
        curSanity = PlayerPrefs.GetInt("san");
        curSanSetup = true;
    }

    public void AdjustSanity(int _adjustment)
    {
        curSanity += _adjustment;
        sliderSanity.value = curSanity;
    }

    public void OnMainMenu()
    {
        gm.LoadScene(0);
    }
    
    public void OnInventory()
    {

    }
}
