using UnityEngine;
using System.Collections;

public class Subplane : MonoBehaviour {

	public GameObject[] bearings; 
	public Rigidbody[] bearingBodies;
	public MeshFilter meshFilter;


	// Use this for initialization
	void Awake () 
	{
		meshFilter = this.GetComponent<MeshFilter>();
	}

	public void setBearings(GameObject[] bearings)
	{
		this.bearings = bearings;

		bearingBodies = new Rigidbody[this.bearings.Length];
		for (int i=0; i < bearings.Length; i++)
		{
			this.bearingBodies[i] = bearings[i].GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (bearings != null)
		{
			Vector3[] newVertices = new Vector3[bearingBodies.Length];
			for (int i=0; i < bearingBodies.Length; i++)
			{
//				newVertices[i] = meshFilter.mesh.vertices[i];
//				newVertices[i].z = bearingBodies[i].transform.position.;
//				newVertices[i].z = meshFilter.mesh.vertices[i].z;
				newVertices[i] = bearingBodies[i].transform.position - this.transform.position;
			}

			meshFilter.mesh.vertices = newVertices;

			meshFilter.mesh.RecalculateNormals();
			meshFilter.mesh.RecalculateBounds();
		}
	}
}
