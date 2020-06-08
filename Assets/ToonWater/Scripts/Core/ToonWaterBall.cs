//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com

using UnityEngine;
using System;
using System.Collections;


public class ToonWaterBall:MonoBehaviour{
    public Transform carrier;
    public float yOffset;
    public float xOffset;
    public static ArrayList followers;
    public bool following;
    public static Transform player;
    float resetPosTimer;
    public bool randomForce;
    float resetPosCounter;
    
    public void Start() {
    	if(followers ==null){
    		followers = new ArrayList();
    	}
    	player = GameObject.FindGameObjectWithTag("Player").transform;
    	if(player == null){
    	this.enabled = false;
    	Debug.Log("no gameobject tagged Player, disabling waterball script");
    	}	
    	if(randomForce){
    		resetPosTimer = (float)UnityEngine.Random.Range(2, 10);
    	}
    }
    
    void OnCollisionEnter(Collision collision) {
    	if(collision.gameObject.tag == "Player" && !following){
    		if(followers.Count == 0){
    		carrier = collision.gameObject.transform;
    		}else{
    		carrier = followers[followers.Count - 1] as Transform;
    		}
    		following = true;
    ////////////////////////
    		//followers.push(transform);
    		//yield return WaitForSeconds(1.0f);
    		Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, false);	
    	}
    }
    
    public void LateUpdate() {
    
    		if((player != null) && (carrier == null) && Vector3.Distance(player.transform.position, transform.position) < GetComponent<Collider>().bounds.size.magnitude && !following){
    			if(followers.Count == 0){
    			carrier = player;
    		}else{
    			carrier = followers[followers.Count - 1] as Transform;
    		}
    			following = true;
    			///////////
    		//	followers.push(transform);		
    		}
    		
    		if(resetPosCounter > resetPosTimer){
    			resetPosCounter = 0.0f;
    			resetPosTimer = (float)UnityEngine.Random.Range(2, 10);
    			transform.GetComponent<Rigidbody>().AddForce((float)UnityEngine.Random.Range(-50,50) , UnityEngine.Random.value * 75 + 25 ,(float)UnityEngine.Random.Range(-50,50));
    			transform.GetComponent<Rigidbody>().AddTorque((float)UnityEngine.Random.Range(-50,50) , (float)UnityEngine.Random.Range(-50,50) ,(float)UnityEngine.Random.Range(-50,50));
    		}else if(resetPosTimer > 0){
    			resetPosCounter+=Time.deltaTime;
    		}
    		
    }
    
    public void FixedUpdate() {
    	if(carrier != null){	
    		float yDif = carrier.position.z -transform.position.z+yOffset;
    		var tmp_cs1 = transform.GetComponent<Rigidbody>().velocity;
            tmp_cs1.z = yDif*50*Time.deltaTime;
            transform.GetComponent<Rigidbody>().velocity = tmp_cs1;
    		float xDif = carrier.position.x -transform.position.x+xOffset;
    		var tmp_cs2 = transform.GetComponent<Rigidbody>().velocity;
            tmp_cs2.x = xDif*50*Time.deltaTime;
            transform.GetComponent<Rigidbody>().velocity = tmp_cs2;
    	}
    }
}