using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Vector3 targetPosition;
	public Transform targetTransform;
	public float smoothing = 5f;
	public Vector3 deltaPosition = Vector3.zero;
	float yOffset;

	// Use this for initialization
	void Start () 
	{
		yOffset = transform.position.y;
	}

	public void MoveTo(float x, float z)
	{
		targetPosition.Set (x, yOffset, z);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (targetTransform != null)
		{
			targetPosition = targetTransform.position;
		}
		deltaPosition = targetPosition - transform.position;
		if (Mathf.Abs(deltaPosition.magnitude) > 0.1)
		{
			transform.position = Vector3.Lerp( transform.position, targetPosition, smoothing * Time.deltaTime);
		}
		else
		{
			transform.position = targetPosition;
			deltaPosition = Vector3.zero;
		}


	}
}
