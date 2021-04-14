using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    // Scroll main texture based on time
    float scrollSpeed = .3f;
    Image img;
    RawImage waterImg;
    float offset;
    Rect uvRect;

    void Start()
    {
        //img = GetComponent<Image>();
        waterImg = GetComponent<RawImage>();
    }

    void Update()
    {
        offset = -Time.time * scrollSpeed;  //moving water texture
        //img.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        uvRect = waterImg.uvRect;
        uvRect.y = offset;
        waterImg.uvRect = uvRect;
        //waterImg.uvRect = new Rect(0, offset, 0, 0);
        //waterImg.color = new Color32(255, 255, 225, 100);
    }
}
