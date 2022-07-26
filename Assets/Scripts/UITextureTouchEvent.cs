using UnityEngine;
using System.Collections;

public class UITextureTouchEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}



	void OnTouched()
	{
		gameObject.SendMessage("OnUITextureTouched");

	}





	// Update is called once per frame
	void Update () {
	
	}
}
