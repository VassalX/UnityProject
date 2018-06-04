using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	int livesCount = 3;
	int coinsCount = 0;
	int fruitsCount = 0;
	int crystalsCount = 0;
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

	public void addFruits(int number){
		this.fruitsCount += number;
	}

	public void addCrystals(int number){
		this.crystalsCount += number;
	}

	public void onRabitDeath(HeroRabit rabit){
		this.livesCount--;
		rabit.normalizeScale ();
		rabit.transform.position = this.startPosition;
		rabit.isDead = false;
	}
}