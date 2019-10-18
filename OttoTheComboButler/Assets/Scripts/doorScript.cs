using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerControlStickyGaze>().inSallysRoom = !player.GetComponent<PlayerControlStickyGaze>().inSallysRoom;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
