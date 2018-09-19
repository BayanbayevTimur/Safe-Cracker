using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobScript : MonoBehaviour {

	[SerializeField]public GameObject LockTick;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag()
	{
		this.transform.RotateAround (Vector3.zero, -Vector3.forward, 0.1f);
	}
}
