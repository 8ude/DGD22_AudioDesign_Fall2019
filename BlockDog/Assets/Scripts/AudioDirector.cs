using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioDirector : MonoBehaviour
{

    //we make this a "singleton" because we know that there is only one of it
    //this is an often-overused programming pattern, but it can work ok for sound
    public static AudioDirector Instance;

    //an audio mixer snapshot contains volume levels and effect parameters
    //we can transition to a snapshot, which changes the levels over time
    //right now we have 2 snapshots - for the gameplay, and for gameOver
    public AudioMixerSnapshot gameplaySnapshot, gameOverSnapshot;

    //you can have as many mixer groups as you want, but keep in mind that you need to assign audio sources to them
    public AudioMixerGroup actionSFX, gameStateSFX;

    //there are many ways to manage sound objects.  
    //this particular one incurs a small performance overhead, because we are instantiating and destroying a prefab
    //if I were to make this more efficient, I might start by making an object pool equal to the number of "real" voices
    //that I want to play at a given time...
    public GameObject SoundPrefab;

    //We have 2 looping sources at the moment, which we keep as children of this game object
    public AudioSource musicSource;
    public AudioSource dangerSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //Set the appropriate clips and volume on music and danger loop, then play
        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();

        dangerSource.clip = dangerLoop;
        dangerSource.volume = 0f;
        dangerSource.Play();

    }

    [Tooltip("Adjust this for panning intensity (0 is 'centered', 1 is 'full panning')")]
    [Range(0f, 1f)] public float PanningSpread = 0.5f;

    [Tooltip("Uncheck this if you don't want any sounds to have pitch randomization")]
    public bool pitchRandEnabled = true;


    [Header("Game Sound Effects")] public AudioClip gameStartSound;
    [Range(0f, 1f)] public float gameStartVolume = 1.0f;
    [Tooltip("for scoring sounds, we have an array, and will pick a sound at random when we score")]
    public AudioClip[] matchSounds;
    [Range(0f, 1f)] public float matchVolume = 1.0f;
    public AudioClip gameOverSound;
    [Range(0f, 1f)] public float gameOverVolume = 1.0f;

    [Header("Player Action Sound Effects")] public AudioClip jumpSound;
    [Range(0f, 1f)] public float jumpVolume = 1.0f;
    public AudioClip pickupSound;
    [Range(0f, 1f)] public float pickupVolume = 1.0f;
    public AudioClip throwSound;
    [Range(0f, 1f)] public float throwVolume = 1.0f;
    public AudioClip landSound;
    [Range(0f, 1f)] public float landVolume = 1.0f;

    [Header("BlockActionSoundEffects")] public AudioClip blockPrepareDrop;
    [Range(0f, 1f)] public float blockPrepVolume = 1.0f;
    public AudioClip blockDrop;
    [Range(0f, 1f)] public float blockDropVolume = 1.0f;
    public AudioClip blockImpact;
    [Range(0f, 1f)] public float blockLandVolume = 1.0f;

    [Header("Loops and Background")] public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 1.0f;
    [Tooltip("This is a loop that plays when the blocks get close to the top, which triggers a game over")]
    public AudioClip dangerLoop;
    [Range(0f, 1f)] public float dangerVolume = 1.0f;


    /// <summary>
    /// This is what we use to play most sounds in the game
    /// </summary>
    /// <param name="clipToPlay">The Sound to Play</param>
    /// <param name="usePanning">Should we use panning?</param>
    /// <param name="xPosition">The X coordinate of the object playing the sound (if needed)</param>
    /// <param name="volume">the volume to play the sound at</param>
    /// <param name="pitchRand">any pitch randomization to use</param>
    /// <param name="gsSound">does this Sound Effect pertain to the game state?</param>
    public void PlaySound(AudioClip clipToPlay, bool usePanning = false, float xPosition = 0f, float volume = 1.0f, float pitchRand = 0f, bool gsSound = false)
    {

        if(clipToPlay == null)
        {
            Debug.Log("AUDIO CLIP NOT ASSIGNED ON AUDIO DIRECTOR!");
            return;
        }

        GameObject newSound = Instantiate(SoundPrefab, Vector3.zero, Quaternion.identity);
        AudioSource newSoundSource = newSound.GetComponent<AudioSource>();

        //set variables on the audio source accordingly
        newSoundSource.clip = clipToPlay;
        
        if (usePanning)
        {
            newSoundSource.panStereo = PositionToPanning(xPosition);
        }

        if (pitchRandEnabled)
        {
            newSoundSource.pitch = 1.0f + Random.Range(-pitchRand, pitchRand);
        }

        newSoundSource.volume = volume;

        if (gsSound)
        {
            newSoundSource.outputAudioMixerGroup = gameStateSFX;
        }
        else
        {
            newSoundSource.outputAudioMixerGroup = actionSFX;
        }

        newSoundSource.Play();
        Destroy(newSound, clipToPlay.length);

    }

    /// <summary>
    /// This is --almost-- the same, but we can use it to play sounds from an array.  In fact, if you wanted to, you could make an array from any of the audioclip variables above, and it would pick one at random.
    /// </summary>
    /// <param name="clipToPlay">The Sound to Play</param>
    /// <param name="usePanning">Should we use panning?</param>
    /// <param name="xPosition">The X coordinate of the object playing the sound (if needed)</param>
    /// <param name="volume">the volume to play the sound at</param>
    /// <param name="pitchRand">any pitch randomization to use</param>
    /// <param name="gsSound">does this Sound Effect pertain to the game state?</param>
    public void PlaySound(AudioClip[] clipsToPlay, bool usePanning = false, float xPosition = 0f, float volume = 1.0f, float pitchRand = 0f, bool gsSound = false)
    {
        GameObject newSound = Instantiate(SoundPrefab, Vector3.zero, Quaternion.identity);
        AudioSource newSoundSource = newSound.GetComponent<AudioSource>();

        if (clipsToPlay[0] == null)
        {
            Debug.Log("AUDIO CLIP NOT ASSIGNED ON AUDIO DIRECTOR!");
            return;
        }

        //pick a random clip from the array
        newSoundSource.clip = clipsToPlay[Random.Range(0, clipsToPlay.Length)];

        if (usePanning)
        {
            newSoundSource.panStereo = PositionToPanning(xPosition);
        }

        if (pitchRandEnabled)
        {
            newSoundSource.pitch = 1.0f + Random.Range(-pitchRand, pitchRand);
        }

        newSoundSource.volume = volume;

        if (gsSound)
        {
            newSoundSource.outputAudioMixerGroup = gameStateSFX;
        }
        else
        {
            newSoundSource.outputAudioMixerGroup = actionSFX;
        }

        //play the sound, and destroy the game object when finished
        newSoundSource.Play();
        Destroy(newSound, newSoundSource.clip.length);

    }

    

    //we aren't using the 3D panning options yet, so we need something to put things into left and right speakers
    float PositionToPanning(float xPosition)
    {
        return Remap(xPosition, 1f, 13f, -PanningSpread, PanningSpread);
    }

    //Always good to fade loops in and out, especially music/ambience loops

    public void FadeInAudio(AudioSource source, float destVolume, float timeToFade)
    {
        Debug.Log("fading in audio");
        StartCoroutine(LinearFadeIn(source, destVolume, timeToFade));
    }

    public void FadeOutAudio(AudioSource source, float timeToFade)
    {
        StartCoroutine(LinearFadeOut(source, timeToFade));
    }

    public IEnumerator LinearFadeIn(AudioSource audioSource, float destVol, float time)
    {
        while (audioSource.volume < destVol)
        {
            audioSource.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator LinearFadeOut(AudioSource audioSource, float time)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / time;
            yield return null;
        }
    }

    //Standard Linear Remapping Function
    public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    

    /*
     * While writing this I realized that Scriptable Objects offer a more flexible solution
     * For a game of this scale, my method is fine.  
     * 
     * But past a certain complexity, I'd recommend using scriptable objects.  They're easier to modify and extend later.
     * 
     * Additionally, SOAudioExamples have options to avoid repeating sounds, or to play sounds in sequence.
     * Doing that on this audio manager would be cumbersome after a while
     * 
     * To use them, you'd replace the AudioClips on this script with scriptable objects called SOAudioExample
     * 
     
     public SOAudioExample jumpSound;

     * you would also need to change the associated sound where it's called, because of the overloads
     * 
     * Then the PlaySound method would look something like this:
     *  
     
     public void PlaySound(SOAudioExample soundToPlay, float xPosition)
     {
        GameObject newSound = Instantiate(SoundPrefab, Vector3.zero, Quaternion.identity);
        AudioSource newSoundSource = newSound.GetComponent<AudioSource>();

        //pick a random clip from the array
        newSoundSource.clip = soundToPlay.GetClip();

        if (soundToPlay.usePanning)
        {
            newSoundSource.panStereo = PositionToPanning(xPosition);
        }
        newSoundSource.pitch = 1.0f + Random.Range(-soundToPlay.pitchRand, soundToPlay.pitchRand);

        newSoundSource.volume = soundToPlay.volume;

        //play the sound, and destroy the game object when finished
        newSoundSource.Play();
        Destroy(newSound, newSoundSource.clip.length);

    }

    * 
    */

}
