using UnityEngine;
using System.Collections;
using System;

public class SoftieMesh : MonoBehaviour {

	public CubeSoftie cubeSoftie;
	public GameObject[] planes;
	

	// Use this for initialization
	void Start () 
	{
		GameObject child = null;
		int numberOfPlanes = 0;
		for (int i=0; i < cubeSoftie.transform.childCount; i++)
		{
			child = cubeSoftie.transform.GetChild(i).gameObject;
			if (child.name.Contains("AxisContainer"))
			{
				numberOfPlanes++;
			}
		}

		planes = new GameObject[numberOfPlanes];

		int planeIndex = 0;
		for (int i=0; i < cubeSoftie.transform.childCount; i++)
		{
			child = cubeSoftie.transform.GetChild(i).gameObject;
			if (child.name.Contains("AxisContainer"))
			{
				planes[planeIndex++] = child;
			}
		}

//		for (int i=0; i < planes.Length; i++)
//		{
//			Debug.Log ("Creating Plane: " + i);
//			int l = (int) cubeSoftie.bearingsPerAxis;
//			GameObject[] bearings = new GameObject[l*l];
//			for (int j=0; j < planes[i].transform.childCount; j++)
//			{
//				bearings[j] = planes[i].transform.GetChild(j).gameObject;
//			}
//			GameObject submesh = CreatePlane(bearings);
//			submesh.name = "submesh_" + i;
//			submesh.transform.parent = this.transform;
//		}

		int verticesPerFace = (int) cubeSoftie.bearingsPerAxis * (int) cubeSoftie.bearingsPerAxis;
		int verticesPerEdge = (int) cubeSoftie.bearingsPerAxis;
		GameObject[] bearings = null;

		// front
		// -----
		bearings = new GameObject[verticesPerFace];

		for (int i=0; i < verticesPerFace; i++)
		{
			bearings[i] = getBearingAt (0, i);
		}
		AddChildSubmesh(CreatePlane(bearings, verticesPerEdge, verticesPerEdge), "front");

		// back
		// ----
		bearings = new GameObject[verticesPerFace];
		for (int i=0; i < verticesPerFace; i++)
		{
			bearings[i] = getBearingAt (planes.Length-1, i);
		}
		AddChildSubmesh(CreatePlane(bearings, verticesPerEdge, verticesPerEdge, true), "back");

		// left
		bearings = new GameObject[verticesPerEdge * planes.Length];
		int b = 0;
		for (int i=planes.Length-1; i >=0; i--)
		{
			for (int j=0; j < verticesPerEdge; j++)
			{
//				Debug.Log ("b=" + b + ", i=" + i + ", j=" + j*verticesPerEdge);
				bearings[b++] = getBearingAt (i, j*verticesPerEdge);
			}
		}
		AddChildSubmesh(CreatePlane(bearings, numberOfPlanes, verticesPerEdge, true), "left");

		// right
		bearings = new GameObject[verticesPerEdge * planes.Length];
	 	int c = 0;
		for (int i=0; i < planes.Length; i++)
		{
			for (int j=0; j < verticesPerEdge; j++)
			{
				int index = (verticesPerEdge-1) + j*verticesPerEdge;
//				Debug.Log ("b=" + b + ", i=" + i + ", j=" + index);
				bearings[c++] = getBearingAt (i, index);
//				Debug.Log ("c, bearing=" + bearings[c-1]);
			}
		}
		AddChildSubmesh(CreatePlane(bearings, numberOfPlanes, verticesPerEdge, true), "right");

		// top
		bearings = new GameObject[verticesPerEdge * planes.Length];
		c = 0;
		for (int i=0; i < verticesPerEdge; i++)
		{
			for (int j=0; j < numberOfPlanes; j++)
			{
				int x = j;
				int y = verticesPerEdge*(verticesPerEdge-1) + i;

//				Debug.Log ("x=" + x + ", y=" + y);

				bearings[c++] = getBearingAt (x,y);
			}
		}
		AddChildSubmesh(CreatePlane(bearings, verticesPerEdge, numberOfPlanes, true), "top");

		// bottom
		bearings = new GameObject[verticesPerEdge * planes.Length];
		c = 0;
		for (int i=0; i < verticesPerEdge; i++)
		{
			for (int j=0; j < numberOfPlanes; j++)
			{
				int x = j;
				int y = verticesPerEdge-1 - i;
				
//				Debug.Log ("x=" + x + ", y=" + y);
				
				bearings[c++] = getBearingAt (x,y);
			}
		}
		AddChildSubmesh(CreatePlane(bearings, verticesPerEdge, numberOfPlanes, true), "bottom");

	}

