using UnityEngine;
using System.Collections;

public class GridMovement : BaseMovement {

	// Use this for initialization
	override protected void Awake () 
	{
		base.Awake();
		Debug.Log ("[<color=orange>GridMovement</color>], OnAwake()");
	}

	void OnAwake()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
