using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController pc;
    int curSanity;
    public Slider sliderSanity;

    private void Start()
    {
        pc = this;
    }

    public void AdjustSanity(int _adjustment)
    {
        curSanity += _adjustment;
        sliderSanity.value = curSanity;
    }
}
