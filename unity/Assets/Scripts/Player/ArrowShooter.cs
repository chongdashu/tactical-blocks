using UnityEngine;
using System.Collections;

public class ArrowShooter : MonoBehaviour {

	public GameObject arrowPrefab;

	// Use this for initialization
	void Awake () 
	{
		if (arrowPrefab == null)
		{
			Debug.LogWarning("[ArrowShooter] No arrow prefab set!", this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
