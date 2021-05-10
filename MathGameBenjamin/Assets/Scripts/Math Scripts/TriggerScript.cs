using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{
    public static TriggerScript instance;
    public GameObject canvas;

    [SerializeField] Animator boxAnimator;

    void Awake()
    {
        instance = this;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {     
        if (other.gameObject.CompareTag("Player"))
        {
            //MultiplierScript myScript = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<MultiplierScript>();
            //RepAddScript addScript = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<RepAddScript>();
            //ShapeScript geoScript = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<ShapeScript>();
            //WordScript wScript = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<WordScript>();
            //WordV2Script wv2Script = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<WordV2Script>();

            MathParent math = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<MathParent>();

            PlayerPrefs.SetString("mathQuestion", this.name);
            boxAnimator = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            canvas = this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
            canvas.SetActive(true);
            boxAnimator.SetTrigger("Popup");
            Time.timeScale = 0;
            Player.instance.FreezePlayer();

            if (math != null)
            {
                math.DoMath();
            }
            //if (myScript != null)
            //{
            //    myScript.DoMath();
            //}
            //if (addScript != null)
            //{
            //    addScript.DoMath();
            //}
            //if (geoScript != null)
            //{
            //    geoScript.DoMath();
            //}
            //if (wScript != null)
            //{
            //    wScript.DoMath();
            //}
            //if (wv2Script != null)
            //{
            //    wv2Script.DoMath();
            //}
        }       
    }
}
