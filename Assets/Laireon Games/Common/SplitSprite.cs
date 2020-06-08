using UnityEngine;
using System.Collections;

public class SplitSprite : MonoBehaviour
{
    public TransitionalObject[] segments;

    void Start()
    {
        for(int i = 0; i < segments.Length; i++)
        {
            segments[i].JumpToStart();//hide the segments that have just been created
            segments[i].State = TransitionalObjects.BaseTransition.TransitionState.Finished;//hack to stop an instant transition from happening
        }
    }

    /// <summary>
    /// This controls which parts of the heart will fall off according to the given percentage. 0 remove them all, 0.5 half
    /// </summary>
    public void SetValue(float value, float delay)
    {
        float percentage = 1.0f / segments.Length;//percentage per segment

        for(int i = 0; i < segments.Length; i++)
            if((i + 1) * percentage > value)//determine if this segment should fade in or out. Nothing will happen if neither
            {
                segments[i].FadeOutDelay = delay * (segments.Length - i - 1);
                segments[i].TriggerFadeOutIfActive();
            }
            else
            {
                segments[i].Delay = delay * i;
                segments[i].TriggerTransitionIfIdle();
            }
    }
}
