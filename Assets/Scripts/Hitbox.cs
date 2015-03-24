using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {
	public float hitpower = 200;
	public Vector3 direction;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Item") 
		{
			other.GetComponent<Rigidbody2D>().AddForce(direction * hitpower);
		}

	}
	
	
	
}
