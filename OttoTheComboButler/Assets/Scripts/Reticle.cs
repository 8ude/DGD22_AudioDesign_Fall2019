using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {

    private Camera _camera;

	// Use this for initialization
	void Start () {

        _camera = GetComponent<Camera>();
	}

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "*"); // The command GUI.Label() displays text on screen
    }
}
