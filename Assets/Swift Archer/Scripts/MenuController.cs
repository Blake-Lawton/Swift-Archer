using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text currentScoreText;
    
    private void Awake()
    {

        highScoreText.text += (int)PlayerPrefs.GetFloat("Score");
        currentScoreText.text += (int)GameManagement.score;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

   public void OnDeath()
    {
        int score = PlayerPrefs.GetInt("ScoreBank") + (int)GameManagement.score;
        PlayerPrefs.SetInt("ScoreBank", score );
        
        if (GameManagement.score > PlayerPrefs.GetFloat("Score"))
        {
            PlayerPrefs.SetFloat("Score", GameManagement.score);
        }
        SceneManager.LoadScene("DeathMenu");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void UpgradesButton()
    {
        SceneManager.LoadScene("UpgradeMenu");
    }

    public void DestroyMusic()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
    }
}
