using UnityEngine;
using System.Collections;

public class TileSelector : MonoBehaviour {

	public int floorMask;
	public float cameraRayLength = 1000f;

	public Camera gameCamera;
	public Vector3 mousePosition;
	public Vector3 mouseWorldPosition;
	public Ray cameraRay;

	public Vector3 dir;
	public GameObject selectedTile = null;


	// Use this for initialization
	void Start () 
	{
		if (gameCamera == null) 
		{
			gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		}

		floorMask = LayerMask.GetMask("TileGround");
	}

	void OnDrawGizmos()
	{
		if (selectedTile != null)
		{
			Gizmos.color = Color.magenta;
//			Vector3 pos = gameCamera.transform.position - Vector3.up;
//			Gizmos.DrawLine(pos, pos + dir);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
//		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
//		Debug.DrawRay(transform.position, forward, Color.red);

		// Get mouse's screen position.
		mousePosition = Input.mousePosition;
//		mousePosition.z = gameCamera.nearClipPlane;
		mouseWorldPosition = gameCamera.ScreenToWorldPoint(mousePosition);

		// Get ray from screen point.
		cameraRay = gameCamera.ScreenPointToRay(mousePosition); // world space

		// Check if ray can hit the floor
		RaycastHit floorHit;	

		GameObject currentlySelectedTile = null;

		if (Physics.Raycast(cameraRay, out floorHit, cameraRayLength, floorMask))
		{
			currentlySelectedTile = floorHit.transform.gameObject;
			dir = cameraRay.direction * cameraRayLength;
			Vector3 pos = gameCamera.WorldToScreenPoint(gameCamera.transform.position);
			Vector3 target = gameCamera.WorldToScreenPoint(pos + dir);

//			Debug.DrawRay(pos, dir, Color.red);
			Debug.DrawLine(pos, target, Color.red);

			if (selectedTile != null)
			{
				if (!GameObject.Equals(currentlySelectedTile, selectedTile)) 
				{
					// Let's reset to old selected tile's position first.
					Vector3 position = selectedTile.transform.position;
					position.y = -0.5f;
					selectedTile.transform.position = position;
				}
			}

			// Let's raise the selected tile now.
			Vector3 newPosition = currentlySelectedTile.transform.position;
			newPosition.y = -0.25f;
			currentlySelectedTile.transform.position = newPosition;

			selectedTile = currentlySelectedTile;

		}
		else
		{
			if (selectedTile != null)
			{
				// Let's reset to old selected tile's position first.
				Vector3 position = selectedTile.transform.position;
				position.y = -0.5f;
				selectedTile.transform.position = position;
				selectedTile = null;
			}

		}
	

	}


}
