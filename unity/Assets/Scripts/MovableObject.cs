using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {

	LevelConstructor levelConstructor;
	TileSelector tileSelector;
	Rigidbody movableRigidBody;
	float speed = 5.0f;

	public System.Action onMovementComplete;

	public Vector3 targetPosition = Vector3.zero;
	public float targetMagnitudeDelta;
	public float snappingDelta = 0.1f;

	void Awake () 
	{
		this.targetPosition = this.transform.position;
		this.movableRigidBody = this.GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () 
	{
		if (levelConstructor == null)
		{
			GameObject levelConstructorObj = GameObject.Find("LevelConstructor");
			if (levelConstructorObj == null)
			{
				// TODO: Some warning here.
			}

			if (levelConstructorObj != null)
			{
				levelConstructor = levelConstructorObj.GetComponent<LevelConstructor>();
			}

		}

		if (tileSelector == null)
		{
			tileSelector = GameObject.Find ("TileSelector").GetComponent<TileSelector>();
			tileSelector.OnTileSelectionCallback += OnTileSelected;
		}
	}

	void OnTileSelected(GameObject tileObject)
	{
		Debug.Log ("[MovableObjet] OnTileSelected()");
		LevelTile levelTile = tileObject.GetComponent<LevelTile>();

		string tileName = tileObject.name.Replace("Tile_","");
//		int x = int.Parse(tileName.Split ('_')[0]);
//		int z = int.Parse(tileName.Split ('_')[1]);
		int x = levelTile.x;
		int z = levelTile.z;

		Debug.Log ("Outer(), x=" + x + ", z=" + this.transform.position.z);
		MoveTo (x, (int) this.transform.position.z, delegate() { 
			Debug.Log ("Inner(), x=" + this.transform.position.x + ", z=" + z);
			MoveTo ((int) this.transform.position.x, z);

		});


	}

	public void MoveTo(int tileX, int tileZ, System.Action onComplete = null)
	{
		Debug.Log ("[MovableObject], MoveTo(tileX=" + tileX + ", tileZ=" + tileZ);
		targetPosition.Set (tileX, transform.position.y, -Mathf.Abs(tileZ));
		onMovementComplete = onComplete;

	}

	void FixedUpdate()
	{
		targetMagnitudeDelta = (this.transform.position-this.targetPosition).magnitude;
		if ( targetMagnitudeDelta > snappingDelta)
		{
			targetMagnitudeDelta = 0f;
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, this.speed * Time.deltaTime); 
		}
		else 
		{
			this.transform.position = targetPosition;
			if (onMovementComplete != null)
			{
				Debug.Log ("onMovementComplete != null");
				onMovementComplete();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
