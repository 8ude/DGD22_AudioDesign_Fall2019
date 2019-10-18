using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControlCupid : MonoBehaviour
{

    public Camera cam;
    public Collider testCollider;
    public Collider floor;
    public GameObject player;
    int timer;
    bool timeme;
    bool played = false;
    bool moved = false;
    public AudioSource testAudio1;
    public AudioSource testAudio2;
    public MeshRenderer testswap;
    public Text evil;
    public Text good;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ///Restart Scene on Escape Press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }

        float z = Input.GetAxis("Vertical") * Time.deltaTime;
        gameObject.transform.position += z * transform.forward * 2f;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;
        gameObject.transform.position += x * transform.right * 2f;

        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.93f, gameObject.transform.position.z);

        //Cast a ray from screen center into space
        Ray ray = cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);//show the debug ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f)) //the 10f is the length the ray extends in distance
        {
            //A collision occured between the ray and a thing
            if (hit.collider != null && hit.collider != floor)
            {
                if (hit.collider.gameObject.name != "brain2")
                {
                    if (Input.GetKey(KeyCode.T))
                    {
                        hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hit.collider.transform.parent = cam.transform;
                    }
                    
                    //hit.collider.transform.GetComponent<Rigidbody>().velocity = cam.transform.GetComponent<Rigidbody>().velocity;
                    // Debug.Log("YOU DOOD IT");
                    moved = true;
                }
                else
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hit.collider.transform.parent = cam.transform;
                    }
                    timer++;
                    if (timer > 30)
                    {
                        if (!played && hit.collider.GetComponent<MeshRenderer>().enabled == true)
                        {
                            //turn off the first object and turn on the child
                            hit.collider.GetComponent<MeshRenderer>().enabled = false;
                            MeshRenderer[] child = hit.collider.transform.GetComponentsInChildren<MeshRenderer>();
                            child[1].GetComponent<MeshRenderer>().enabled = true;
                            Debug.Log(child[1].gameObject.name + " " + hit.collider.gameObject.name);
                            if (!played)
                            {
                                testAudio1.Play();
                                played = true;
                            }

                            timer = 0;
                        }
                        else
                        {
                            if (moved && !testAudio1.isPlaying)
                            {
                                //if the player looked and came back
                                hit.collider.GetComponent<MeshRenderer>().enabled = true;
                                MeshRenderer[] child = hit.collider.transform.GetComponentsInChildren<MeshRenderer>();
                                child[1].GetComponent<MeshRenderer>().enabled = false;
                                testAudio2.Play();
                            }

                        }

                    }
                }
                //if (Input.GetKeyDown(KeyCode.E))
                // {

                //
                // }
            }
            else
            {
                moved = true;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //push space to launch stuff
            //this.transform.parent = null;
            detachItems();
            cam.transform.DetachChildren();

            Debug.Log("YOU DOOD IT");
        }
    }

    void detachItems()
    {
        //remove items from sticky gaze
        Rigidbody[] items = cam.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody i in items)
        {
            i.useGravity = true;
            //Vector3 force = 7f * Time.deltaTime * transform.forward;
            //i.AddForce(force);
        }
    }
}

