using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridEditor : MonoBehaviour {

	Waypoint waypoint;

	private void Awake(){
		waypoint = GetComponent<Waypoint>();
	}

	void Update(){
		if (Application.isEditor && !Application.isPlaying) {
			SnapToGrid();
		}
	}

    private void SnapToGrid()
    {
        float gridSize = waypoint.GetGridSize();
		transform.position = new Vector2(
			waypoint.GetGridPos().x * gridSize,
			waypoint.GetGridPos().y * gridSize
		);
    }
}
