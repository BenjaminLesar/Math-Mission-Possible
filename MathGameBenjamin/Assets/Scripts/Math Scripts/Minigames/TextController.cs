using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    /// <summary>
    /// A function to run text animation by a defined speed (int)
    /// </summary>
    /// <param name="runText">a Text UI</param>
    /// <param name="speed">an integer number</param>
    public void RunAnimationText(Text runText, int speed)
    {
        string currentText = runText.text;
        runText.text = "";
        StartCoroutine(Typing(currentText, runText, speed));
    }

    IEnumerator Typing(string text, Text runText, int speed)
    {
        foreach (char letter in text.ToCharArray())
        {
            runText.text += letter;
            yield return new WaitForSeconds(0.05f/speed);
        }
    }
}
