using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{

    public PlayerController player;
    public SimpleHealthBar healthBar;
    [SerializeField] private float currentProgress;
    [SerializeField] private float maxProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.UpdateBar(100f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.speed > 6f)
        {
            healthBar.UpdateBar(0f, 0f);
            return;
        }

        currentProgress += Time.deltaTime;
        
        healthBar.UpdateBar(currentProgress, maxProgress);

        if (currentProgress >= maxProgress)
        {
            player.SpeedUp();
            //Speed up 
            currentProgress = 0;
            healthBar.UpdateBar(currentProgress, maxProgress);
        }
    }
}
