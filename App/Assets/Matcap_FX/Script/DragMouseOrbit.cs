using UnityEngine;
using System.Collections;

public class DragMouseOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float smoothTime = 2f;

    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;

    float velocityX = 0.0f;
    float velocityY = 0.0f;
	
	public float zoomSpeed = 50f;
	
	public Vector3 m_lastMousePosition;
	
    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
		
		m_lastMousePosition = Input.mousePosition;
    }
	
	void OnMouseDrag()
	{
        
		Vector3 mouseDeltaPos = m_lastMousePosition - Input.mousePosition;
		m_lastMousePosition = Input.mousePosition;	
		
        if (Input.GetMouseButton(0))
        {
            velocityX += xSpeed * mouseDeltaPos.x * distance * 0.02f;
            velocityY += ySpeed * mouseDeltaPos.y * 0.02f;
        }		
    }
	
	
    void LateUpdate()
    {
        if (target)
        {
			if( Input.GetMouseButtonDown(0) )
				m_lastMousePosition = Input.mousePosition;
			
			
			if (Input.touchCount > 0 )
	        {
				Touch t = Input.GetTouch(0);
				if( t.phase == TouchPhase.Began )
					m_lastMousePosition = Input.mousePosition;
				
				if( t.phase == TouchPhase.Moved )
				{
	           	 	velocityX -= xSpeed * t.deltaPosition.x * distance * 0.1f;
	            	velocityY -= ySpeed * t.deltaPosition.y * 0.1f;
				}
	        }
			
			
            rotationYAxis -= velocityX;
            rotationXAxis += velocityY;

            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, distanceMin, distanceMax);

			
			
            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}