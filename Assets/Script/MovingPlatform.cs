using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy;
	public float speed = 2;
	public float wait = 5;
	float time_to_wait;
	bool going_to_a;

	Vector3 pointA;
	Vector3 pointB;

	// Use this for initialization
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
		this.time_to_wait = wait;
		this.going_to_a = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 my_pos = this.transform.position;
		Vector3 target;

		if (this.going_to_a) {
			target = this.pointA;
		} else {
			target = this.pointB;
		}

		if(!isArrived(this.transform.position, target)){
			float path = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (transform.position, target, path);
		} else {
			this.time_to_wait -= Time.deltaTime;
			if (this.time_to_wait <= 0) {
				this.going_to_a = !this.going_to_a;
				this.time_to_wait = wait;
			}
 		}
	}

	bool isArrived(Vector3 pos, Vector3 target){
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance (pos, target) < 0.02f;
	}
}
