using UnityEngine;
using System.Collections;

public class TileSelector : MonoBehaviour {

	public int floorMask;
	public float cameraRayLength = 1000f;
	public LevelConstructor levelConstructor;

	public Camera gameCamera;
	public Vector3 mousePosition;
	public Vector3 mouseWorldPosition;
	public Ray cameraRay;

	public Vector3 dir;
	public Vector3 dir1;
	public Vector3 cameraOrigin;
	public Vector3 cameraDirection;
	public LevelTile selectedTile = null;

	public System.Action<GameObject> OnTileSelectionCallback;


	// Use this for initialization
	void Start () 
	{
		if (gameCamera == null) 
		{
			gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		}

		if (levelConstructor == null)
		{
			levelConstructor = GameObject.Find ("LevelConstructor").GetComponent<LevelConstructor>();
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


		cameraOrigin = cameraRay.origin;
		cameraDirection = cameraRay.direction;

		// Check if ray can hit the floor
		RaycastHit floorHit;	

		LevelTile currentlySelectedTile = null;

		if (Physics.Raycast(cameraRay, out floorHit, cameraRayLength, floorMask))
		{
			currentlySelectedTile = floorHit.transform.gameObject.GetComponent<LevelTile>();
			dir = cameraRay.direction * cameraRayLength;
//			Vector3 pos = gameCamera.transform.position;
//			Vector3 target = pos + dir;
			dir1 = currentlySelectedTile.transform.position - gameCamera.transform.position;

			Debug.DrawRay(currentlySelectedTile.transform.position, -dir1, Color.green);
//			Debug.DrawRay(gameCamera.transform.position, dir, Color.blue);
			Debug.DrawRay(cameraRay.origin, dir,  Color.white);
//			Debug.DrawRay(currentlySelectedTile.transform.position, Vector3.up*cameraRayLength, Color.yellow);
//			Debug.DrawLine(currentlySelectedTile.transform.position, 
//			               currentlySelectedTile.transform.position + Vector3.up*cameraRayLength, 
//			               Color.magenta);

			if (selectedTile != null)
			{
				if (!GameObject.Equals(currentlySelectedTile, selectedTile)) 
				{
					// If the last selected tile is not the same tile as now,
					// let's reset to old selected tile's position first.
					Vector3 position = selectedTile.transform.position;
					position.y = selectedTile.y - selectedTile.size.y/2;
					selectedTile.transform.position = position;
				}
			}

			// Let's raise the selected tile now.
			Vector3 newPosition = currentlySelectedTile.transform.position;
			newPosition.y = currentlySelectedTile.y - currentlySelectedTile.size.y/2 + 0.25f;
			currentlySelectedTile.transform.position = newPosition;

			selectedTile = currentlySelectedTile;

		}
		else
		{
			if (selectedTile != null)
			{
				// Let's reset to old selected tile's position first.
				Vector3 position = selectedTile.transform.position;
				position.y = selectedTile.y - selectedTile.size.y/2;
				selectedTile.transform.position = position;
				selectedTile = null;
			}

		}

		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log ("OnMouseButtonUp()");
			if (selectedTile != null)
			{
				if (OnTileSelectionCallback != null)
				{
					OnTileSelectionCallback(selectedTile.gameObject);
				}
			}
		}
	

	}


}
