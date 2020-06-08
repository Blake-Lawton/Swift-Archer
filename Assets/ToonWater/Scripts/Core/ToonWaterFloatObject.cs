//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com	

using UnityEngine;

public class ToonWaterFloatObject:MonoBehaviour{
    public float _yPosBuffer;
    public float floatHeight=.5f;
    public Vector3 buoyancyCentreOffset;
    public float bounceDamp =.8f;
    public ParticleSystem ripplePS;
    public ParticleSystem splashPS;
    bool ripple;
    public ToonWater water;						//Points to the ToonWater script
    public float waterPosAdjust;				//Adjust the Y position in water
    public float rippleSizeAdjust = 1.0f;		//Adjust the size of particle system ripples
    public bool inWater;						//If the object is in water this will be toggled true
    public bool initialized;
    float rippleCounter = .5f;
    
    public void Start(){
    	InvokeRepeating("calcWaterLevel", 0.0f,.5f);
    }
    
    public void calcWaterLevel(){
    	_yPosBuffer = water.transform.position.y+(GetComponent<Collider>().bounds.size.y*.25f)+buoyancyCentreOffset.z+waterPosAdjust;
    }
    
    public void Ripple(){
    	if(inWater && rippleCounter < GetComponent<Rigidbody>().velocity.magnitude){
    		rippleCounter = .5f;
    		water.Ripple(transform);
    	}
    }
    
    public void Update(){
    	//Countdown to emit a ripple
    	rippleCounter -= Time.deltaTime;
    }
    
    public void FixedUpdate() {
    	if(inWater){	//
    		Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
    		float forceFactor = 1f - ((actionPoint.y - _yPosBuffer) / floatHeight);
    		if (forceFactor > 0f) {
    			Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
    			GetComponent<Rigidbody>().AddForceAtPosition(uplift*GetComponent<Rigidbody>().mass, actionPoint);
    		}
    		
    		if(water.currentMultiplier > 0){
    			GetComponent<Rigidbody>().AddForce(ToonWater.scrollSpeedX*water.currentMultiplier*GetComponent<Rigidbody>().mass,0.0f,ToonWater.scrollSpeedZ*water.currentMultiplier*GetComponent<Rigidbody>().mass);
    		}
    		bob();
    		if(transform.position.y > _yPosBuffer + (GetComponent<Collider>().bounds.size.y*.5f)){
    			exitWater();
    		}
    	}else if(!inWater && transform.position.y < _yPosBuffer + (GetComponent<Collider>().bounds.size.y*.5f)){
    		enterWater();
    	}
    }
    
    public void bob(){
    	GetComponent<Rigidbody>().AddForce(0.0f,-water.waveForce/(GetComponent<Rigidbody>().mass+1),0.0f);
    }
    	
    public void enterWater() {
    	if(!ripple){
    		ripple = true;
    		InvokeRepeating("Ripple", 0.0f, UnityEngine.Random.Range(0.35f, 0.3f));
    	}
    	inWater = true;
    	water.Splash(transform);
    }
    
    public void exitWater() {
    	inWater = false;
    	water.Splash(transform);
    }
}
