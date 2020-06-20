using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject tutorialObjects;
    [SerializeField] private GameObject speedUp;
    void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            tutorialObjects.SetActive(false);
            speedUp.SetActive(false);
        }
        PlayerPrefs.SetInt("Tutorial", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
