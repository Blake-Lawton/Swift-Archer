using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemoControls : MonoBehaviour
{
    enum HeartsStyle { Normal = 0, Beating, Last }
    HeartsStyle style;

    public IconProgressBar healthBar;
    public SplitIconProgressBar splitArmourBar;

    public Slider currentValueSlider;
    public Text iconCountLabel;
    public Toggle fractionsToggle;

    bool runIntro;
    float startingTimer;
    const float IntroTime = 1.5f;

    void Update()
    {
        if(!runIntro)
        {
            startingTimer += Time.deltaTime;

            healthBar.CurrentValue = (startingTimer / IntroTime) * healthBar.maxValue;
            splitArmourBar.CurrentValue = (startingTimer / IntroTime) * splitArmourBar.maxValue;

            if(startingTimer > IntroTime)
                runIntro = true;
        }
        else
        {
            healthBar.CurrentValue = currentValueSlider.value * healthBar.maxValue;
            splitArmourBar.CurrentValue = currentValueSlider.value * splitArmourBar.maxValue;
        }

        healthBar.showFractions = !fractionsToggle.isOn;

        if(Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            healthBar.CurrentValue++;
            splitArmourBar.CurrentValue++;

            currentValueSlider.value = healthBar.CurrentValue / healthBar.maxValue;
        }

        if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            healthBar.CurrentValue--;
            splitArmourBar.CurrentValue--;

            currentValueSlider.value = healthBar.CurrentValue / healthBar.maxValue;
        }

        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            healthBar.CurrentValue = 0;
            splitArmourBar.CurrentValue = 0;

            currentValueSlider.value = healthBar.CurrentValue / healthBar.maxValue;
        }

        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            healthBar.CurrentValue = 5;
            splitArmourBar.CurrentValue = 5;

            currentValueSlider.value = healthBar.CurrentValue / healthBar.maxValue;
        }
    }

    public void PlusHeartStyle()
    {
        style++;

        if(style == HeartsStyle.Last)
            style = 0;

        //heartsStyleLabel.text = style.ToString();

        SetStyle();
    }

    public void MinusHeartsStyle()
    {
        style--;

        if(style < 0)
            style = HeartsStyle.Last - 1;

        //heartsStyleLabel.text = style.ToString();

        SetStyle();
    }

    void SetStyle()
    {
        //switch(style)
        //{
        //    case HeartsStyle.Normal:
        //        beatingHealthBar.gameObject.SetActive(false);
        //        healthBar.gameObject.SetActive(true);
        //        break;

        //    case HeartsStyle.Beating:
        //        beatingHealthBar.gameObject.SetActive(true);
        //        healthBar.gameObject.SetActive(false);
        //        break;
        //}
    }


    public void PlusIconCount()
    {
        if(healthBar.MaxIcons < 10)
        {
            healthBar.MaxIcons++;
            splitArmourBar.MaxIcons++;

            iconCountLabel.text = healthBar.MaxIcons.ToString();
        }
    }

    public void MinusIconCount()
    {
        if(healthBar.MaxIcons > 1)
        {
            healthBar.MaxIcons--;
            splitArmourBar.MaxIcons--;

            iconCountLabel.text = healthBar.MaxIcons.ToString();
        }
    }
}
