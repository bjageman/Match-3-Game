using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	[SerializeField] float gridSize = 10;

	public float GetGridSize(){
		return gridSize;
	}

	public Vector2Int GetGridPos()
	{
		return new Vector2Int (
			Mathf.RoundToInt (transform.position.x / gridSize),
			Mathf.RoundToInt (transform.position.y / gridSize)
		);
	}


}
