using UnityEngine;
using System.Collections;

public class KameBehavior : MonoBehaviour {

	public enum trayectory {orbit , spyrog , back, nothing , gotohell};
	public trayectory selected;	
	
	public GameObject mainCharacter;
	
	// Use this for initialization
	void Start () {
	
	}
	
	float t = 0.0f;
	float lx = 0.0f;
	float ly = 1.0f;
	float dt = 0.02f;
	float dl = 0.02f; //Lerp Steep
	
	Vector3 targetPos;
	
	Vector3 startpos, endpos;
 	// Update is called once per frame
	void Update () {
		targetPos = mainCharacter.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
	
	
		if (Input.GetKeyDown(KeyCode.X) && selected != trayectory.back) {
			startpos = this.transform.position;
			endpos = targetPos;
			selected = trayectory.back;
			lv = 0f;
		}
	
		switch (selected) {
			case trayectory.orbit: 
			Oribt();
			break;
			case trayectory.spyrog:
			Spirograph();
			break;
			case trayectory.back:
			GoBack();
			break;
			case trayectory.gotohell:
			GoForward();
			break;
			
			
			default:
			break;
		
		}
	
		
		t += dt;
		transform.position = targetPos + new Vector3(lx,ly,0.0f);
		
	}
	
	float R = 2f;
	float r = 0.5f;
	float d = 2f;
	
	float lv = 0.0f;
	
	void GoForward() {
		Vector3 lerped = Vector3.Lerp(startpos,direction,lv);
		lx = lerped.x;
		ly = lerped.y;
		
		lv += dl;
		
		if (lv > 1f) selected = trayectory.spyrog;
	
	
	
	}
	
	void GoBack() {
		
		Vector3 lerped = Vector3.Lerp(startpos,endpos,lv);
		lx = lerped.x;
		ly = lerped.y;
		
		lv += dl;
	
		if (lv > 1f) selected = trayectory.nothing;
	
	}
	
	
	void Spirograph() {
		lx = (R-r)*Mathf.Cos(t) + d*Mathf.Cos(t*R-r/r) ;
		ly = (R-r)*Mathf.Sin(t) - d*Mathf.Sin(t*R-r/r) ;
		
	
	}
	
	void Oribt () {
		lx = 0.7f* Mathf.Cos(t);
		ly = 0.7f* Mathf.Sin(t);
	}
	
	Vector3 direction;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Hitbox") {
			bool right = mainCharacter.GetComponent<CustomController>().right;
			int r = 1;
			if (!right) r =-1; 
			 direction = r*this.transform.right + this.transform.up*3f;
			 //direction = direction;
			 startpos = this.transform.position;
			selected = trayectory.gotohell;
			lv = 0f;
		}
	}
	
	
		
}









