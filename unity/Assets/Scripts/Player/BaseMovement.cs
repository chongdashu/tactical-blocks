using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour {

	public float movementSpeed = 4.0f;
	public int floorMask;
	public Vector3 movementVector;
	public Vector3 normalizedMovementVector;
	public Rigidbody rigidBody;
	public Ray floorRay;
	public RaycastHit floorHit;
	public float floorRayLength = 1.0f;
	public GameObject tileBelowObject;
	public LevelTile tileBelow;


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
	void FixedUpdate () 
	{
		UpdateTileBelow();

		float h = Input.GetAxisRaw("Horizontal"); 	// values {-1, 0, +1}
		float v = Input.GetAxisRaw("Vertical");		// values {-1, 0, +1}
		UpdateTileBelow();

		DoMove(h, v);
	}

	private void UpdateTileBelow()
	{
		floorRay = new Ray(this.transform.position, Vector3.down);
		if (Physics.Raycast(floorRay, out floorHit, floorRayLength, floorMask))
		{
			tileBelowObject = floorHit.collider.gameObject;
			tileBelow = tileBelowObject.GetComponent<LevelTile>();

			Debug.DrawRay(this.transform.position, floorRay.direction, Color.cyan);
		}
		else
		{
			tileBelowObject = null;
			tileBelow = null;
		}

	}

	protected virtual void DoMove(float h, float v)
	{

	}
}
