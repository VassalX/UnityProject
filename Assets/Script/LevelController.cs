using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
	int livesCount = 3;
	int coinsCount = 0;
	int fruitsCount = 0;
	int maxAmountOfFruits = 0;
	int redFound = 0;
	int greenFound = 0;
	int blueFound = 0;
	Vector3 startPosition;
	public Text coinsText;
	public Text fruitsText;
	public Image redGemImage;
	public Image greenGemImage;
	public Image blueGemImage;
	public Image heart1Image;
	public Image heart2Image;
	public Image heart3Image;

	public static LevelController current;

	void Awake(){
		current = this;
		maxAmountOfFruits = GameObject.FindGameObjectsWithTag ("Fruit").Length;
		coinsText.text = "0000";
		fruitsText.text = "0/" + maxAmountOfFruits;
	}

	void Start(){
		
	}

	public void setStartPosition(Vector3 pos){
		this.startPosition = pos;
	}

	public int getLivesCount (){
		return this.livesCount;
	}

	public void addCoins(int number){
		this.coinsCount += number;
		string strNum = coinsCount.ToString ();
		coinsText.text = "0000".Substring (strNum.Length ) + strNum;
	}

	public void addFruits(int number){
		this.fruitsCount += number;
		fruitsText.text = fruitsCount + "/" + maxAmountOfFruits;
	}

	public void addCrystals(char color){
		switch (color) {
		case 'r':
			redFound++;
			redGemImage.canvasRenderer.SetAlpha (0f);
			break;
		case 'g':
			greenFound++;
			greenGemImage.canvasRenderer.SetAlpha (0f);
			break;
		case 'b':
			blueFound++;
			blueGemImage.canvasRenderer.SetAlpha (0f);
			break;
		}
	}

	public void onRabitDeath(HeroRabit rabit){
		switch (livesCount) {
		case 3:
			heart3Image.canvasRenderer.SetAlpha (0f);
			break;
		case 2:
			heart2Image.canvasRenderer.SetAlpha (0f);
			break;
		case 1:
			heart1Image.canvasRenderer.SetAlpha (0f);
			break;
		}
		this.livesCount--;
		rabit.normalizeScale ();
		rabit.transform.position = this.startPosition;
		rabit.isDead = false;
	}
}