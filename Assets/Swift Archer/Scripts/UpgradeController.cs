using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private MusicManager soundFX;

    [SerializeField] private Text scoreBankText;

    [SerializeField] private GameObject whiteButtonLock;
    [SerializeField] private GameObject purpleButtonLock;
    [SerializeField] private GameObject yellowButtonLock;
    [SerializeField] private GameObject tealButtonLock;
    [SerializeField] private GameObject redButtonLock;
    [SerializeField] private GameObject greenButtonLock;
    [SerializeField] private GameObject orangeButtonLock;
    [SerializeField] private GameObject pinkButtonLock;
    [SerializeField] private GameObject blackButtonLock;

    [SerializeField] private GameObject blueTrailLock;
    [SerializeField] private GameObject redTrailLock;
    [SerializeField] private GameObject rainbowTrailLock;

    [SerializeField] private GameObject blueTrial;
    [SerializeField] private GameObject redTrial;
    [SerializeField] private GameObject rainbowTrial;

    [SerializeField] private GameObject armour1;
    [SerializeField] private GameObject armour2;
    [SerializeField] private GameObject armour3;
    [SerializeField] private GameObject armourMaxed;

    [SerializeField] private GameObject needMoreScore;
    public int scoreBank;
    // Start is called before the first frame update
    void Start()
    {
        


        if (PlayerPrefs.GetInt("Health") == 3)
        {
            armour1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Health") == 4)
        {
            armour2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Health") == 5)
        {
            armour3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Health") == 6)
        {
            armourMaxed.SetActive(true);
        }

        if(PlayerPrefs.GetInt("Trail") == 1)
        {
            blueTrial.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Trail") == 2)
        {
            redTrial.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Trail") == 2)
        {
            rainbowTrial.SetActive(true);
        }

        if (PlayerPrefs.GetInt("BlueTrailUnlock") == 1)
        {
            blueTrailLock.SetActive(false);
            
        }
        if (PlayerPrefs.GetInt("RedTrailUnlock") == 1)
        {
            redTrailLock.SetActive(false);
            
        }
        if (PlayerPrefs.GetInt("RainbowTrailUnlock") == 1)
        {
            rainbowTrailLock.SetActive(false);
            
        }

        if (PlayerPrefs.GetInt("WhiteUnlock") == 1)
        {
            whiteButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("PurpleUnlock") == 1)
        {
            purpleButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("YellowUnlock") == 1)
        {
            yellowButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("TealUnlock") == 1)
        {
            tealButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("RedUnlock") == 1)
        {
            redButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("GreenUnlock") == 1)
        {
            greenButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("OrangeUnlock") == 1)
        {
            orangeButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("PinkUnlock") == 1)
        {
            pinkButtonLock.SetActive(false);
        }
        if (PlayerPrefs.GetInt("BlackUnlock") == 1)
        {
            blackButtonLock.SetActive(false);
        }


        


        scoreBankText.text += PlayerPrefs.GetInt("ScoreBank");
        scoreBank = PlayerPrefs.GetInt("ScoreBank");
        
    }

   
    public bool Buy(int cost)
    {
        if(scoreBank >= cost)
        {
            scoreBank -= cost;
            scoreBankText.text = "Score Bank: " + scoreBank;
            soundFX.BuyFX();
            PlayerPrefs.SetInt("ScoreBank", scoreBank);
            needMoreScore.SetActive(false);
            return true;
        }
        else 
        {
            needMoreScore.SetActive(true);
            return false;
        }
            
        
    }

    public void BuyArmour()
    {
        
        if(PlayerPrefs.GetInt("Health") == 3)
        {
            if(Buy(10000))
            {
                PlayerPrefs.SetInt("Health", 4);
                armour1.SetActive(false);
                armour2.SetActive(true);
                
                return;
                
            }
        }
        if (PlayerPrefs.GetInt("Health") == 4)
        {
            if (Buy(20000))
            {
                PlayerPrefs.SetInt("Health", 5);
                armour2.SetActive(false);
                armour3.SetActive(true);
                
                return;

            }
        }
        if (PlayerPrefs.GetInt("Health") == 5)
        {
            if (Buy(50000))
            {
                PlayerPrefs.SetInt("Health", 6);
                armour3.SetActive(false);
                armourMaxed.SetActive(true);
                
                return;
            }
        }
    }

    public void SelectBlueTrail()
    {
        blueTrial.SetActive(true);
        redTrial.SetActive(false);
        rainbowTrial.SetActive(false);
        PlayerPrefs.SetInt("Trial", 1);
    }
    public void SelectRedTrail()
    {
        blueTrial.SetActive(false);
        redTrial.SetActive(true);
        rainbowTrial.SetActive(false);
        PlayerPrefs.SetInt("Trial", 2);
    }
    public void SelectRainbowTrail()
    {
        blueTrial.SetActive(false);
        redTrial.SetActive(false);
        rainbowTrial.SetActive(true);
        PlayerPrefs.SetInt("Trial", 3);
    }


    public void BuyBlueTrail()
    {
        if (Buy(5000))
        {
            blueTrailLock.SetActive(false);
            blueTrial.SetActive(true);
            redTrial.SetActive(false);
            rainbowTrial.SetActive(false);
            PlayerPrefs.SetInt("BlueTrailUnlock", 1);
            PlayerPrefs.SetInt("Trial", 1);
        }
    }
    public void BuyRedTrail()
    {
        if (Buy(15000))
        {
            redTrailLock.SetActive(false);
            blueTrial.SetActive(false);
            redTrial.SetActive(true);
            rainbowTrial.SetActive(false);
            PlayerPrefs.SetInt("RedTrailUnlock", 1);
            PlayerPrefs.SetInt("Trial", 2);
        }
    }
    public void BuyRainbowTrail()
    {
        if (Buy(25000))
        {
            rainbowTrailLock.SetActive(false);
            blueTrial.SetActive(false);
            redTrial.SetActive(false);
            rainbowTrial.SetActive(true);
            PlayerPrefs.SetInt("RainbowTrailUnlock", 1);
            PlayerPrefs.SetInt("Trial", 3);
        }
    }
    public void BuyWhite()
    {
        if(Buy(5000))
        {
            whiteButtonLock.SetActive(false);
            PlayerPrefs.SetInt("WhiteUnlock", 1);
        }

    }
    public void BuyPurple()
    {
        if (Buy(5000))
        {
            purpleButtonLock.SetActive(false);
            PlayerPrefs.SetInt("PurpleUnlock", 1);
        }

    }
    public void BuyYellow()
    {
        if (Buy(5000))
        {
            yellowButtonLock.SetActive(false);
            PlayerPrefs.SetInt("YellowUnlock", 1);
        }

    }
    public void BuyTeal()
    {
        if (Buy(5000))
        {
            tealButtonLock.SetActive(false);
            PlayerPrefs.SetInt("TealUnlock", 1);
        }

    }
    public void BuyRed()
    {
        if (Buy(5000))
        {
            redButtonLock.SetActive(false);
            PlayerPrefs.SetInt("RedUnlock", 1);
        }

    }
    public void BuyGreen()
    {
        if (Buy(5000))
        {
            greenButtonLock.SetActive(false);
            PlayerPrefs.SetInt("GreenUnlock", 1);
        }

    }
    public void BuyOrange()
    {
        if (Buy(5000))
        {
            orangeButtonLock.SetActive(false);
            PlayerPrefs.SetInt("OrangeUnlock", 1);
        }

    }
    public void BuyPink()
    {
        if (Buy(5000))
        {
            pinkButtonLock.SetActive(false);
            PlayerPrefs.SetInt("PinkUnlock", 1);
        }

    }
    public void BuyBlack()
    {
        if (Buy(5000))
        {
            blackButtonLock.SetActive(false);
            PlayerPrefs.SetInt("BlackUnlock", 1);
        }

    }
   
}
