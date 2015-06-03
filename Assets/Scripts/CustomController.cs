﻿using UnityEngine;
using System.Collections;

public class CustomController : MonoBehaviour {


public Animator anim;
public float speed = 0.8f;
public float jumpPower = 200;

private bool onAir = true;
private bool dJump = false;
private bool Guard = false;

public bool right = true;
public GameObject hitbox;

	// Use this for initialization
	void Start () {
	
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		float yin = Input.GetAxis("Vertical");
		
		if (yin < 0) {
			anim.SetBool("Guard", true);
			Guard = true;
		} else if (Guard) {
			anim.SetBool("Guard",false);
			Guard = false;
		}
	
		
	
	
		float move = Input.GetAxis("Horizontal");
		if (Guard) move = 0;
		anim.SetFloat("Movement", Mathf.Abs(move));
	
		//face right or left
		if (move != 0) {
			Vector3 newScale = transform.localScale;
			if (move < 0) {
				newScale.x = -1.0f;
				right = false;
			}
			else {
				newScale.x = 1.0f;
				right = true;
			}
			transform.localScale = newScale;
			
			move *= Time.deltaTime*speed;
			this.transform.position += new Vector3(move,0.0f,0.0f);
		}

	
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (!onAir) {
				anim.SetBool("onAir",true);
				GetComponent<Rigidbody2D>().AddForce(transform.up * jumpPower);
				onAir = true;
			} else if (!dJump) {
				//anim.SetTrigger("Jump");
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);
				GetComponent<Rigidbody2D>().AddForce(transform.up * jumpPower);
				dJump = true;
			}
		}
	
		if (Input.GetKeyDown(KeyCode.Q) && !onAir ) {
			doBazooka();
		
		}
	
	}
	
	
	private void doBazooka()
	{
		anim.SetTrigger("DoBazooka");
	
	}
	
	
	private void readyHit() 
	{
		int r = 1;
		if (!right) r =-1; 
		hitbox.GetComponent<Hitbox>().direction = r*this.transform.right + this.transform.up*3f;
		anim.SetTrigger("Attack");
	}
	
	public void Hit() 
	{
		hitbox.GetComponent<Collider2D>().enabled= true;
	}
	
	public void EndHit() 
	{
		hitbox.GetComponent<Collider2D>().enabled= false;
	}
	
	
	void OnCollisionEnter2D(Collision2D crash) {
		if (crash.gameObject.tag == "Floor") { 		
			if (onAir) {
				anim.SetBool("onAir",false);
				onAir = false;
				dJump = false;
			}
		}
	  }
	
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor") { 		
				anim.SetBool("onAir",true);
				onAir = true;
				dJump = false;
		}
	
	}
	
}
