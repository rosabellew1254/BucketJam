using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public float waitTime;
    FMOD.Studio.PLAYBACK_STATE state;
    float volume;
    private void Start()
    {
        GameManager.gm.generalHUD.SetActive(false);
        StartCoroutine(AudioManager.am.PlayMusic(12));

        AudioManager.am.musicInstance[12].getPlaybackState(out state);
        FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
        musicBus.getVolume(out volume);
        Debug.Log(state + "volume: " + volume);
        if (waitTime != 0)
        {
            Invoke("ExitCredits", waitTime);
        }
    }

    

    public void ExitCredits()
    {
        AudioManager.am.StopInstance(12);
        GameManager.gm.LoadScene(0);

    }
}
