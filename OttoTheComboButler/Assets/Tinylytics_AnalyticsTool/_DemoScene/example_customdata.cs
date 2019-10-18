using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example_customdata : MonoBehaviour {

	public float secondsDelayToSendData = 1f;
	void Start () {
		Invoke("SendCustomData",secondsDelayToSendData);
	}
	

void SendCustomData(){
GetComponent<Tinylytics.Tinylytics_MetricWidget>().OnCustomTrigger();
}

}
