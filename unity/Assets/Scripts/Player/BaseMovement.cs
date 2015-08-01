using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour {

	public float movementSpeed = 4.0f;
	public int floorMask;
	public Vector3 movementVector;
	public Vector3 normalizedMovementVector;
	public Rigidbody rigidBody;

	// Use this for initialization
	protected virtual void Awake () 
	{
		Debug.Log ("[<color=red>BaseMovement</color>], OnAwake()");
		floorMask = LayerMask.GetMask("TileGround");
		rigidBody = GetComponent<Rigidbody>();
		movementVector = Vector3.zero;
		normalizedMovementVector = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
