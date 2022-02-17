using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class TextureSwap : MonoBehaviour
{
    public bool sister;
    public Image[] baseObjects;
    public Sprite[] firstPhase;
    public Sprite[] secondPhase;
    public Sprite[] thirdPhase;

    public Text[] textCases;

    int tempInt = 0;
    //bool toLate;

    public Color[] textColorHud;



    private void Start()
    {
        tempInt = 0;
    }

    void SwapTexture(int _index)
    {
        switch (gm.worldState)
        {
            case state.normal:
                if (sister == true && gm.isSiblingAlive != true)
                {
                    Debug.Log("Can't turn back time bud");
                    baseObjects[_index].sprite = null;
                }
                else
                {
                    baseObjects[_index].sprite = firstPhase[_index];
                    if (textCases != null)
                    {
                        for (int i = 0; i < textCases.Length; i++)
                        {
                            textCases[i].color = textColorHud[0];
                        }
                    }
                }
                break;
            case state.smallEvil:
                if (sister == true && gm.isSiblingAlive != true)
                {
                    Debug.Log("Can't turn back time bud");
                    baseObjects[_index].sprite = null;
                }
                else
                {
                    baseObjects[_index].sprite = secondPhase[_index];
                }
                break;
            case state.largeEvil:
                //toLate = true;
                if (sister == true && gm.isSiblingAlive != true)
                {
                    Debug.Log("Can't turn back time bud");
                    baseObjects[_index].sprite = null;
                }
                else
                {
                    baseObjects[_index].sprite = thirdPhase[_index];
                    if (textCases != null)
                    {
                        for (int i = 0; i < textCases.Length; i++)
                        {
                            textCases[i].color = textColorHud[1];
                        }
                    }
                }
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
            if (sister == true && gm.isSiblingAlive == false)
            {
                baseObjects[_index].gameObject.SetActive(false);
            }
            else
            {
                baseObjects[_index].gameObject.SetActive(true);
            }
        }
    }

    void ChangeEachSprite(state _state)
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            SwapTexture(i);
        }
        gm.tempWorldState = gm.worldState;
    }

    void Update()
    {
        if (tempInt == 0 || gm.uncessessaryButAlsoNessessaryCheck == true)
        {
            gm.uncessessaryButAlsoNessessaryCheck = false;
            ChangeEachSprite(gm.worldState);
            tempInt++;
        }
    }
}
