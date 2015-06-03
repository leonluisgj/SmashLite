using UnityEngine;
using System.Collections;

public class AnimationEventTestScript : MonoBehaviour
{
	public GUIText footstepText;
	
	public void OnFootStep(string side)
	{
		footstepText.text = "Footstep:" + side;
		Debug.Log("Play footstep sound now! send from animation side:" + side);
	}
}
