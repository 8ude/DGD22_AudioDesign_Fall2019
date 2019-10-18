using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //The audio manager for FMOD is a bit different that standard Unity, but we don't do as much setting volume, pitch, etc (all of that is done in FMOD Studio)

    //A lot of the scripting techniques I use come from the Unity Example on FMOD's website: https://www.fmod.com/resources/documentation-unity?version=2.0&page=examples-basic.html

    //These strings are how Unity/FMOD look up the corresponding events in the sound banks

    [Header("UI Sounds")]
    [FMODUnity.EventRef] public string gameStartSound;
    [FMODUnity.EventRef] public string gameWinSound, menuButtonSound, hoverOverObjectSound;

    [Header("Player Sounds")]
    [FMODUnity.EventRef] public string pickupObjectSound;
    [FMODUnity.EventRef] public string putdownObjectSound, playerMoveSound;

    [Header("Diegetic Sounds")]
    [FMODUnity.EventRef] public string doorOpening;
    [FMODUnity.EventRef] public string deliverWrongItem, deliverCorrectItem;

    [Header("NonDiegetic Sounds")]
    [FMODUnity.EventRef] public string createUndies;
    [FMODUnity.EventRef] public string createSpicy, createPopcorn, createMedicine, comboFailed;

    //since this is a loop, we have a sound instance in our audio manager
    FMOD.Studio.EventInstance playerMoveSoundInstance;

    //like in BlockDog, I'm using a singleton pattern
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    //this is our main way to play sounds in this game.  
    //Most non-loops can be played with PlayOneShotAttached, which attaches the fmod event to a game object and plays it.

    //this doesn't work if we have parameters though
    public void PlaySound(string soundToPlay, GameObject go)
    {
        //PlayOneShotAttached will play a sound and attach it to the specified transform

        FMODUnity.RuntimeManager.PlayOneShotAttached(soundToPlay, go);

    }

    //for sounds that have parameters, we need to keep track of the instance of the sound
    //we can attach a sound to an object with a rigidbody, and it will automatically update it's position and velocity
    public void StartPlayerMovementSound(Transform playerTransform, Rigidbody playerRB)
    {

        //check to see if we already have a player move sound running (in which case we just update the parameter)
        if (!playerMoveSoundInstance.isValid())
        {
            //first we make an instance of the sound
            playerMoveSoundInstance = FMODUnity.RuntimeManager.CreateInstance(playerMoveSound);
            //then attach that to the player object (now it will update position and velocity data)
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerMoveSoundInstance, playerTransform, playerRB);
            //then we start it and release it (release means the instance will be destroyed when playback stops)
            playerMoveSoundInstance.start();
            playerMoveSoundInstance.release();
        }

        playerMoveSoundInstance.setParameterByName("PlayerIsMoving", 1.0f);
    }

    //Because of how the playerMovement event is set up in fmod studio, we just set the playerIsMoving parameter to 0 to make it stop
    public void StopPlayerMovementSound()
    {
        playerMoveSoundInstance.setParameterByName("PlayerIsMoving", 0.0f);
    }

    //these are just so we can play sounds from the game menu buttons
    public void PlayGameStartSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(gameStartSound);
    }

    public void PlayButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(menuButtonSound);
    }

    


}
