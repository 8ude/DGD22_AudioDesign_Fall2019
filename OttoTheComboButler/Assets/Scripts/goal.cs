using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {
    public ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        ps.Play();
    }
    // Use this for initialization
    void Start () {
        ps.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
