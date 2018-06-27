using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemRaycast : MonoBehaviour {

	float maxRaycastDepth = 100f;

	void Update() {
    if (Input.GetMouseButtonDown(0)) {
        Debug.Log("Pressed left click, casting ray.");
        CastRay();
    	}
	}

	void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit.collider) {
			Debug.DrawLine(ray.origin, hit.point);
			Debug.Log("Hit object: " + hit.collider.gameObject.name);
		}else{
			Debug.Log("Nothing detected");
		}
	}
}
