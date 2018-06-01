using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	int livesCount = 3;
	int coinsCount = 0;
	Vector3 startPosition;

	public static LevelController current;

	void Awake(){
		current = this;
	}

	public void setStartPosition(Vector3 pos){
		this.startPosition = pos;
	}

	public int getLivesCount (){
		return this.livesCount;
	}

	public void addCoins(int number){
		this.coinsCount += number;
	}

	public void onRabitDeath(HeroRabit rabit){
		rabit.transform.position = this.startPosition;
		this.livesCount--;
	}
}