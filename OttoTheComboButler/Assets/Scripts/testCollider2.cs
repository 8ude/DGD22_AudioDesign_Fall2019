using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testCollider2 : MonoBehaviour
{
    public Text good;

    void OnTriggerEnter(Collider col)
    {
        good.enabled = true;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
