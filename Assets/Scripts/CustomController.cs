using UnityEngine;
using System.Collections;

public class CustomController : MonoBehaviour {


public Animator anim;
public float speed = 0.8f;
public float jumpPower = 200;

private bool jump1 = true;
private bool jump2 = true;
private bool right = true;
public GameObject hitbox;

	// Use this for initialization
	void Start () {
	
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis("Horizontal");
		anim.SetFloat("Movement", Mathf.Abs(move));
	
		//face right or left
		if (move < 0) {
			Vector3 newScale = transform.localScale;
			newScale.x = -1.0f;
			transform.localScale = newScale;
			right = false;
		}
		else if (move > 0) {
			Vector3 newScale = transform.localScale;
			newScale.x = 1.0f;
			transform.localScale = newScale;
			right = true;
		}
		move *= Time.deltaTime*speed;
		this.transform.position += new Vector3(move,0.0f,0.0f);
	
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (jump1) {
				anim.SetTrigger("Jump");
				rigidbody2D.AddForce(transform.up * jumpPower);
				jump1 = false;
			} else if (jump2) {
				//anim.SetTrigger("Jump");
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0);
				
				rigidbody2D.AddForce(transform.up * jumpPower);
				jump2 = false;
			}
		}
	
		if (Input.GetKeyDown(KeyCode.Q) && jump1 && move ==0) {
			readyHit();
		
		}
	
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
		hitbox.collider2D.enabled= true;
	}
	
	public void EndHit() 
	{
		hitbox.collider2D.enabled= false;
	}
	
	
	void OnCollisionEnter2D(Collision2D crash) {
	
	
		if (crash.gameObject.tag == "Floor") { 
		
		if (!jump1) {
				anim.SetTrigger("Floor");
				jump1 = true;
				jump2 = true;
			}
		}
	  }
	
}
