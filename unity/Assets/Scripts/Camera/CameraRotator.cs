using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour {

	public float cameraVelocity = 10f;
	public float remainingRotation = 0f;
	public float timeStart = 0f;
	public float timeEnd = 0f;
	public float accumulatedRotation = 0f;
	public float targetRotation = 0f;
	public float timeRemaining = 0f;
	public float t;
	public float timeToRotate = 0.25f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Left
		if((Input.GetKeyDown(KeyCode.LeftArrow)))
		{
			targetRotation = -90f;
			accumulatedRotation = 0f;
			timeStart = Time.time;
			timeEnd = timeStart + timeToRotate;

		}
		// Right
		if((Input.GetKey(KeyCode.RightArrow)))
		{
			transform.Translate((Vector3.right * cameraVelocity) * Time.deltaTime);
		}
		// Up
		if((Input.GetKey(KeyCode.UpArrow)))
		{
			transform.Translate((Vector3.up * cameraVelocity) * Time.deltaTime);
		}
		// Down
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate((Vector3.down * cameraVelocity) * Time.deltaTime);
		}
	}

	void FixedUpdate()
	{
		if (targetRotation != 0)
		{
			remainingRotation = targetRotation - accumulatedRotation;
			if (Mathf.Approximately(remainingRotation, 0f))
			{
				targetRotation = 0f;
//				accumulatedRotation = 0f;
			}
			else
			{
				t = Mathf.Clamp((Time.time - timeStart) / timeToRotate, 0 , 1);
				float rotationAtThisTime = Mathf.Lerp(0, targetRotation, t);
				transform.Rotate(Vector3.up, rotationAtThisTime-accumulatedRotation);
				accumulatedRotation = rotationAtThisTime;

			}
		}



	}
}
