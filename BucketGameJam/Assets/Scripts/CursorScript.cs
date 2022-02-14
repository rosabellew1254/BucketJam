using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite eldritchSprite;
    GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        gm = GameManager.gm;
    }

    public void UpdateSprite()
    {
        Image myImage = GetComponent<Image>();
        int curSanity = FindObjectOfType<PlayerController>().curSanity;
        myImage.sprite = curSanity < 0 ? eldritchSprite : normalSprite;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
