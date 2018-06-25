using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public AudioClip collectSound;
	protected AudioSource collectSource;
	bool hideAnimation = false;

	protected virtual void OnRabitHit(HeroRabit rabit) {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		
		if(!this.hideAnimation) {
			HeroRabit rabit = collider.GetComponent<HeroRabit>();
			if(rabit != null && !rabit.isDead) {
				collectSource.Play ();
				Debug.Log ("play");
				this.OnRabitHit (rabit);
			}
		}
	}
	public void CollectedHide() {
		Destroy(this.gameObject);
	}
}
