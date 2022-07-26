using UnityEngine;
using System.Collections;

public class CreatAllUI : MonoBehaviour {


	public GameObject UIMainPrefab;

	// Use this for initialization
	void Start () {

		GameObject go = GameObject.Instantiate(UIMainPrefab , new Vector3( 0, 0, 0), Quaternion.identity) as GameObject;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
