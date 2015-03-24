using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public float dampTime =0.5f;
	public Transform target;
	private Vector3 velocity = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		if (target) {
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f,0.2f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position,destination,ref velocity, dampTime);
		
		}
	}
}
