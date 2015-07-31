using UnityEngine;
using System.Collections;

public class FreeMovement : MonoBehaviour {

	public float movementSpeed = 4.0f;
	public int floorMask;
	public Vector3 movementVector;
	public Vector3 normalizedMovementVector;
	public Rigidbody rigidBody;


	
	// Use this for initialization
	void Awake () 
	{
		floorMask = LayerMask.GetMask("TileGround");
		rigidBody = GetComponent<Rigidbody>();
		movementVector = Vector3.zero;
		normalizedMovementVector = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float h = Input.GetAxisRaw("Horizontal"); 	// values {-1, 0, +1}
		float v = Input.GetAxisRaw("Vertical");		// values {-1, 0, +1}



		DoMove(h, v);
	}

	void DoMove(float h, float v)
	{
		if (h == 0 && v == 0) 
		{
			movementVector.Set(0f,0f,0f);
		}
		else
		{
			movementVector = Camera.main.transform.forward;
		}

		if (v != 0)
		{
			movementVector.Set(movementVector.x*v, 0f, movementVector.z*v);
		}
		if (h != 0)
		{
			movementVector = Quaternion.AngleAxis(90*h, Vector3.up) * movementVector;
		}
		normalizedMovementVector = movementVector.normalized * movementSpeed * Time.deltaTime;
		rigidBody.MovePosition(transform.position + normalizedMovementVector);
	}
}
