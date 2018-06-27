using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour {

	[SerializeField] float dragThreshold = 3f;

	private Vector3 screenPoint;
    private Vector3 freezeYoffset;
	private Vector3 freezeXoffset;
	private Vector3 freezeCompletelyoffset;

	bool isDragging;
	Vector3 originalPosition;
	Vector3 originalMousePosition;
	Vector3 freezeCoordinateScreenPoint;
	GameObject targetSquare;
	Waypoint waypoint;
	Rigidbody2D rb;

	void Start(){
		waypoint = GetComponent<Waypoint>();
		rb = GetComponent<Rigidbody2D>();
	}

	void OnMouseDown() {
		isDragging = true;
		originalPosition = transform.position;
		originalMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    	freezeYoffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y, screenPoint.z));
    	freezeXoffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, Input.mousePosition.y, screenPoint.z));
    	freezeCompletelyoffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, screenPoint.z));
	}

	//TODO "Lock" on a frozen position once dragged through the threshold
	//TODO Prevent tile from going more than one away
	void OnMouseDrag()
	{
		Vector3 curPosition;
		Vector3 currentMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		if (Mathf.Abs(currentMousePosition.y - originalMousePosition.y) > dragThreshold){
			freezeCoordinateScreenPoint = new Vector3(transform.position.x, Input.mousePosition.y, screenPoint.z);
			curPosition = Camera.main.ScreenToWorldPoint(freezeCoordinateScreenPoint) + freezeXoffset;
		}else if (Mathf.Abs(currentMousePosition.x - originalMousePosition.x) > dragThreshold){
			freezeCoordinateScreenPoint = new Vector3(Input.mousePosition.x, transform.position.y, screenPoint.z);
			curPosition = Camera.main.ScreenToWorldPoint(freezeCoordinateScreenPoint) + freezeYoffset;
		}else{
			freezeCoordinateScreenPoint = new Vector3(transform.position.x, transform.position.y, screenPoint.z);
			curPosition = Camera.main.ScreenToWorldPoint(freezeCoordinateScreenPoint) + freezeCompletelyoffset;
		}
		
		transform.position = curPosition;
	}

	void OnMouseUp(){
		SnapToGrid();
		isDragging = false;
		if (targetSquare != null && targetSquare.transform.position == transform.position){
			targetSquare.transform.position = originalPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (isDragging){
			targetSquare = col.gameObject;
		}
	}

	private void SnapToGrid()
    {
        float gridSize = waypoint.GetGridSize();
		transform.position = new Vector2(
			RoundUpToNumber(transform.position.x, gridSize),
			RoundUpToNumber(transform.position.y, gridSize)
		);
		
    }

	private float RoundUpToNumber(float originalNumber, float targetNumber){
		return Mathf.Round(originalNumber / targetNumber) * targetNumber;
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(
			transform.position, 
			new Vector3(dragThreshold / 2, dragThreshold / 2, 0));
	}
}
