using UnityEngine;
using System.Collections;

public class GridMovement : BaseMovement {

	public Vector3 tempVector3;
	public GameObject tempObj = null;

	public GameObject tileInDirection;

	// Use this for initialization
	override protected void Awake () 
	{
		base.Awake();
		Debug.Log ("[<b>GridMovement</b>], OnAwake()");

		tempVector3 = Vector3.zero;

	}

	override protected void DoMove(float h, float v)
	{

		if (v != 0)
		{
			Debug.Log ("[<color=orange>GridMovement</color>], DoMove()");
			tileInDirection = GetTileInDirection(h, v);
			if (tileInDirection != null)
			{
				movementVector.Set (h, 0f, -v);
				normalizedMovementVector = movementVector.normalized * movementSpeed * Time.deltaTime;
				rigidBody.MovePosition(transform.position + normalizedMovementVector);

				tempVector3 = Vector3.Lerp (transform.position, tileInDirection.transform.position, movementSpeed*Time.deltaTime);
				rigidBody.MovePosition(tempVector3);
			}
		}

		if (h == 0 && v == 0)
		{
			if (tileInDirection != null)
			{
				tempVector3 = Vector3.Lerp (transform.position, tileInDirection.transform.position, movementSpeed*Time.deltaTime);
				rigidBody.MovePosition(tempVector3);
			}
		}
	}

	private GameObject GetTileInDirection(float h, float v)
	{
		if (tileBelow != null)
		{
			// We are on the ground...
			tempVector3.Set (h, 0f, -v);
			Ray ray = new Ray(tileBelow.transform.position, tempVector3);
			Debug.DrawRay(tileBelow.transform.position, tempVector3, Color.magenta);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 1.0f, floorMask))
			{
				return hit.collider.gameObject;
			}
		}

		return null;
	}
}
