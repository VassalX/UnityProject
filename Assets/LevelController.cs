using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	int livesCount = 3;
	Vector3 startPosition;

	public static LevelController current;

	void Awake(){
		current = this;
	}

	public void setStartPosition(Vector3 pos){
		this.startPosition = pos;
	}

	public int getLivesCount (){
		return livesCount;
	}

	public void onRabitDeath(HeroRabit rabit){
		rabit.transform.position = this.startPosition;
		livesCount--;
	}
}