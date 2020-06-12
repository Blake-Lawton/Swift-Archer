
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    public GameObject[] platformPrefabs;


    private Transform playerTransform;
    private float spawnZ = 20f;
    private float spawnX = 33.6f;
    private float platformLength = 24f; 
    private int platformsOnScreen = 4;
    private float safeZone = 15f;
    private int lastPrefabIndex = 0;

    
    public List<GameObject> activePlatforms;

    


    // Start is called before the first frame update
    void Start()
    {
        GameManagement.score = 0;
        activePlatforms = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < platformsOnScreen; i++)
        {
            if(i < 2)
            {
                SpawnPlatform(0);
            }
            SpawnPlatform();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z  - safeZone > (spawnZ - platformsOnScreen * platformLength))
        {
            SpawnPlatform();
            DeletePlatform();
        }
    }

    

    private void SpawnPlatform(int prefabIndex = -1)
    {
        
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(platformPrefabs[RandomPrefabIndex()]) as GameObject;
            
        }
        else
        {
            go = Instantiate(platformPrefabs[prefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        //go.transform.position = Vector3.forward * spawnZ;
        go.transform.position = new Vector3(spawnX, 0, spawnZ);
        spawnZ += platformLength;

        activePlatforms.Add(go);
        
    }

    private void DeletePlatform()
    {
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
       if(platformPrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        if (GameManagement.score < 950)
        {
            while (randomIndex == lastPrefabIndex)
            {
                randomIndex = UnityEngine.Random.Range(1, 5);
            }
        }
        else if(GameManagement.score > 950 && GameManagement.score < 1950)
        {
            while (randomIndex == lastPrefabIndex)
            {
                randomIndex = UnityEngine.Random.Range(5, 9);
            }
        }
        else if(GameManagement.score > 1950 && GameManagement.score < 2950)
        {
            while (randomIndex == lastPrefabIndex)
            {
                randomIndex = UnityEngine.Random.Range(9, 13);
            }
        }
        else if (GameManagement.score > 2950 && GameManagement.score < 3950)
        {
            while (randomIndex == lastPrefabIndex)
            {
                randomIndex = UnityEngine.Random.Range(1, 5);
            }
        }
        else if (GameManagement.score > 3950 && GameManagement.score < 4950)
        {
            while (randomIndex == lastPrefabIndex)
            {
                randomIndex = UnityEngine.Random.Range(5, 9);
            }
        }
        else if ( GameManagement.score < 4950)
        {
            while (randomIndex == lastPrefabIndex)
            {
                randomIndex = UnityEngine.Random.Range(9, 13);
            }
        }



        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

   
}
