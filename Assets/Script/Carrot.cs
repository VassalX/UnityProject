using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

	public float speed = 1f;
	float direction = 0f;

	// Use this for initialization
	void Start() {
		StartCoroutine (destroyLater ());
	}

	IEnumerator destroyLater() {
		yield return new WaitForSeconds (3.0f);
		Destroy (this.gameObject);
	}

	public void launch(float direction){
		this.direction = direction;
		if (direction < 0) {
			this.GetComponent<SpriteRenderer> ().flipX = true;
		} else if (direction > 0) {
			this.GetComponent<SpriteRenderer> ().flipX = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(direction != 0)
		transform.Translate (Vector2.right * direction * Time.deltaTime * speed);
	}

	protected override void OnRabitHit (HeroRabit rabit)
	{
		if (!rabit.isDamaged) {
			rabit.makeSmaller ();
			this.CollectedHide ();
		}
	}
}
