using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuckboi : MonoBehaviour {
	public GameObject myCube;
	private BoxCollider myBox;
	private Vector3 goUp = new Vector3(0,1);

	// Use this for initialization
	void Start () {
		myBox = myCube.GetComponent<BoxCollider>();
		
	}
	
	// Update is called once per frame
	void Update () {
		myCube.transform.position+=goUp;
		
	}
}
