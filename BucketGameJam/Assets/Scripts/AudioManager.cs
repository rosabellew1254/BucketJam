using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    //public FMODUnity.EventReference HappyMusicStateEvent;
    public FMODUnity.EventReference[] events;

    //public FMOD.Studio.EventInstance happyMusicState; //town happy
    public FMOD.Studio.EventInstance[] instance;

    //public int StartingSanity = PlayerController.pc.curSanity;
    int sanityParam;
    public FMOD.Studio.PARAMETER_ID sanityParameterId;
    public FMOD.Studio.EventDescription sanityEventDescription;

    public bool instanceIsSetup = false;
    // instance 0: normal <- town--------------------------good
    // instance 1: small <- town---------------------------bad_small
    // instance 2: large <- town---------------------------bad
    // instance 3: normal <- bedroom, garden, shop---------bedroom
    // instance 4: small/large <- bedroom, garden, shop----bedroom_bad
    // instance 5: ending peaceful-------------------------peaceful_ending
    // instance 6: ending doubleFaced----------------------doublefaced_ending
    // instance 7: ending deserted-------------------------deserter_ending
    // instance 8: ending evil-----------------------------evil_ending
    // instance 9: title-----------------------------------title_music

    private void Awake()
    {
        am = this;
        instance = new FMOD.Studio.EventInstance[events.Length];
        for (int i = 0; i < events.Length; i++)
        {
            instance[i] = FMODUnity.RuntimeManager.CreateInstance(events[i]);
            instance[i].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        //instance[9].start();
        //happyMusicState = FMODUnity.RuntimeManager.CreateInstance(HappyMusicStateEvent);
        //happyMusicState.start();
        //FMOD.Studio.EventDescription sanityEventDescription;
        //happyMusicState.getDescription(out sanityEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION sanityParameterDescription;
        //sanityEventDescription.getParameterDescriptionByName("sanity", out sanityParameterDescription);
        //sanityParameterId = sanityParameterDescription.id;
        instanceIsSetup = true;
    }

    private void Start()
    {

    }

    void SpawnIntoWorld()
    {
        //sanityParam = StartingSanity;

        //--------------------------------------------------------------------
        // 7: This shows that a single event instance can be started, stopped, 
        //    and restarted as often as needed.
        //--------------------------------------------------------------------
        //happyMusicState.start();
    }

    void OnDestroy()
    {
        StopAllMusicEvents();
        //happyMusicState.release();
    }
    void StopAllMusicEvents()
    {
        FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
        musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


    public void StartInstance(int _index)
    {
        instance[_index].start();
    }

    public void StopInstance(int _index)
    {
        instance[_index].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        for (int i = 0; i < events.Length; i++)
        {
            instance[i].release();
        }
    }

    public IEnumerator PlaySound(int _index)
    {
        yield return new WaitUntil(() => instanceIsSetup == true);
        StartInstance(_index);
    }

}