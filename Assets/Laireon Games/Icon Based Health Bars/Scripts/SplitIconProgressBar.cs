using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class SplitIconProgressBar : IconProgressBar
{
    public SplitSprite splitSpritePrefab;//your heart prefab

    public float delayBetweenSegments = 0.1f;//helps create a ncie staggered effect when losing lots of health at once

    [SerializeField]
    List<SplitSprite> splitSprites = new List<SplitSprite>();

    public override float CurrentValue
    {
        get
        {
            return base.CurrentValue;
        }

        set
        {
            float percentage = currentValue / maxValue;
            float iconPercentage = 1.0f / splitSprites.Count;

            for(int i = 0; i < splitSprites.Count; i++)
            {
                if(percentage < 1 && (i + 1) * iconPercentage > percentage)//if this heart should fade.
                {
                    if(i * iconPercentage <= percentage)
                        splitSprites[i].SetValue((percentage % iconPercentage) / iconPercentage + 0.0001f, delayBetweenSegments);//show the heart what percentage it should display. We add a small amount on to help with floating point error
                    else
                        splitSprites[i].SetValue(0, delayBetweenSegments);//this is sprites past the current value and should be empty
                }
                else
                    splitSprites[i].SetValue(1, delayBetweenSegments);
            }

            base.CurrentValue = value;
        }
    }

#if(UNITY_EDITOR)
    /// <summary>
    /// Called by the inspector to delete unused images
    /// </summary>
    public override void ClearInactive()
    {
        for(int i = maxIcons; i < backings.Count; i++)//if we have removed icons
        {
            DestroyImmediate(backings[i]);//destroy the icons
            DestroyImmediate(mainImages[i]);
        }

        backings.RemoveRange(maxIcons, backings.Count - maxIcons);//clear the lists
        mainImages.RemoveRange(maxIcons, mainImages.Count - maxIcons);
        splitSprites.RemoveRange(maxIcons, splitSprites.Count - maxIcons);
    }
#endif

    protected override GameObject InstantiateMainImage(int index)
    {
        GameObject temp = Instantiate(splitSpritePrefab.gameObject);

        temp.transform.SetParent(transform);
        temp.transform.localScale = Vector3.one;

        splitSprites.Add(temp.GetComponent<SplitSprite>());//animates the hear fragments falling etc

        return temp;
    }

    public override bool SetupFinished()
    {
        return splitSpritePrefab != null && backingImage != null;
    }

    protected override void CheckImageContainer()
    {
        //do nothing, we don't need a mask!
    }
}
