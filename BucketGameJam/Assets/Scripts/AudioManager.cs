using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public FMODUnity.EventReference HappyMusicStateEvent;

    FMOD.Studio.EventInstance happyMusicState;

    public int StartingSanity = PlayerController.pc.curSanity;
    int sanityParam;
    FMOD.Studio.PARAMETER_ID sanityParameterId;

    private void Start()
    {
        happyMusicState = FMODUnity.RuntimeManager.CreateInstance(HappyMusicStateEvent);
        happyMusicState.start();
        FMOD.Studio.EventDescription sanityEventDescription;
        happyMusicState.getDescription(out sanityEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION sanityParameterDescription;
        sanityEventDescription.getParameterDescriptionByName("sanity", out sanityParameterDescription);
        sanityParameterId = sanityParameterDescription.id;
    }

    void SpawnIntoWorld()
    {
        sanityParam = StartingSanity;

        //--------------------------------------------------------------------
        // 7: This shows that a single event instance can be started, stopped, 
        //    and restarted as often as needed.
        //--------------------------------------------------------------------
        happyMusicState.start();
    }

    void OnDestroy()
    {
        StopAllMusicEvents();
        happyMusicState.release();
    }
    void StopAllMusicEvents()
    {
        FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
        musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


}