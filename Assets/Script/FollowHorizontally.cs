using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHorizontally : MonoBehaviour {

	public HeroRabit rabit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform rabit_tr = rabit.transform;
		Transform tr = this.transform;
		Vector3 rabit_vec = rabit_tr.position;
		Vector3 vec = tr.position;
		vec.x = rabit_vec.x;
		this.transform.position = vec;
	}
}
