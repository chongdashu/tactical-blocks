using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	public Rigidbody rigidBody;
	public Transform target;
	public Vector3 targetPosition;

	// Use this for initialization
	void Awake () 
	{
		this.rigidBody = this.GetComponent<Rigidbody>();
	}

	void Start() 
	{
		if (this.target != null)
		{
			this.targetPosition = this.target.transform.position;
		}
	}

	void FixedUpdate () 
	{
		if (this.target != null)
		{
			this.targetPosition = this.target.position;
		}
		if (this.targetPosition != null)
		{
			Vector3 dir = this.targetPosition - this.transform.position;
			Quaternion rotation = Quaternion.LookRotation(this.targetPosition - this.transform.position);
			Debug.DrawRay(this.transform.position, dir, Color.blue);
			this.transform.rotation = rotation;
		}
	}
}
