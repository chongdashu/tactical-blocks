using UnityEngine;
using System.Collections;

public class LevelConstructor : MonoBehaviour {

	public GameObject[] tilePrefabArray;	
	public GameObject groundContainer;
	public int levelTileWidth = 7;
	public int levelTileLength = 5;
	public float levelTileHeight = 0.5f;
	public string defaultMap = "";
	
	// Use this for initialization
	void Start () 
	{
		defaultMap = 
			"1212121" +
			"2121212" +
			"1212121" +
			"2121212" +
			"1212121";

		groundContainer = GameObject.Find("_Ground");
		if (!groundContainer) 
		{
			// TODO: Warning message here.
		}

		for (int z=0; z < levelTileLength; z++)
		{
			for (int x=0; x < levelTileWidth; x++) 
			{
				int tileIndex = z*levelTileWidth + x;
				int tileType = 0;

				int.TryParse(defaultMap.Substring(tileIndex,1), out tileType);
				if (tileType > 0)
				{
					tileType -= 1; // account for empty tile at index 0;
					GameObject tile = Instantiate(tilePrefabArray[tileType]);
					tile.transform.localPosition = new Vector3(x, -levelTileHeight/2,-z); 
					tile.transform.parent = groundContainer.transform;
					tile.name = "Tile_" + x + "_" + Mathf.Abs(z);
				}
			}
		}
	}

	public float GetGroundY() 
	{
		return levelTileHeight;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