	GameObject AddChildSubmesh(GameObject submesh, string name = null)
	{
		if (name != null)
		{
			submesh.name = name;
		}

		submesh.transform.parent = this.transform;

		return submesh;
	}



	GameObject getBearingAt(int axisIndex, int bearingIndex)
	{
		return planes[axisIndex].transform.GetChild (bearingIndex).gameObject;
	}

	GameObject CreatePlane(GameObject[] bearings, int width, int height, bool reversed=false)
	{
		int numberOfSubplanes = (width-1) * (height-1);
//		int numberOfSubplanes = (bearings.Length / cubeSoftie.numberOfAxes)

		Debug.Log ("numberOfSubplanes=" + numberOfSubplanes);

		GameObject container = new GameObject();

		float distanceBetweenBearings = (cubeSoftie.cubeLength/ (cubeSoftie.bearingsPerAxis-1))/2;
//		float l = cubeSoftie.bearingsPerAxis;
		int l = height;
		for (int i=0; i < numberOfSubplanes; i++)
		{

			int v1Index = (int) ((int)(i/(l-1))*l + (i%(l-1)));
			int v2Index = v1Index+1;
			int v3Index = (int) (v1Index + l+1);
			int v4Index = v3Index - 1;

//			Debug.Log ("v1Index=" + v1Index);
//			Debug.Log ("v2Index=" + v2Index);
//			Debug.Log ("v3Index=" + v3Index);
//			Debug.Log ("v4Index=" + v4Index);

//			Debug.Log ("Creating Sub-Plane: " + i);
//			Debug.Log ("|-v1Index: " + v1Index);

			GameObject bearing1 = bearings[v1Index].gameObject;
			GameObject bearing2 = bearings[v2Index].gameObject;
			GameObject bearing3 = bearings[v3Index].gameObject;
			GameObject bearing4 = bearings[v4Index].gameObject;

//			GameObject p = GameObject.CreatePrimitive(PrimitiveType.Plane);
			GameObject p = new GameObject("subplane_"+i);
 			
			p.transform.position = (bearing1.transform.position + bearing2.transform.position + 
			                        bearing3.transform.position + bearing4.transform.position) / 4;


			Mesh m = new Mesh();
			m.name = "softmesh_" + i;
			m.vertices = new Vector3[]{
				bearing1.transform.position - p.transform.position,
				bearing2.transform.position - p.transform.position,
				bearing3.transform.position - p.transform.position,
				bearing4.transform.position - p.transform.position
			};
			if (!reversed)
			{
				m.triangles = new int[] {
					2,1,0,
					3,2,0
				};
			}
			else 
			{
				m.triangles = new int[] {
					0,1,2,
					0,2,3
				};
			}

			m.RecalculateNormals();

			MeshFilter mf = p.AddComponent<MeshFilter>();
			p.AddComponent<MeshRenderer>();
			mf.mesh = m;

			p.transform.parent = container.transform;

			Subplane sp = p.AddComponent<Subplane>();
			sp.setBearings(new GameObject[]{
				bearing1,
				bearing2,
				bearing3,
				bearing4
			});
	

//			Vector3 scale = Vector3.one;
//			scale *= distanceBetweenBearings;
//			p.transform.localScale = scale;
//			p.transform.Rotate(Vector3.right, -90);

		}

		return container;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
