using UnityEngine;
using System.Collections;

public class Softie : MonoBehaviour {

	public GameObject bearingPrefab;
	public GameObject core;
	public float bearingsPerAxis = 4;
	public float bearingDistance = 1.0f;
	public int numberOfAxes = 1;


	// Use this for initialization
	void Awake () 
	{
		for (int a=0; a < numberOfAxes; a++)
		{
			GameObject axisContainer = new GameObject();
			axisContainer.transform.parent = this.transform;
			axisContainer.name = "AxisContainer_" + a;

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
			axisContainer.transform.Rotate(Vector3.up, a*180f/numberOfAxes);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
