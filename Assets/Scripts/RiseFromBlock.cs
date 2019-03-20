using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseFromBlock : MonoBehaviour {

	
	public float smoothTime;
	public Vector2 newPos;


	void FixedUpdate()
	{
		transform.localPosition = Vector2.Lerp(transform.localPosition, newPos, Time.deltaTime * smoothTime);	
	}

	
}
