using UnityEngine;
using System.Collections;

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

		for (int i=0; i < planes.Length; i++)
		{
			Debug.Log ("Creating Plane: " + i);
			int l = (int) cubeSoftie.bearingsPerAxis;
			GameObject[] bearings = new GameObject[l*l];
			for (int j=0; j < planes[i].transform.childCount; j++)
			{
				bearings[j] = planes[i].transform.GetChild(j).gameObject;
			}
			GameObject submesh = CreatePlane(bearings);
			submesh.name = "submesh_" + i;
			submesh.transform.parent = this.transform;
		}

		// front
		// -----
		int verticesPerFace = cubeSoftie.bearingsPerAxis * cubeSoftie.bearingsPerAxis;

		   

	}

	GameObject getBearingAt(int axisIndex, int bearingIndex)
	{
		return planes[axisIndex].transform.GetChild (bearingIndex).gameObject;
	}

	GameObject CreatePlane(GameObject[] bearings)
	{
		int numberOfSubplanes = (int) ((cubeSoftie.bearingsPerAxis-1) * (cubeSoftie.bearingsPerAxis-1));

		GameObject container = new GameObject();
	


		float distanceBetweenBearings = (cubeSoftie.cubeLength/ (cubeSoftie.bearingsPerAxis-1))/2;
		float l = cubeSoftie.bearingsPerAxis;
		for (int i=0; i < numberOfSubplanes; i++)
		{

			int v1Index = (int) ((int)(i/(l-1))*l + (i%(l-1)));
			int v2Index = v1Index+1;
			int v3Index = (int) (v1Index + l+1);
			int v4Index = v3Index - 1;

			Debug.Log ("Creating Sub-Plane: " + i);
			Debug.Log ("|-v1Index: " + v1Index);

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
			m.triangles = new int[] {
				2,1,0,
				3,2,0
			};
			m.RecalculateNormals();

			MeshFilter mf = p.AddComponent<MeshFilter>();
			p.AddComponent<MeshRenderer>();
			mf.mesh = m;

			p.transform.parent = container.transform;
	

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
