using UnityEngine;
using UnityEngine.UI;

public class SellPlants : MonoBehaviour
{
    GameManager gm;
    Inventory inventory;
    bool isMinus;
    GameManager.plants curPlant;
    public Text[] inventoryQuantity;
    public Text[] textSellQuantity;
    int[] sellQuantity;
    public Text[] textSalePrice;
    int[] salePrice;
    public Text[] textSanityChange;
    int[] sanityChange;
    public Text textTotalSale;
    int totalSale;
    public Text textTotalSanity;
    int totalSanity;
    public Button[] minusButtons;
    public Button[] plusButtons;
    public Button confirmButton;
    public Image backgroundImage;
    public Sprite backgroundEldritch;
    public Sprite minusEldritchSprite;
    public Sprite plusEldritchSprite;
    public Sprite eldritchConfirmSprite;
    public Image closeButtonImage;
    public Sprite eldritchCloseSprite;

    private void Start()
    {
        gm = GameManager.gm;
        inventory = Inventory.inventory;

        int numPlants = (int)GameManager.plants.terminator;
        sellQuantity = new int[numPlants];
        salePrice = new int[numPlants];
        sanityChange = new int[numPlants];

        ShowButtons();

        if (PlayerController.pc.curSanity < 0)
        {
            backgroundImage.sprite = backgroundEldritch;
            for (int i = 0; i < minusButtons.Length; i++)
            {
                minusButtons[i].GetComponent<Image>().sprite = minusEldritchSprite;
                plusButtons[i].GetComponent<Image>().sprite = plusEldritchSprite;
            }
            confirmButton.GetComponent<Image>().sprite = eldritchConfirmSprite;
            closeButtonImage.sprite = eldritchCloseSprite;
        }

        for (int i = 0; i < numPlants; i++)
        {
            inventoryQuantity[i].text = inventory.plants[i].ToString();
        }
    }

    public void IsMinus(bool _isMinus)
    {
        isMinus = _isMinus;
    }

    public void SetPlant(int _plantIndex)
    {
        curPlant = (GameManager.plants)(_plantIndex);
    }

    public void AdjustSellAmount(int _adjustAmount)
    {
        int plantIndex = (int)curPlant;
        PlantsSO plantData = gm.plantData[plantIndex];
        int adjustAmount = isMinus ? -_adjustAmount : _adjustAmount;

        sellQuantity[plantIndex] += adjustAmount;
        textSellQuantity[plantIndex].text = sellQuantity[plantIndex].ToString();

        salePrice[plantIndex] += plantData.sellPrice * adjustAmount;
        textSalePrice[plantIndex].text = salePrice[plantIndex].ToString();

        sanityChange[plantIndex] += plantData.sanity * adjustAmount;
        textSanityChange[plantIndex].text = sanityChange[plantIndex].ToString();

        totalSale += plantData.sellPrice * adjustAmount;
        textTotalSale.text = totalSale.ToString();

        totalSanity += plantData.sanity * adjustAmount;
        textTotalSanity.text = totalSanity.ToString();

        inventoryQuantity[plantIndex].text = (inventory.plants[plantIndex] - sellQuantity[plantIndex]).ToString();

        ShowButtons();
    }

    void ShowButtons()
    {
        for (int i = 0; i < (int)GameManager.plants.terminator; i++)
        {
            minusButtons[i].GetComponent<Image>().enabled = sellQuantity[i] > 0;
            //minusButtons[i].GetComponentInChildren<Text>().enabled = sellQuantity[i] > 0;
            plusButtons[i].GetComponent<Image>().enabled = sellQuantity[i] < inventory.plants[i];
            //plusButtons[i].GetComponentInChildren<Text>().enabled = sellQuantity[i] < inventory.plants[i];
        }

        confirmButton.gameObject.SetActive(totalSale > 0);
    }

    public void ConfirmSale()
    {
        inventory.AdjustMoney(totalSale);
        PlayerController.pc.AdjustSanity(totalSanity);
        gm.tSanity.text = PlayerController.pc.curSanity.ToString();

        for (int i = 0; i < (int)GameManager.plants.terminator; i++)
        {
            inventory.AdjustPlantQuantity((GameManager.plants)i, -sellQuantity[i]);
        }

        FindObjectOfType<CursorScript>().UpdateSprite();
        FindObjectOfType<Shop>().UpdateUiForSanity();
        AudioManager.am.PlaySFX("event:/buy");
        CloseMenu();
    }

    public void ButtonSound()
    {
        AudioManager.am.PlaySFX("event:/click");
    }

    public void CloseMenu()
    {
        FindObjectOfType<Shop>().ShowSellPlantsButton();
        Destroy(gameObject);
    }
}
