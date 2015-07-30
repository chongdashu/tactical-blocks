using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelConstructor : MonoBehaviour {

	public CameraMovement cameraMovement;

	public GameObject[] tilePrefabArray;	
	public float[] prefabHeights;
	public GameObject groundContainer;
	public int levelTileWidth = 7;
	public int levelTileLength = 5;
	public float groundY = 0f;
	public string defaultMap = "";
	public List<string> map = new List<string>();

	void Awake()
	{

	}

	// Use this for initialization
	void Start () 
	{
		prefabHeights = new float[tilePrefabArray.Length];
		for (int i=0; i <tilePrefabArray.Length; i++) 
		{
			prefabHeights[i] = tilePrefabArray[i].GetComponent<MeshFilter>().sharedMesh.bounds.size.y;
		}

		defaultMap = 
			"1212121" +
			"2121212" +
			"1212121" +
			"2121212" +
			"1212121";

		map.Add(defaultMap);
//		map.Add (
//			"0000000" +
//		    "0000000" +
//		    "0000000" +
//		    "3434343" +
//			"4343434" 
//		);


		groundContainer = GameObject.Find("_Ground");
		if (!groundContainer) 
		{
			// TODO: Warning message here.
		}
		for (int m=0; m < map.Count; m++)
		{
			for (int z=0; z < levelTileLength; z++)
			{
				for (int x=0; x < levelTileWidth; x++) 
				{
					int tileIndex = z*levelTileWidth + x;
					int tileType = 0;
					
					int.TryParse(map[m].Substring(tileIndex,1), out tileType);
					if (tileType > 0)
					{
						tileType -= 1; // account for empty tile at index 0;
						GameObject tile = Instantiate(tilePrefabArray[tileType]);

						LevelTile levelTile = tile.GetComponent<LevelTile>();
						levelTile.x = x;
						levelTile.z = Mathf.Abs (z);
						levelTile.y = m;

						tile.transform.localPosition = new Vector3(x, m+levelTile.size.y/2, -z); 
						tile.transform.parent = groundContainer.transform;
						tile.name = "Tile_" + m + "_" + x + "_" + Mathf.Abs(z);




					}
				}
			}
		}


		if (cameraMovement == null)
		{
			cameraMovement = GameObject.Find ("GameCamera").GetComponent<CameraMovement>();
		}

		if (cameraMovement != null)
		{
			cameraMovement.MoveTo(( (float) levelTileWidth)/2, -((float)levelTileLength)/2);
		}


	}

	public float GetGroundY() 
	{
		return groundY;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
