using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2Enemy : Orc1Enemy
{
	public GameObject prefabCarrot;
	float radius = 6f;
	float last_carrot = 0f;
	float shoot_time = 2f;

	void launchCarrot(float direction){
		GameObject obj = GameObject.Instantiate (this.prefabCarrot);
		Vector3 pos = this.transform.position;
		pos.y += this.GetComponent<SpriteRenderer> ().bounds.size.y / 2;
		obj.transform.position = pos;
		Carrot carrot = obj.GetComponent<Carrot> ();
		carrot.launch (direction);
		last_carrot = Time.time;
	}

	protected override void changeVelocity (float value)
	{
		base.changeVelocity (value);
	}

	protected override void changeRunning(float value){
		if (mode == Mode.Attack) {
			changeVelocity (0);
			if (Mathf.Abs (rabit_pos.x - my_pos.x) < radius
			    && Time.time - last_carrot > shoot_time) {
				this.myController.SetTrigger ("attack2");
				this.launchCarrot (sr.flipX ? 1 : -1);
			}
		} else {
			changeVelocity (value);
		}
		if (Mathf.Abs (myBody.velocity.x) > 0) {
			this.myController.SetBool ("run", true);
		} else {
			this.myController.SetBool ("run", false);
		}
	}
}
