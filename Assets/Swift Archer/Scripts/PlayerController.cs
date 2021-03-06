﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public TextureManager textureManager;
    public MenuController menuController;
    public MusicManager SoundFX;


    private Vector3 moveVector;

   

    [SerializeField] public float speed =0;
    [SerializeField] private float dodgeDuration = 0;
    private float dodgeTime;

    private SwipeData swipe;

    private float nextPositionX;
    private float nextPositionZ;
    private Vector3 currentPosition;

    [SerializeField] private float blink;
    [SerializeField] private float immuned;
     private float blinkTime = .1f;
     private float immunedTime;
    [SerializeField] Renderer model1;
    [SerializeField] Renderer model2;
    [SerializeField] Renderer model3;

    [SerializeField] GameObject blueTrail;
    [SerializeField] GameObject redTrail;
    [SerializeField] GameObject rainbowTrail;

    private bool movement = false;
    private bool inMotion = false;
    [SerializeField] private GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Renderer>().material.mainTexture = textureManager.playerTexture[PlayerPrefs.GetInt("TextureIndex")];

        
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;

        if(PlayerPrefs.GetInt("Trial") == 1)
        {
            blueTrail.SetActive(true);
            model3 = blueTrail.GetComponent<TrailRenderer>();
        }
        if (PlayerPrefs.GetInt("Trial") == 2)
        {
            redTrail.SetActive(true);
            model3 = redTrail.GetComponent<TrailRenderer>();
        }
        if (PlayerPrefs.GetInt("Trial") == 3)
        {
            rainbowTrail.SetActive(true);
            model3 = rainbowTrail.GetComponent<TrailRenderer>();
        }


    }
    // Update is called once per frame
    void Update()
    {
        
        if( immunedTime > 0 )
        {
            immunedTime -= Time.deltaTime;
            blinkTime -= Time.deltaTime;

            if(blinkTime <= 0)
            {
                model1.enabled = !model1.enabled;
                model2.enabled = !model2.enabled;
                model3.enabled = !model3.enabled;
                blinkTime = blink;
            }

            if(immunedTime <= 0)
            {
                model1.enabled = true;
                model2.enabled = true;
                model3.enabled = true;
            }
        }
       

        if (swipe.Direction == SwipeDirection.Down)
        {
            
            Debug.Log("DOWN");
        }


        if(swipe.Direction == SwipeDirection.Left || swipe.Direction == SwipeDirection.Right)
        {
            movement = true;
        }

        
    }

    private void FixedUpdate()
    {

        moveVector.z = speed;

        

        if(movement)
        {
            
            Movement();
            
        }

        player.transform.Translate((moveVector * speed) * Time.deltaTime);
    }


    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        swipe = data;
    }

    public void SpeedUp()
    {
        if (speed <= 5f)
        {
            speed += .125f;
        }


        if (dodgeDuration > .15)
        {
            dodgeDuration -= .025f;
        }
    }


    public void Movement()
    {

        
        if (player.transform.position.x != 26f && !inMotion && swipe.Direction == SwipeDirection.Right)
        {
            SoundFX.DashFX();
            currentPosition = player.transform.position;
            nextPositionX = currentPosition.x + 2f;
            nextPositionZ = currentPosition.z + 2f;
            dodgeTime = 0;
            inMotion = true;
           
        }

        
        if (player.transform.position.x != 18f && !inMotion && swipe.Direction == SwipeDirection.Left)
        {
            SoundFX.DashFX();
            currentPosition = player.transform.position;
            nextPositionX = currentPosition.x - 2f;
            nextPositionZ = currentPosition.z + 2f;
            dodgeTime = 0;
            inMotion = true;
            
        }

        

        if (player.transform.position.x == nextPositionX)
        {
            swipe.Direction = SwipeDirection.Down;
            inMotion = false;
            movement = false;
            

        }
        
        //dodgeTime < dodgeDuration
        if (inMotion)
        {
            
            dodgeTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(currentPosition, new Vector3(nextPositionX, player.transform.position.y, nextPositionZ), dodgeTime / dodgeDuration);
           
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        
        if (hit.CompareTag("Obstacle"))
        {
            
            if (immunedTime <= 0)
            {

                HealthManager.health -= 1; 

                if (HealthManager.health <= 0)
                {
                    menuController.OnDeath();
                }
                else
                {
                    SoundFX.DamageFX();
                    immunedTime = immuned;
                    model1.enabled = false;
                    model2.enabled = false;
                    model3.enabled = false;

                    blinkTime = blink;

                }
            }
            
            

        }
    }
}
