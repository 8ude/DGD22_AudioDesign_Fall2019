using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterInfo : MonoBehaviour
{
    public PlayerControlStickyGaze playerScript;
    public bool isSally;
    AudioSource audio;
    bool win = false;

    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public ParticleSystem ps3;
    bool playing = false;
    int timer = 0;

    private void OnCollisionEnter(Collision collision)
    {
        //get the task requester:
        string requestor = playerScript.currentRequestor;

        if (collision.gameObject.tag == "COMBO")
        {
            Debug.Log("COMBO..." + requestor);
            //if this is a sally object
            if (this.gameObject.name == requestor)
            {
                //correctly gave to right person.
                Debug.Log("Got here.");
                audio.Play();
                //destroy combo object and gain a point
                //Destroy(collision.gameObject);

                //if this is the final puzzle
                if(playerScript.taskNum == 6)
                {
                    win = true;
                    Debug.Log("YOU WIN YOU ARE DONE YAY");
                }

                playerScript.detachItems();
                playerScript.cleanCam();
                playerScript.nextTask();

                if(collision.gameObject.name == "SlimyCucumber")
                {
                    //OPEN APT2
                    // Destroy(playerScript.door1);
                    playerScript.door1.transform.position = new Vector3(playerScript.door1OpenPosition.x, playerScript.door1OpenPosition.y, playerScript.door1OpenPosition.z);
                    playerScript.door1.transform.eulerAngles = playerScript.door1OpenRotation;
                    playerScript.door1.GetComponent<BoxCollider>().enabled = false;
                    playerScript.doorAnim.SetTrigger("opened");
                    //playerScript.doorSound.Play();

                    //newAudio
                    AudioManager.instance.PlaySound(AudioManager.instance.deliverCorrectItem, gameObject);

                    //the door sound is set to play immediately, but it will be covered up by the deliverCorrectItem sound.  You'll need to add a bit of a start delay on the fmod event in order for it to be heard
                    AudioManager.instance.PlaySound(AudioManager.instance.doorOpening, playerScript.door1);

                }
                if (collision.gameObject.name == "SpicyPoops")
                {
                    //OPEN APT3
                    // Destroy(playerScript.door1);
                   // playerScript.door2.transform.position = new Vector3(playerScript.door2OpenPosition.x, playerScript.door2OpenPosition.y, playerScript.door2OpenPosition.z);
                    playerScript.door2.transform.eulerAngles = playerScript.door2OpenRotation;
                    playerScript.door2.GetComponent<BoxCollider>().enabled = false;
                    playerScript.doorAnim.SetTrigger("opened");
                    //playerScript.doorSound.Play();

                    //newAudio
                    AudioManager.instance.PlaySound(AudioManager.instance.deliverCorrectItem, gameObject);
                    AudioManager.instance.PlaySound(AudioManager.instance.doorOpening, playerScript.door2);
                }
                if (collision.gameObject.name == "Popcorn")
                {
                    //OPEN Theatre
                    // Destroy(playerScript.door1);
                    playerScript.door3.transform.position = new Vector3(playerScript.door3OpenPosition.x, playerScript.door3OpenPosition.y, playerScript.door3OpenPosition.z);
                    playerScript.door3.transform.eulerAngles = playerScript.door3OpenRotation;
                    playerScript.door3.GetComponent<BoxCollider>().enabled = false;
                    playerScript.doorAnim.SetTrigger("opened");
                    //playerScript.doorSound.Play();

                    //newAudio
                    AudioManager.instance.PlaySound(AudioManager.instance.deliverCorrectItem, gameObject);
                    AudioManager.instance.PlaySound(AudioManager.instance.doorOpening, playerScript.door3);
                }
            }
           /* if (!playerScript.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                playerScript.gameObject.GetComponent<AudioSource>().Play();
            }*/



            /* //if this is not a sally object
             if (!isSally)
             {
                 //correctly gave to right person (Bob).
                 Debug.Log("Got here.");
                 audio.Play();
                 //destroy combo object and gain a point
                 playerScript.detachItems();
                 playerScript.cleanCam();
                // Destroy(collision.gameObject);
                 playerScript.nextTask();
             }*/
            else
            {
                //wrong person!
                if (!playerScript.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    //playerScript.gameObject.GetComponent<AudioSource>().Play();
                }
                Debug.Log("WRONG PERSON");
                //play the particle systems
                ps1.Play();
                ps2.Play();
                ps3.Play();
                playing = true;

                //newAudio
                AudioManager.instance.PlaySound(AudioManager.instance.deliverWrongItem, gameObject);

            }



        }
        else
        {
            if (collision.gameObject.name != "Player Body" && collision.gameObject.name != "Main Camera")
            {
                if (!playerScript.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    //playerScript.gameObject.GetComponent<AudioSource>().Play();
                }

                //newAudio
                AudioManager.instance.PlaySound(AudioManager.instance.deliverWrongItem, gameObject);

                if (collision.gameObject.tag != "ignore")
                {
                    Debug.Log("WRONG PERSON" + collision.gameObject.name);
                    //play the particle systems
                    ps1.Play();
                    ps2.Play();
                    ps3.Play();
                    playing = true;
                }
            }

        }
    }
    // Use this for initialization
    void Start()
    {
        //playerScript = GameObject.Find("Main Camera").GetComponent<PlayerControlStickyGaze>();
        audio = this.gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(win && !audio.isPlaying)
        {
            Debug.Log("THE END WOULD NOW BE LOADING");
            //AVERY END THE GAME HERE

            AudioManager.instance.StopPlayerMovementSound();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(playing && timer < 30)
        {
            timer++;

        }
        else
        {
            ps1.Stop();
            ps2.Stop();
            ps3.Stop();
            playing = false;
            timer = 0;
        }
    }

}
