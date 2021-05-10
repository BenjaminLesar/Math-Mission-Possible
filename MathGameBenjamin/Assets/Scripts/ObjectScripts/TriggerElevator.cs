using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TriggerElevator : MonoBehaviour
{
    public Elevator elevator;
    public GameObject dialogueBox;
    public Text runText;
    public CinemachineVirtualCamera vcam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        elevator.ActivateElevator();
        dialogueBox.SetActive(true);
        RunAnimationText();

        //move to off screen
        this.transform.position += new Vector3(0, 100);

        StartCoroutine(CameraZoomOut());
    }

    IEnumerator CameraZoomOut()
    {
        int speed = 10;
        while (vcam.m_Lens.OrthographicSize < 10)
        {
            vcam.m_Lens.OrthographicSize += Time.deltaTime * speed;
            yield return null;
        }
        print("camera   " + vcam.m_Lens.OrthographicSize);
    }

    void RunAnimationText()
    {
        string currentText = runText.text;
        runText.text = "";
        StartCoroutine(Typing(currentText));
    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            runText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
