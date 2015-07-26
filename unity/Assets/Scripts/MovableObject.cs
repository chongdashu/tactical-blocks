using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {

	LevelConstructor levelConstructor;
	TileSelector tileSelector;
	Rigidbody movableRigidBody;
	float speed = 5.0f;

	public Vector3 targetPosition = Vector3.zero;

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
		string tileName = tileObject.name.Replace("Tile_","");
		int x = int.Parse(tileName.Split ('_')[0]);
		int z = int.Parse(tileName.Split ('_')[1]);

		MoveTo (x, z);


	}

	public void MoveTo(int tileX, int tileZ)
	{
		Debug.Log ("[MovableObject], MoveTo(tileX=" + tileX + ", tileZ=" + tileZ);


		targetPosition.Set (tileX, transform.position.y, -Mathf.Abs(tileZ));

	}

	void FixedUpdate()
	{
		if (this.transform.position != this.targetPosition)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, this.speed * Time.deltaTime); 
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
