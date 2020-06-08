using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 moveVector;

    public MenuController menuController;

    [SerializeField] public float speed =0;
    [SerializeField] private float dodgeDuration = 0;
    private float dodgeTime;

    private SwipeData swipe;

    private float nextPositionX;
    private float nextPositionZ;
    private Vector3 currentPosition;

    
    
    private bool movement = false;
    private bool inMotion = false;
     [SerializeField] private GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {

        IconProgressBar.currentValue =3;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        
    }
    // Update is called once per frame
    void Update()
    {
        
        
       

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
        if (speed <= 6f)
        {
            speed += .125f;
        }


        if (dodgeDuration > .1)
        {
            dodgeDuration -= .025f;
        }
    }


    public void Movement()
    {

        
        if (player.transform.position.x != 26f && !inMotion && swipe.Direction == SwipeDirection.Right)
        {
            Debug.Log("RIGHT");
            currentPosition = player.transform.position;
            nextPositionX = currentPosition.x + 2f;
            nextPositionZ = currentPosition.z + 2f;
            dodgeTime = 0;
            inMotion = true;
        }

        
        if (player.transform.position.x != 18f && !inMotion && swipe.Direction == SwipeDirection.Left)
        {
            Debug.Log("LEFT");
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
            
            IconProgressBar.valueChanged = true;
            if(IconProgressBar.currentValue == 1)
            {
                

                menuController.OnDeath();

                
            }
            IconProgressBar.currentValue -= 1;
        }
    }
}
