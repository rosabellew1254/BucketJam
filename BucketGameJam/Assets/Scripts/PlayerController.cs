using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController pc;
    public int curSanity;
    public int dayStartSanity;
    public Slider sliderSanity;
    public bool curSanSetup;

    public Sprite[] dateState;
    public Sprite[] spekState;
    public Sprite[] sanityTxtState;
    public Sprite[] sanitySliderState;
    public Sprite[] bMainMenuState;
    public Sprite[] bInventoryState;
    public Image iDate;
    public Image iSpeks;
    public Image iSanityText;
    public Image iSanitySlider;
    public Image bMainMenu;
    public Image bInventory;

    GameManager gm;
    GameObject confirm;

    private void Start()
    {
        gm = GameManager.gm;
        pc = this;
        curSanity = PlayerPrefs.GetInt("san");
        curSanSetup = true;
        AdjustSanity(0);
        dayStartSanity = curSanity;
    }

    public void AdjustSanity(int _adjustment)
    {
        curSanity += _adjustment;
        sliderSanity.value = curSanity;
        if (curSanity < 0)
        {
            iDate.sprite = dateState[1];
            iSpeks.sprite = spekState[1];
            iSanityText.sprite = sanityTxtState[1];
            iSanitySlider.sprite = sanitySliderState[1];
            bMainMenu.sprite = bMainMenuState[1];
            bInventory.sprite = bInventoryState[1];
        }
        else
        {
            iDate.sprite = dateState[0];
            iSpeks.sprite = spekState[0];
            iSanityText.sprite = sanityTxtState[0];
            iSanitySlider.sprite = sanitySliderState[0];
            bMainMenu.sprite = bMainMenuState[0];
            bInventory.sprite = bInventoryState[0];
        }

    }

    public void OnMainMenu()
    {
        //gm.LoadScene(0);
        confirm = Instantiate(gm.confirmPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }
    
    public void OnInventory()
    {

    }
}
