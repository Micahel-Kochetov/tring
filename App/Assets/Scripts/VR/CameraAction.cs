using UnityEngine;
using System.Collections;

public class CameraAction : MonoBehaviour {
	public Transform root;
	private float v_angle, h_angle;
	private Vector2 startPos;
	private Vector2 endPos;
	private Vector2 currentswipe;
	private float deltaPos;
	private float deltaPos1;
	private float delta;
	private Vector2 direction;
	private bool directionChosen;
	private float touchBeginTime;
	// Use this for initialization
	void Start () {
		touchBeginTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (root.position + new Vector3(0,0.1f, 0));

		if (Input.touchCount == 1) {	//Camera Rotate
			var touch = Input.GetTouch(0);
			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				directionChosen = false;
				touchBeginTime = Time.time;
					break;
				case TouchPhase.Moved:
					direction = touch.position - startPos;
					transform.RotateAround(root.position, root.up, Time.deltaTime * direction.x*0.3f);
					if(transform.position.y > 1.2f && direction.y < 0) 
						return;
					else if(transform.position.y < 0f && direction.y > 0)
						return;
					else
						transform.RotateAround(root.position, transform.right, -Time.deltaTime * direction.y * 0.2f);
					break;
			case TouchPhase.Ended:
				if ((Time.time - touchBeginTime) < 0.5f) {
					endPos = touch.position;
					currentswipe = new Vector2 (endPos.x - startPos.x, endPos.y - startPos.y);
					currentswipe.Normalize ();
					if (currentswipe.x < 0 && currentswipe.y > -0.4f && currentswipe.y < 0.4f) {
						root.GetComponent<SelectObject> ().Prev ();
					}
					//swipe right
					if (currentswipe.x > 0 && currentswipe.y > -0.4f && currentswipe.y < 0.4f) {
						Debug.Log ("right swipe");
						root.GetComponent<SelectObject> ().Next ();
					}
					//swipe up
					if(currentswipe.y > 0 &&  currentswipe.x> -0.4f &&  currentswipe.x < 0.4f)
					{
						Debug.Log("up swipe");
						root.GetComponent<SelectObject> ().NextColor ();
					}
					//swipe down
					if(currentswipe.y < 0 &&  currentswipe.x > -0.4f &&  currentswipe.x < 0.4f)
					{
						Debug.Log("down swipe");
						root.GetComponent<SelectObject> ().PrevColor ();
					}
				}
					directionChosen = true;
					break;
			}

		} else if (Input.touchCount == 2) {	//Camera Zoom
			var touch0 = Input.GetTouch(0);
			var touch1 = Input.GetTouch(1);
			if(touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began){
				deltaPos = (touch0.position - touch1.position).magnitude;
			}
			else if(touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved){
				deltaPos1 = (touch0.position - touch1.position).magnitude;
				delta = deltaPos1 - deltaPos;
				Camera.main.fieldOfView = Mathf.Clamp((-delta*0.3f)/10 + Camera.main.fieldOfView, 35, 60);
				deltaPos = deltaPos1;
			}
//			else{
//				deltaPos = 0;
//			}
		}

		if (Input.GetMouseButton (0)) {
			
		}
//		transform.RotateAround(root.position, root.up, -Time.deltaTime * 40);
	}
}
