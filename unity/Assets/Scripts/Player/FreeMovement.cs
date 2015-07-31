using UnityEngine;
using System.Collections;

public class FreeMovement : MonoBehaviour {

	public float movementSpeed = 4.0f;
	public int floorMask;
	public Vector3 movementVector;
	public Rigidbody rigidBody;


	
	// Use this for initialization
	void Awake () 
	{
		floorMask = LayerMask.GetMask("TileGround");
		rigidBody = GetComponent<Rigidbody>();
		movementVector = Vector3.zero;
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
		movementVector.Set(h, 0, v);
		movementVector = movementVector.normalized * movementSpeed * Time.deltaTime;
		rigidBody.MovePosition(transform.position + movementVector);
	}
}
