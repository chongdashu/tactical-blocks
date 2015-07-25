using UnityEngine;
using System.Collections;

public class TileSelector : MonoBehaviour {

	public int floorMask;
	public float cameraRayLength = 1000f;

	public Camera gameCamera;
	public Vector3 mousePosition;
	public Ray cameraRay;
	
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
	
	// Update is called once per frame
	void Update () 
	{
		// Get mouse's screen position.
		mousePosition = Input.mousePosition;

		// Get ray from screen point.
		cameraRay = gameCamera.ScreenPointToRay(mousePosition); // world space

		// Check if ray can hit the floor
		RaycastHit floorHit;	


		GameObject currentlySelectedTile = null;

		if (Physics.Raycast(cameraRay, out floorHit, cameraRayLength, floorMask))
		{

			currentlySelectedTile = floorHit.transform.gameObject;
			Vector3 dir = cameraRay.direction * cameraRayLength;

			Debug.DrawRay(gameCamera.transform.position + 5*cameraRay.direction, dir, Color.red);

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
				print ("wop");
				// Let's reset to old selected tile's position first.
				Vector3 position = selectedTile.transform.position;
				position.y = -0.5f;
				selectedTile.transform.position = position;
				selectedTile = null;
			}

		}
	

	}


}
