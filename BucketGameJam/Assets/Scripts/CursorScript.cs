using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    Image image;
    public Sprite normalSprite;
    public Sprite eldritchSprite;
    public static CursorScript cursor;
    GameManager gm;
    Camera mainCam;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cursor = this;
        gm = GameManager.gm;
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        mainCam = Camera.main;
    }

    private void OnLevelWasLoaded(int level)
    {
        mainCam = Camera.main;
    }

    public void UpdateSprite()
    {
        image.sprite = gm.worldState == GameManager.state.largeEvil ? eldritchSprite : normalSprite;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
