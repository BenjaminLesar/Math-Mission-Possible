using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointSave : MonoBehaviour
{
    public static CheckPointSave instance;
    public Text checkPointMessage;
    [SerializeField] float MessageDelay = 0.3f;

    void Awake()
    {
        instance = this;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Touched checkpoint");
            PlayerPrefs.SetFloat("PlayerX", this.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", this.transform.position.y);
            PlayerPrefs.SetString("IsNotFirst", "true");

            StartCoroutine(DisplayMessage());

            IEnumerator DisplayMessage()
            {
                checkPointMessage.transform.parent.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(MessageDelay);
                checkPointMessage.transform.parent.gameObject.SetActive(false);
                Destroy(gameObject);
            }
            
        }
    }
}