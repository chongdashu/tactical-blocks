using UnityEngine;
using System.Collections;

public class Softie : MonoBehaviour {

	public GameObject bearingPrefab;
	public GameObject core;
	public float bearingsPerAxis = 4;
	public float bearingDistance = 1.0f;
	public int numberOfAxes = 1;
	public GameObject[] axisContainers;
	public Vector3 startingVelcocity = Vector3.zero;


	void Awake () 
	{
		core.GetComponent<Rigidbody>().velocity = startingVelcocity;
		axisContainers = new GameObject[numberOfAxes];
		for (int a=0; a < numberOfAxes; a++)
		{
			GameObject axisContainer = new GameObject();
			axisContainer.transform.parent = this.transform;
			axisContainer.name = "AxisContainer_" + a;
			axisContainers[a] = axisContainer;

			for (float i=0; i < bearingsPerAxis; i++)
			{
				float angle = 2 * Mathf.PI * (i/bearingsPerAxis);
//				float angleDeg = Mathf.Rad2Deg *  (angle);
				//			Debug.Log ("i=" + i + ", angleDeg= " + angleDeg);
				float posX = bearingDistance * Mathf.Cos(angle);
				float posY = bearingDistance * Mathf.Sin(angle);
				
				//			Debug.Log ("i=" + i + ", xPos=" + posX + ", yPos=" + posY);
				
				Vector3 corePos = core.transform.position;
				
				posX += core.transform.position.x;
				posY += core.transform.position.y;
				
				GameObject bearingObject = Instantiate(bearingPrefab);
				Vector3 bearingPosition = new Vector3( posX, posY, corePos.z);
				bearingObject.transform.position = bearingPosition;
				bearingObject.transform.parent = axisContainer.transform;


		
			}
			axisContainer.transform.RotateAround(core.transform.position, Vector3.up, a*180f/numberOfAxes);
			                                   
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
