using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    [SerializeField] public  Texture[] playerTexture;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.GetComponent<Renderer>().material.mainTexture = playerTexture[PlayerPrefs.GetInt("TextureIndex")];
    }
    public void BlueArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 0);
    }
    public void WhiteArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 1);
    }
    public void BlackArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 2);
    }
    public void GreenArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 3);
    }
    public void OrangeArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 4);
    }
    public void PinkArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 5);
    }
    public void PurpleArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 6);
    }
    public void TealArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 7);
    }
    public void YellowArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 8);
    }
    public void RedArcherButton()
    {
        PlayerPrefs.SetInt("TextureIndex", 9);
    }
}
