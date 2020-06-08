using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class IconProgressBar : MonoBehaviour
{
    public int MaxIcons//how many icons we display
    {
        get
        {
            return maxIcons;
        }

        set
        {
#if(UNITY_EDITOR)
            if(transform == null)
                transform = (RectTransform)base.transform;

            CheckImageContainer();//makes sure there is a mask etc for the images to be placed into
#endif

            if(SetupFinished())
            {
                if(totalWidth == 0)
                    Padding = Padding;//this seems redundant but its very important!! Its updates the width values so thinks are positioned properly on start

                if(maxIcons != value)//if the value has changed
                {
                    maxIcons = value;
                    transform.sizeDelta = new Vector2(totalWidth * maxIcons, backingImage.rect.height);

                    if(mainImageContainer != null)
                        mainImageContainer.sizeDelta = transform.sizeDelta;

                    valueChanged = true;

                    RefreshImages();//add or remove instances of images if needed
                    UpdateAnimation();
                }
            }
        }
    }

    [SerializeField]
    protected int maxIcons;
    public Sprite backingImage, mainImage;

    public float maxValue = 1;
    public static float currentValue;

    public static bool valueChanged;//used whenever the bar needs updated

    public float Padding
    {
        get
        {
            return padding;
        }

        set
        {
#if(UNITY_EDITOR)
            if(transform == null)
                transform = (RectTransform)base.transform;
#endif

            if(mainImage != null)
            {
                padding = value;
                totalWidth = backingImage.rect.width + padding;
                valueChanged = true;

                if(mainImageContainer != null)
                {
                    mainImageContainer.sizeDelta = new Vector2((totalWidth * maxIcons), mainImage.rect.height);
                    transform.sizeDelta = mainImageContainer.sizeDelta;
                }

                RefreshImages();
            }
        }
    }

    public virtual float CurrentValue
    {
        set
        {
            valueChanged = currentValue != value;//if the value actually changed
            currentValue = value;
        }

        get
        {
            return currentValue;
        }
    }

    [SerializeField]
    float padding;
    float totalWidth;

    public bool showFractions;

    public List<GameObject> backings = new List<GameObject>();
    public List<GameObject> mainImages = new List<GameObject>();

    public RectTransform mainImageContainer;//this helps with clipping;
    new RectTransform transform;

    void Start()
    {
        transform = (RectTransform)base.transform;

        if(mainImage != null && backingImage != null)
            Padding = Padding;//this seems redundant but its very important!! Its updates the width values so thinks are positioned properly on start
    }

    void Update()
    {
        if(valueChanged && SetupFinished())//don't try and update when there are no images! Prevents errors
            UpdateAnimation();
    }

#if(UNITY_EDITOR)
    /// <summary>
    /// Called by the inspector to delete unused images
    /// </summary>
    public virtual void ClearInactive()
    {
        for(int i = maxIcons; i < backings.Count; i++)//if we have removed icons
        {
            DestroyImmediate(backings[i]);//destroy the icons
            DestroyImmediate(mainImages[i]);
        }

        backings.RemoveRange(maxIcons, backings.Count - maxIcons);//clear the lists
        mainImages.RemoveRange(maxIcons, mainImages.Count - maxIcons);
    }

    public bool HasInactiveImages()
    {
        return backings.Count > maxIcons;
    }
#endif

    /// <summary>
    /// A helper to determine if we have setup correct
    /// </summary>
    /// <returns></returns>
    public virtual bool SetupFinished()
    {
        return mainImage != null && backingImage != null;
    }

    /// <summary>
    /// Called when initialising or when the max icons changes
    /// </summary>
    void RefreshImages()
    {
        #region Hide Unused Images
        for(int i = maxIcons; i < backings.Count && i > -1; i++)//if we have removed icons
        {
            backings[i].SetActive(false);//then disable them
            mainImages[i].SetActive(false);
        }
        #endregion

        for(int i = 0; i < maxIcons; i++)
        {
            #region Add New Objects
            if(backings.Count <= i)//if there are not enough images in the pool
            {
                backings.Add(InstantiateBacking(i));
                mainImages.Add(InstantiateMainImage(i));
            }

            if(backings[i] == null)
                backings[i] = InstantiateBacking(i);

            if(mainImages[i] == null)
                mainImages[i] = InstantiateMainImage(i);
            #endregion

            backings[i].SetActive(true);
            mainImages[i].SetActive(true);
        }

        CheckImageContainer();

        if(mainImageContainer != null)
            mainImageContainer.SetAsLastSibling();//render main images on top of the backings
    }

    /// <summary>
    /// Called to move the mask as needed
    /// </summary>
    void UpdateAnimation()
    {
        if(mainImageContainer != null)
            if(maxValue > 0)
            {
                if(showFractions)
                    mainImageContainer.localPosition = new Vector3(totalWidth * (1 - (currentValue / maxValue)) * -1 * maxIcons, 0, 0);//slide the mask. Makes for a much smoother animation
                else
                    mainImageContainer.localPosition = new Vector3(totalWidth * (1 - (Mathf.Round(currentValue) / maxValue)) * -1 * maxIcons, 0, 0);//slide the mask. Makes for a much smoother animation
            }

        for(int i = 0; i < maxIcons; i++)
        {
            ((RectTransform)backings[i].transform).anchoredPosition = new Vector2(backingImage.rect.width * (i + 0.5f) - (transform).sizeDelta.x * 0.5f, 0);
            backings[i].transform.position = new Vector3(backings[i].transform.position.x, backings[i].transform.position.y, transform.position.z);
            mainImages[i].transform.position = backings[i].transform.position;
        }

        valueChanged = false;
    }

    /// <summary>
    /// Creates a new instance of the backing game object
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    GameObject InstantiateBacking(int index)
    {
        GameObject backingObject = new GameObject("Backing " + index);
        backingObject.AddComponent<CanvasRenderer>();
        backingObject.AddComponent<Image>();
        backingObject.GetComponent<Image>().sprite = backingImage;
        backingObject.transform.SetParent(transform);
        backingObject.transform.localScale = Vector3.one;
        ((RectTransform)backingObject.transform).sizeDelta = new Vector2(backingImage.rect.width, backingImage.rect.height);

        return backingObject;
    }

    /// <summary>
    /// Creates a new instance of the main image game object
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    protected virtual GameObject InstantiateMainImage(int index)
    {
        GameObject mainImageObject = new GameObject("Icon " + index);
        mainImageObject.AddComponent<CanvasRenderer>();
        mainImageObject.AddComponent<Image>();

        if(mainImageContainer != null)
            mainImageObject.transform.SetParent(mainImageContainer);
        else
            mainImageObject.transform.SetParent(transform);

        mainImageObject.transform.localScale = Vector3.one;
        ((RectTransform)mainImageObject.transform).sizeDelta = new Vector2(mainImage.rect.width, mainImage.rect.height);
        mainImageObject.GetComponent<Image>().sprite = mainImage;

        return mainImageObject;
    }

    /// <summary>
    /// Checks if the image container exists and if it doesnt then it makes one
    /// </summary>
    protected virtual void CheckImageContainer()
    {
        if(mainImageContainer == null)
        {
            GameObject temp = new GameObject("Image Mask");
            temp.transform.parent = transform;
            temp.AddComponent<Image>();
            temp.AddComponent<Mask>();
            temp.GetComponent<Mask>().showMaskGraphic = false;
            mainImageContainer = temp.transform as RectTransform;
            mainImageContainer.pivot = new Vector2(0, 0.5f);
            mainImageContainer.localPosition = Vector3.zero;
        }
    }
}