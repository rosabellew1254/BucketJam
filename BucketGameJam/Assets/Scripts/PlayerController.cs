using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController pc;
    public int curSanity;
    public int dayStartSanity;
    public Slider sliderSanity;
    public bool curSanSetup;
    public int[] sanityCap;

    public Sprite[] dateState;
    public Sprite[] spekState;
    public Sprite[] sanityTxtState;
    public Sprite[] sanitySliderState;
    public Sprite[] bMainMenuState;
    public Sprite[] bInventoryState;
    public Sprite[] spriteSanBubble;
    public Image iDate;
    public Image iSpeks;
    public Image iSanityText;
    public Image iSanitySlider;
    public Image iSanityNumberBubble;
    public Button bMainMenu;
    public Button bInventory;

    public Toggle musicToggle;
    public Image musicToggleImage;
    public Sprite[] musicToggleSprite;

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
        // cap the sanity when it reaches the max/min value
        if (curSanity + _adjustment > sanityCap[1])
        {
            curSanity = sanityCap[1];
        }
        else if (curSanity + _adjustment < sanityCap[0])
        {
            curSanity = sanityCap[0];
        }
        else
        {
            curSanity += _adjustment;
        }
        sliderSanity.value = curSanity;
        if (curSanity < 0)
        {
            iDate.sprite = dateState[1];
            iSpeks.sprite = spekState[1];
            iSanityText.sprite = sanityTxtState[1];
            iSanitySlider.sprite = sanitySliderState[1];
            bMainMenu.GetComponent<Image>().sprite = bMainMenuState[1];
            bInventory.GetComponent<Image>().sprite = bInventoryState[1];
            iSanityNumberBubble.sprite = spriteSanBubble[1];
        }
        else
        {
            iDate.sprite = dateState[0];
            iSpeks.sprite = spekState[0];
            iSanityText.sprite = sanityTxtState[0];
            iSanitySlider.sprite = sanitySliderState[0];
            bMainMenu.GetComponent<Image>().sprite = bMainMenuState[0];
            bInventory.GetComponent<Image>().sprite = bInventoryState[0];
            iSanityNumberBubble.sprite = spriteSanBubble[0];
        }
    }

    public void OnMainMenu()
    {
        //gm.LoadScene(0);
        confirm = Instantiate(gm.confirmPrefab, gm.generalHUD.gameObject.transform.position, Quaternion.identity, gm.generalHUD.gameObject.transform);
    }
    
    public void OnToggleMusic()
    {
        musicToggle = GameObject.Find("Toggle Music").GetComponent<Toggle>();
        if (musicToggle.isOn == true)
        {
            musicToggleImage.sprite = musicToggleSprite[0];
            FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
            musicBus.setVolume(1f);
        }
        else
        {
            musicToggleImage.sprite = musicToggleSprite[1];
            FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
            musicBus.setVolume(0f);
        }
    }

    public void OnInventory()
    {

    }
}
