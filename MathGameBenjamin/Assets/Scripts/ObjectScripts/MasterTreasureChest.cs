using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterTreasureChest : MonoBehaviour
{
    public GameObject treasureCanvas;
    public Animator popUp;
    public Button compass;
    public GameObject teleporter;
    public Animator masterTreasure;
  
    void Awake()
    {
        treasureCanvas.SetActive(false);
        teleporter.SetActive(false);
    }

    void Update()
    {
        compass.onClick.AddListener(GetCompass);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player.instance.FreezePlayer();
        masterTreasure.SetBool("openMasterTreasure", true);
        treasureCanvas.SetActive(true);
        popUp.SetBool("getTreasureNote", true);
    }

    private void GetCompass()
    {
        treasureCanvas.SetActive(false);
        teleporter.SetActive(true);
        Player.instance.UnFreezePlayer();
        Destroy(gameObject);
    }
}
