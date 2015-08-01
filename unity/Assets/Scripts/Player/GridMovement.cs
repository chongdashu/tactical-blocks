using UnityEngine;
using System.Collections;

public class GridMovement : BaseMovement {

	public Vector3 tempVector3;
	public GameObject tempObj = null;

	public GameObject tileInDirection;
	public GameObject targetTile;
	public float tileSnapSensitivity = 0.755f;

	// Use this for initialization
	override protected void Awake () 
	{
		base.Awake();
		Debug.Log ("[<b>GridMovement</b>], OnAwake()");

		tempVector3 = Vector3.zero;

	}

	override protected void DoMove(float h, float v)
	{

		if (v != 0 || h != 0)
		{
			tileInDirection = GetTileInDirection(h, v);
			if (tileInDirection != null)
			{
				// There is something in front, continue to move to it.
				movementVector.Set (h, 0f, v);
				normalizedMovementVector = movementVector.normalized * movementSpeed * Time.deltaTime;
				rigidBody.MovePosition(transform.position + normalizedMovementVector);

				targetTile = tileInDirection;

				tempVector3 = Vector3.Lerp (transform.position, tileInDirection.transform.position, movementSpeed*Time.deltaTime);
				rigidBody.MovePosition(tempVector3);
			}
			else
			{	
				// Nothing else in front.
				if (targetTile != null)
				{
					if ((targetTile.transform.position - transform.position).magnitude < tileSnapSensitivity) 
					{
						// we're almost at our target destination, stop.
						rigidBody.MovePosition(targetTile.transform.position);
						rigidBody.transform.position = new Vector3(targetTile.transform.position.x, transform.position.y, targetTile.transform.position.z);
						targetTile = null;
					}
					else
					{
						tempVector3 = (targetTile.transform.position - transform.position);
						tempVector3 = Vector3.Lerp (transform.position, targetTile.transform.position, (movementSpeed*Time.deltaTime)/tempVector3.magnitude);
						rigidBody.MovePosition(tempVector3);
					}
				}
			}
		}

		if (h == 0 && v == 0)
		{
			if (targetTile != null)
			{
				if ((targetTile.transform.position - transform.position).magnitude < tileSnapSensitivity) 
				{
					// we're almost at our target destination, stop.
					rigidBody.MovePosition(targetTile.transform.position);
					rigidBody.transform.position = new Vector3(targetTile.transform.position.x, transform.position.y, targetTile.transform.position.z);
					targetTile = null;
				}
				else
				{
					tempVector3 = (targetTile.transform.position - transform.position);
					tempVector3 = Vector3.Lerp (transform.position, targetTile.transform.position, (movementSpeed*Time.deltaTime)/tempVector3.magnitude);
					rigidBody.MovePosition(tempVector3);
				}
			}
		}
	}

	private GameObject GetTileInDirection(float h, float v)
	{
		if (tileBelow != null)
		{
			// We are on the ground...
			tempVector3.Set (h, 0f, v);
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
