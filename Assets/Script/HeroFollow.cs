using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabit rabit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform rabit_tr = rabit.transform;
		Transform camera_tr = this.transform;
		Vector3 rabit_vec = rabit_tr.position;
		Vector3 camera_vec = camera_tr.position;
		camera_vec.x = rabit_vec.x;
		camera_vec.y = rabit_vec.y;
		transform.position = camera_vec;
	}
}
