using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mirror : MonoBehaviour
{
    public Sprite normalRefl;
    public Sprite[] smallEvilRefls;
    public Sprite[] bigEvilRefls;
    public GameObject overlay;

    private void Start()
    {
        int randNumSmall = Random.Range(0, 2);
        int randNumBig = Random.Range(0, 4);
        switch (GameManager.gm.worldState)
        {
            case GameManager.state.normal:
                gameObject.GetComponent<Image>().sprite = normalRefl;
                overlay.gameObject.SetActive(false);
                break;
            case GameManager.state.smallEvil:
                gameObject.GetComponent<Image>().sprite = smallEvilRefls[randNumSmall];
                overlay.gameObject.SetActive(false);
                break;
            case GameManager.state.largeEvil:
                gameObject.GetComponent<Image>().sprite = bigEvilRefls[randNumBig];
                overlay.gameObject.SetActive(true);
                break;
            case GameManager.state.terminator:
                break;
            default:
                break;
        }
        //
    }
    public void ClickMirror()
    {
        //fade object then destroy it

        Object.Destroy(gameObject);
    }

}
