using UnityEngine;
using System.Collections;

public class CubeSoftie : MonoBehaviour {

	public GameObject bearingPrefab;
	public GameObject core;

	public float cubeLength = 1.0f;
	public float bearingsPerAxis = 4.0f;
	public int numberOfAxes = 1;
	public GameObject[] axisContainers;
	public Vector3 startingVelcocity = Vector3.zero;

	void Awake () 
	{
		core.GetComponent<Rigidbody>().velocity = startingVelcocity;
		axisContainers = new GameObject[numberOfAxes];
		for (float a=0; a < numberOfAxes; a++)
		{
			GameObject axisContainer = new GameObject();
			axisContainer.transform.parent = this.transform;
			axisContainer.name = "AxisContainer_" + a;
			axisContainers[(int)a] = axisContainer;

			float t = 0;
			for (float i=0; i < bearingsPerAxis; i++)
			{
				for (float j=0; j < bearingsPerAxis; j++)
				{
//					float angle = 2 * Mathf.PI * (i/bearingsPerAxis);
					//				float angleDeg = Mathf.Rad2Deg *  (angle);
					//			Debug.Log ("i=" + i + ", angleDeg= " + angleDeg);


//					Debug.Log ("cubeLength=" + cubeLength);
					float posX = Mathf.Lerp (-cubeLength/2, cubeLength/2, (j)/(bearingsPerAxis-1));
					float posY = Mathf.Lerp (-cubeLength/2, cubeLength/2, (i)/(bearingsPerAxis-1));
//					Debug.Log ("t=" + t + ", posX=" + posX + ", posY=" + posY);
					//			Debug.Log ("i=" + i + ", xPos=" + posX + ", yPos=" + posY);
					
					Vector3 corePos = core.transform.position;
					
					posX = core.transform.position.x + posX;
					posY = core.transform.position.y + posY;
					
					GameObject bearingObject = Instantiate(bearingPrefab);
					bearingObject.name = "bearing_" + t;
					Vector3 bearingPosition = new Vector3( posX, posY, corePos.z);
					bearingObject.transform.position = bearingPosition;
					bearingObject.transform.parent = axisContainer.transform;

					if (j>=1)
					{
						SpringJoint joint = bearingObject.AddComponent<SpringJoint>();
						//				joint.autoConfigureConnectedAnchor = true;
						//				joint.axis = axisContainer.transform.rotation.eulerAngles;
						joint.axis = Vector3.one;
						
						joint.connectedBody = axisContainer.transform.GetChild((int)(t)-1).gameObject.GetComponent<Rigidbody>();
						//
						joint.enablePreprocessing = true;
						
						joint.breakForce = Mathf.Infinity;
						joint.breakTorque = Mathf.Infinity;
						
						joint.anchor = Vector3.zero;
						joint.connectedAnchor = Vector3.zero;
					}
					t++;
				}


				
			}
			float f = a/(numberOfAxes-1);
			float posZ = Mathf.Lerp (-cubeLength/2, cubeLength/2, a/(numberOfAxes-1));
			Debug.Log ("a=" + a + "/" + numberOfAxes + ", posZ=" + posZ + ", f=" + f);
			axisContainer.transform.position = new Vector3(0f, 0f, posZ);
			
		}

		for (int i=0; i < axisContainers.Length; i++)
		{
			GameObject axisContainer = axisContainers[i];
			for (int j=0; j < axisContainer.transform.childCount; j++)
			{
				GameObject bearingObject = axisContainer.transform.GetChild(j).gameObject;
				SpringJoint joint = bearingObject.AddComponent<SpringJoint>();
				//				joint.autoConfigureConnectedAnchor = true;
				//				joint.axis = axisContainer.transform.rotation.eulerAngles;
				joint.axis = Vector3.one;
				
				joint.connectedBody = core.GetComponent<Rigidbody>();
				//
				joint.enablePreprocessing = true;
				
				joint.breakForce = Mathf.Infinity;
				joint.breakTorque = Mathf.Infinity;
				
				joint.anchor = Vector3.zero;
				joint.connectedAnchor = Vector3.zero;
			}
		}
	}
}
	