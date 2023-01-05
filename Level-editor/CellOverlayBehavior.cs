using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellOverlayBehavior : MonoBehaviour
{
    RawImage image;
    bool isSelected = false;
    float originalAlpha;
    public float selectedAlpha;
    void Awake()
    {
        image = GetComponent<RawImage>();
        originalAlpha = image.color.a;
    }
    public void ToggleSelect(bool selected)
    {
        if (selected)
        {
            image.color = new Color(255, 255, 255, selectedAlpha);
        }
        else
        {
            image.color = new Color(255, 255, 255, originalAlpha);
        }
    }
}
