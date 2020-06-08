using UnityEngine;
using System;


public class ToonWater:MonoBehaviour{
    																								
    public static float scrollSpeedX = 0.015f;
    public static float scrollSpeedZ = 0.015f;
    public float tileMaterial1 =1.0f;
    public float tileMaterial2 =1.0f;
    public static ParticleSystem _splashPS;
    public static ParticleSystem _ripplePS;
    public GameObject splashPS;
    public GameObject ripplePS;
    public float currentMultiplier;
    public int _randomizeCurrent;
    public float maxCurrent = 10.1f;
    float targetCurrentX;
    float targetCurrentY;
    public float waveForce;
    public float waveForceHeight=10.0f;
    public float waveForceSpeed=3.0f;
    public bool autoAddFloat;
    public bool wave;
    public float waveScale;
    
    public void Start() {
    		//Instantiate particles used for splash and ripple effects (Particles should only be instantiated twice)
    		if(_splashPS == null) {
				GameObject g = (GameObject)Instantiate(splashPS);
				_splashPS = g.GetComponent<ParticleSystem>();
			}
			if (_ripplePS == null) {
				GameObject g = (GameObject)Instantiate(ripplePS);
				_ripplePS = g.GetComponent<ParticleSystem>();
			}
    		
    		
    		
    		GetComponent<Renderer>().sharedMaterials[0].SetTextureScale("_MainTex", Vector2.one * tileMaterial1);
    		if(GetComponent<Renderer>().sharedMaterials.Length==2){
    			GetComponent<Renderer>().sharedMaterials[1].SetTextureScale("_MainTex", Vector2.one * tileMaterial2);		
    		}
    		if(_randomizeCurrent>0){
    			InvokeRepeating("randomizeCurrent", 0.0f, (float)_randomizeCurrent);
    		}else{
    			scrollSpeedX = scrollSpeedZ = maxCurrent;
    		}	
    }
    
    public void Ripple(Transform go){
    	
    	_ripplePS.transform.position = go.position;
    	var tmp_cs1 = _ripplePS.transform.position;
        tmp_cs1.y = transform.position.y;
        _ripplePS.transform.position = tmp_cs1;
    	_ripplePS.transform.localScale = go.GetComponent<Collider>().bounds.size;
    	
    	_ripplePS.Emit(1);
    
    }
    
    public void Splash(Transform go) {
    	_splashPS.transform.position = go.position;
    	_splashPS.transform.localScale = go.GetComponent<Collider>().bounds.size;
    	
    	
    	_splashPS.Play();
    	
    
    }
    
    public void OnDrawGizmosSelected() {	//Updates materials in editor
    		GetComponent<Renderer>().sharedMaterials[0].SetTextureScale("_MainTex", Vector2.one * tileMaterial1);
    		if(GetComponent<Renderer>().sharedMaterials.Length==2){
    			GetComponent<Renderer>().sharedMaterials[1].SetTextureScale("_MainTex", Vector2.one * tileMaterial2);		
    		}	
    }
    
    public void FixedUpdate() {
    	if(_randomizeCurrent>0){
    		scrollSpeedX = Mathf.Lerp(scrollSpeedX, targetCurrentX, Time.deltaTime*.2f);
      		scrollSpeedZ = Mathf.Lerp(scrollSpeedZ, targetCurrentY, Time.deltaTime*.2f);
        }
    	var tmp_cs2 = GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset;
        tmp_cs2.x += scrollSpeedX*.1f*Time.deltaTime;
        GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset = tmp_cs2;
    	var tmp_cs3 = GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset;
        tmp_cs3.x = GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset.x%1;
        GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset = tmp_cs3;
        var tmp_cs4 = GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset;
        tmp_cs4.y += scrollSpeedZ*.1f*Time.deltaTime;
        GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset = tmp_cs4;
        var tmp_cs5 = GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset;
        tmp_cs5.y = GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset.y%1;
        GetComponent<Renderer>().sharedMaterials[0].mainTextureOffset = tmp_cs5;
        if(GetComponent<Renderer>().sharedMaterials.Length==2){
       	 	var tmp_cs6 = GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset;
                tmp_cs6.x += scrollSpeedX*.07f*Time.deltaTime;
                GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset = tmp_cs6;
        	var tmp_cs7 = GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset;
            tmp_cs7.x = GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset.x%1;
            GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset = tmp_cs7;
        	var tmp_cs8 = GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset;
            tmp_cs8.y += scrollSpeedZ*.07f*Time.deltaTime;
            GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset = tmp_cs8;
        	var tmp_cs9 = GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset;
            tmp_cs9.y = GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset.y%1;
            GetComponent<Renderer>().sharedMaterials[1].mainTextureOffset = tmp_cs9;
        }
    	if(wave){
    		waveForce = Mathf.Sin(Time.time*waveForceSpeed)*waveForceHeight*.01f;
    		var tmp_cs10 = transform.position;
            tmp_cs10.y += waveForce;
            transform.position = tmp_cs10;
    		var tmp_cs11 = transform.localScale;
            tmp_cs11.y += waveForce*transform.localScale.y;
            transform.localScale = tmp_cs11;
    	}
    	
    }
    
    public void randomizeCurrent(){
    	targetCurrentX=UnityEngine.Random.Range(maxCurrent*-1, maxCurrent);
    	targetCurrentY=UnityEngine.Random.Range(maxCurrent*-1, maxCurrent);
    }
    
    public void OnTriggerEnter(Collider collision) {						//executes when a object hits the water
    	ToonWaterFloatObject fo = null;
    	fo = collision.transform.GetComponent<ToonWaterFloatObject>();
    	if(fo ==null){
    		if(autoAddFloat){
    			fo = collision.gameObject.AddComponent<ToonWaterFloatObject>();
    			initFloatObject(fo);
    			fo.enterWater();
    		}
    	}else  {	
    			initFloatObject(fo);
    			fo.enterWater();
    	}
    }
    
    public void initFloatObject(ToonWaterFloatObject fo){
    		fo.GetComponent<Rigidbody>().drag =(UnityEngine.Random.value*.5f+.3f);		//Adds drag to simulate movement restrictions in water
    		if(!fo.initialized){
    			fo.initialized = true;
    			if(ripplePS != null)
    			fo.ripplePS = ripplePS.transform.GetComponent<ParticleSystem>();
    			if(splashPS != null)
    			fo.splashPS = splashPS.transform.GetComponent<ParticleSystem>();
    			fo.water = this;
    			fo.enabled = true;
    		}
    }
    
    public void OnTriggerExit(Collider collision) {
    	ToonWaterFloatObject fo = collision.gameObject.GetComponent<ToonWaterFloatObject>();
    		
    		if(fo !=null){
    			fo.exitWater();
    		}
    }
}
