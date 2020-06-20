using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource dash;
    public AudioSource takeDamage;
    public AudioSource buy;

    public void Awake()
    {
        
    }
    public void DashFX()
    {
        dash.Play();
    }

    public void DamageFX()
    {
        takeDamage.Play();
    }    

    public void BuyFX()
    {
        buy.Play();

    }
}
