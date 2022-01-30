using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class TextureSwap : MonoBehaviour
{
    public Image[] baseObjects;
    public Sprite[] firstPhase;
    public Sprite[] secondPhase;
    public Sprite[] thirdPhase;

    state tempWorldState;

    private void Start()
    {
        tempWorldState = gm.worldState;
    }


    void SwapTexture(int _index)
    {
        switch (gm.worldState)
        {
            case state.normal:
                baseObjects[_index].sprite = firstPhase[_index];
                break;
            case state.smallEvil:
                baseObjects[_index].sprite = secondPhase[_index];
                break;
            case state.largeEvil:
                baseObjects[_index].sprite = thirdPhase[_index];
                break;
            case state.terminator:
                Debug.Log("Bruh, how?");
                break;
            default:
                break;
                
        }
        if (baseObjects[_index].sprite == null)
        {
            baseObjects[_index].gameObject.SetActive(false);
        }
        else
        {
            baseObjects[_index].gameObject.SetActive(true);
        }
    }

    void ChangeEachSprite(state _state)
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            SwapTexture(i);
        }
        tempWorldState = gm.worldState;
    }

    void Update()
    {
        if (tempWorldState != gm.worldState)
        {
            ChangeEachSprite(gm.worldState);
        }
    }
}
