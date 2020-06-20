using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] public static  int health = 3;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject armour1;
    [SerializeField] private GameObject armour2;
    [SerializeField] private GameObject armour3;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Health") <= 2)
        {
            health = 3;
            PlayerPrefs.SetInt("Health", health);
        }
        else 
        {
            health = PlayerPrefs.GetInt("Health");
        }
        
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        armour1.SetActive(true);
        armour2.SetActive(true);
        armour3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 5)
        {
            armour3.SetActive(false);
        }
        if(health <= 4)
        {
            armour2.SetActive(false);
        }
        if (health <= 3)
        {
            armour1.SetActive(false);
        }
        if (health <= 2)
        {
            heart3.SetActive(false);
        }
        if (health <= 1)
        {
            heart2.SetActive(false);
        }
    }

       
}
