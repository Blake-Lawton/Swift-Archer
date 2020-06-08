using UnityEngine;
using System;


public class SmoothFollowCamBump:MonoBehaviour
{
    public Transform target;
    public float distance = 3.0f;
    public float height = 1.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public float rotationDamping = 10.0f;
    
    public Vector3 targetLookAtOffset;     // allows offsetting of camera lookAt, very useful for low bumper heights
    
    public float bumperDistanceCheck = 2.5f;  // length of bumper ray
    public float bumperCameraHeight = 1.0f;   // adjust camera height while bumping
    public Vector3 bumperRayOffset;    // allows offset of the bumper ray from target origin
    
    // If the target moves, the camera should child the target to allow for smoother movement. DR
    public void Awake()
    {
        //camera.transform.parent = target;
    }
    
    public void FixedUpdate() {
        
        Vector3 wantedPosition = target.TransformPoint(0.0f, height, -distance);
        
        // check to see if there is anything behind the target
     //   RaycastHit hit = new RaycastHit();
     //   Vector3 back = target.transform.TransformDirection((Vector3)(-1 * Vector3.forward));   
        
        // cast the bumper ray out from rear and check to see if there is anything behind
       // if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, hit, bumperDistanceCheck) 
         //         && hit.transform != target) { // ignore ray-casts that hit the user. DR
            // clamp wanted position to hit position
        //    wantedPosition.x = hit.point.x;
         //   wantedPosition.z = hit.point.z;
           // wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
      //  } 
        
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    	
    	var tmp_cs1 = transform.position;
        tmp_cs1.y = height;
        transform.position = tmp_cs1;
        Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);
        
        if (smoothRotation) {
            Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        } else {
            transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
        }
    }
}