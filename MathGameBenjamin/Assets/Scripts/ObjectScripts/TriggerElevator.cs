using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerElevator : MonoBehaviour
{
    public Elevator elevator;
    public GameObject dialogueBox;
    public Text runText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        elevator.ActivateElevator();
        dialogueBox.SetActive(true);
        RunAnimationText();

        //move to off screen
        this.transform.position += new Vector3(0, 100);
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
            yield return new WaitForSeconds(0.05f);
        }
    }
}
