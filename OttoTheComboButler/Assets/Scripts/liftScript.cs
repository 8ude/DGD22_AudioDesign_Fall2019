using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftScript : MonoBehaviour {

    bool moveMeUp;
    bool moveMeDown;
    public GameObject pb;

    void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject.name == "Main Camera")
        {
            Debug.Log(col.gameObject.name + "HELLOOOOO");
            //raise the lift
            moveMeUp = true;
            //make the player a child of the lift 
            //GameObject go = col.transform.parent.gameObject;
            //go.transform.SetParent(this.gameObject.transform);
           // moveMeDown = false;
        }
    }

    /*void OnTriggerExit(Collider col)
    {
        Debug.Log(col.gameObject.name + "HELLOOOOO");
        if (col.gameObject.name == "PlayerBody")
        {
            //lower the lift
            moveMeDown = true;
           // moveMeUp = false;
        }
    }*/

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*Debug.Log("MOVE ME" + moveMeUp + " " + pb.transform.position.y);
		if(moveMeUp && this.gameObject.transform.position.y < 1.519f)
        {
            Debug.Log("I SHOULD BE MOVING UP");
            transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + .1f, this.gameObject.transform.position.z);
            pb.transform.position = new Vector3(pb.transform.position.x, pb.transform.position.y + .1f, pb.transform.position.z);
        }*/
       /* else if (moveMeUp)
        {
            //moveMeUp = false;
        }
        else
        {
            Debug.Log("POPP"+this.gameObject.transform.position.y);
        }

        if (moveMeDown && this.gameObject.transform.position.y > -1.44f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 2, this.gameObject.transform.position.z);
        }
        else if (moveMeDown)
        {
            moveMeDown = false;
        }*/
    }
}
