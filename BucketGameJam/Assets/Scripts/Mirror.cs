using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mirror : MonoBehaviour
{
    public Sprite normalRefl;
    public Sprite[] smallEvilRefls;
    public Sprite[] bigEvilRefls;

    private void Start()
    {
        int randNumSmall = Random.Range(0, 2);
        int randNumBig = Random.Range(0, 4);
        switch (GameManager.gm.worldState)
        {
            case GameManager.state.normal:
                gameObject.GetComponent<Image>().sprite = normalRefl;
                break;
            case GameManager.state.smallEvil:
                gameObject.GetComponent<Image>().sprite = smallEvilRefls[randNumSmall];
                break;
            case GameManager.state.largeEvil:
                gameObject.GetComponent<Image>().sprite = bigEvilRefls[randNumBig];
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
