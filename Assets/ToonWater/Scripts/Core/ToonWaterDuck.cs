using UnityEngine;
using System;


public class ToonWaterDuck:MonoBehaviour{
    public void FixedUpdate() {
      #if UNITY_ANDROID
        if(Input.touchCount==1){
    		GetComponent<Rigidbody>().AddForce (transform.forward *Time.deltaTime* 1000);
    	}	
        	GetComponent<Rigidbody>().AddTorque (transform.up *Time.deltaTime*Input.acceleration.y*-1000);
        
     #endif
    if (Input.GetKey ("up")||Input.GetKey ("w")) {
    GetComponent<Rigidbody>().AddForce (transform.forward *Time.deltaTime* 1000);
    }
    if (Input.GetKey ("down")||Input.GetKey ("s")) {
    GetComponent<Rigidbody>().AddForce (transform.forward *Time.deltaTime* -1000);
    }
    if (Input.GetKey ("right")||Input.GetKey ("d")) {
    GetComponent<Rigidbody>().AddTorque (transform.up *Time.deltaTime* 550);
    }
    if (Input.GetKey ("left")||Input.GetKey ("a")) {
    GetComponent<Rigidbody>().AddTorque (transform.up *Time.deltaTime* -550);
    }
    //if (Input.GetKey ("space")||Input.GetKey ("x")) {
    //}
    //var both = Mathf.Abs(rigidbody.velocity.x*.01) +Mathf.Abs(rigidbody.velocity.z*.01);
    //var foo = Vector3(1+both,1-both,1+both);
    //transform.localScale =foo;
    }
//function OnMouseOver(){

//var b:GameObject = Instantiate(bullet,Camera.main.transform.position ,Camera.main.transform.rotation);
//b.rigidbody.AddForce (Camera.main.transform.forward * 350);


//}
}