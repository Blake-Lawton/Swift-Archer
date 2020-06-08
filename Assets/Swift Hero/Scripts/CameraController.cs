using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 startOffSet;
    private Vector3 moveVector;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startOffSet = transform.position - playerTransform.position;
    } 
     
    // Update is called once per frame
    void Update()
    {
        moveVector = playerTransform.position + startOffSet;

        moveVector.x = 21.8f;

        moveVector.y = 27f;
        
         
        transform.position = moveVector; 
        
    }
}
