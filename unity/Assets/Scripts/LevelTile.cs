using UnityEngine;
using System.Collections;

public class LevelTile : MonoBehaviour {

	public int x;
	public int z;
	public int y;
	public Vector2 size;

	// Use this for initialization
	void Awake () {
		size = this.GetComponent<MeshFilter>().mesh.bounds.size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
