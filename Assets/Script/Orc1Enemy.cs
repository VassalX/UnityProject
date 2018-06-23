using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc1Enemy : MonoBehaviour {

	public enum Mode{
		GoToA,
		GoToB,
		Attack,
		Dead
	}

	public Vector3 pointA;
	public Vector3 pointB;
	public float speed;

	protected Vector3 my_pos;
	protected Vector3 rabit_pos;

	protected Mode mode = Mode.GoToA;

	protected Rigidbody2D myBody;
	protected Animator myController;

	protected Animator animator = null;
	protected SpriteRenderer sr = null;

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		rabit_pos = HeroRabit.lastRabit.transform.position;
		my_pos = this.transform.position;
		myBody = this.GetComponent<Rigidbody2D> ();
		myController = this.GetComponent<Animator> ();
		rabit_pos = HeroRabit.lastRabit.transform.position;
		animator = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		Physics2D.IgnoreLayerCollision (11, 11);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		rabit_pos = HeroRabit.lastRabit.transform.position;
		my_pos = this.transform.position;
		if (mode != Mode.Dead) {
			updateMode ();
			float value = getDirection ();
			changeRunning (value);
			changeDirection (value);
		}
	}

	protected float getDirection(){
		if (mode == Mode.GoToA) {
			if (my_pos.x < pointA.x) {
				return 1;
			} else {
				return -1;
			}
		} else if (mode == Mode.GoToB) {
			if (my_pos.x < pointB.x) {
				return 1;
			} else {
				return -1;
			}
		} else if (mode == Mode.Attack) {
			if (my_pos.x < rabit_pos.x) {
				return 1;
			} else {
				return -1;
			}
		}
		return 0;
	}

	protected void changeDirection(float direction){
		if (direction > 0) {
			sr.flipX = true;
		} else if (direction < 0) {
			sr.flipX = false;
		}
	}

	protected virtual void changeVelocity(float value){
		Vector2 vel = myBody.velocity;
		vel.x = value * speed;
		myBody.velocity = vel;
	}

	protected virtual void changeRunning(float value){
		changeVelocity (value);
		if (Mathf.Abs (value) > 0) {
			this.myController.SetBool ("run", true);
		} else {
			this.myController.SetBool ("run", false);
		}
	}

	protected virtual void updateMode(){
		if (mode != Mode.Attack 
			&& rabit_pos.x > Mathf.Min (pointA.x, pointB.x) 
			&& rabit_pos.x < Mathf.Max (pointA.x, pointB.x)) 
		{
			Debug.Log ("Mode changed to Attack");
			mode = Mode.Attack;
		} else if (mode == Mode.GoToA) {
			if (isArrived (pointA)) {
				Debug.Log ("Mode changed to B");
				mode = Mode.GoToB;
			}
		} else if (mode == Mode.GoToB) {
			if (isArrived (pointB)) {
				Debug.Log ("Mode changed to A");
				mode = Mode.GoToA;
			}
		}
	}

	protected bool isArrived(Vector3 point){
		return Mathf.Abs(my_pos.x - point.x) < myBody.velocity.magnitude/60;  
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (this.isActiveAndEnabled)
		{
			HeroRabit rabit = collision.gameObject.GetComponent<HeroRabit>();
			if (rabit != null){
				this.OnRabitHit(rabit);
			}
		}
	}

	protected void OnRabitHit(HeroRabit rabit) 
	{
		Vector3 v = rabit.transform.position - transform.position;
		float angle = Mathf.Atan2(v.y, v.x) / Mathf.PI * 180;
		if (angle > 60f && angle < 150f) 
		{
			//Debug.Log ("kill orc");
			rabit.Jump();
			Die();
		} else {
			//Debug.Log ("kill rabit");
			animator.SetTrigger("attack");
			if(!rabit.isDamaged)
				rabit.makeSmaller ();
		}
	}

	protected IEnumerator DieCoroutine()
	{
		mode = Mode.Dead;
		changeVelocity (myBody.velocity.x / 4);
		animator.SetTrigger("death");
		yield return new WaitForSeconds(1f);
		Destroy(this.gameObject);
	}

	public void Die()
	{
		StartCoroutine(DieCoroutine());
	}
}
