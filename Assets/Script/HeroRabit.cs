using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

	public static HeroRabit lastRabit = null;

	public float speed = 1f;
	public bool isGrounded = false;
    public bool isDead = false;
	public bool isBig = false;
	public bool isDamaged = false;
	public float damageTime = 4f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	public float bigScale = 0.5f;
    
    bool JumpActive = false;
    float JumpTime = 0f;
	Rigidbody2D myBody = null;
	Transform heroParent = null;
	Vector3 normScale = new Vector3(1f,1f,0);
	Color normColor = Color.white;

	Animator animator = null;
	SpriteRenderer sr = null;

	void Awake(){
		lastRabit = this;
	}

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelController.current.setStartPosition (transform.position);
		heroParent = this.transform.parent;
		normScale = this.transform.localScale;
		animator = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		normColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			float value = Input.GetAxis ("Horizontal");
			if (Mathf.Abs (value) > 0) {
				animator.SetBool ("run", true);
			} else {
				animator.SetBool ("run", false);
			}
			if (this.isGrounded) {
				animator.SetBool ("jump", false);
			} else {
				animator.SetBool ("jump", true);
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (!isDead) {
			float value = Input.GetAxis ("Horizontal");
			if (Mathf.Abs (value) > 0) {
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}
			if (Input.GetButton ("Jump") && isGrounded) {
				this.JumpActive = true;
			}
			if (this.JumpActive) {
				if (Input.GetButton ("Jump")) {
					this.JumpTime += Time.deltaTime;
					if (this.JumpTime < this.MaxJumpTime) {
						Vector2 vel = myBody.velocity;
						vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
						myBody.velocity = vel;
					}
				} else {
					this.JumpActive = false;
					this.JumpTime = 0;
				}
			}
			if (value < 0) {
				sr.flipX = true;
			} else if (value > 0) {
				sr.flipX = false;
			}
		}
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");

		RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);

		if (hit) {
			//Перевіряємо чи ми опинились на платформі
			if(hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				Utility.SetNewParent(this.transform, hit.transform);
			}
			isGrounded = true;
		} else {
			isGrounded = false;
			//Ми в повітрі відліпаємо під платформи
			Utility.SetNewParent(this.transform, this.heroParent);
		}

		Debug.DrawLine (from, to, Color.red);
	}

	public void makeBigger(){
		if (!isBig) {
			this.transform.localScale += new Vector3 (bigScale, bigScale, 0);
			isBig = true;
		}
	}

	public void makeSmaller(){
		if (isBig) {
			sr.color = Color.red;
			isDamaged = true;
			normalizeScale ();
			Invoke ("removeDamaged", damageTime);
		} else if(!isDead){
			die ();
		}
	}

	void removeDamaged(){
		isDamaged = false;
		sr.color = normColor;
	}

	public void normalizeScale(){
		this.transform.localScale = normScale;
		isBig = false;
	}

	public void die (){
		isDead = true;
		Vector2 vel = myBody.velocity;
		vel.x /= 2;
		myBody.velocity = vel;
		animator.SetTrigger ("dead");
	}

	public void respawn(){
		LevelController.current.onRabitDeath (this);
	}

	public void Jump() 
	{
		StartCoroutine(JumpCoroutine());
	}

	private IEnumerator JumpCoroutine()
	{
		float jumpTime = 0;
		while (true)
		{
			jumpTime += Time.deltaTime;
			if (jumpTime < 1f)
			{
				myBody.velocity = new Vector2(myBody.velocity.x, JumpSpeed * (1.0f - jumpTime / 1f));
				yield return null;
			} else {
				isGrounded = true;
				jumpTime = 0;
				break;
			}
		}
	}
}