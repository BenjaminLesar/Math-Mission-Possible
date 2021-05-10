using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class WordScript : MathParent
{

    private String[] question = { "firstQuestion", "secondQuestion", "thirdQuestion", "fourthQuestion", "fifthQuestion" };
    public String[] numberText;
    private int[] numberOne = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    private int[] numberTwo = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

    private bool isText;
    private String q;
    private String nT1;
    private String nT2;

    private int n1;
    private int n2;

    //Used for last question, However when isText = true realAnswer continuosly changes
    //Used Debug.Log(realAnswer) within return method 
    //Used to change the numbers to text within the question eg. 5 = five
    void NumberToText()
    {
        if (isText)
        {
            for (int i=0; i<numberOne.Length; i++)
            {
                if(n1 == numberOne[i])
                {
                    nT1 = numberText[i];
                }
                if(n2 == numberTwo[i])
                {
                    nT2 = numberText[i];
                }
            }
        }

        else
        {
            nT1 = Convert.ToString(n1);
            nT2 = Convert.ToString(n2);
        }
    }

    override
    public int Question(Text questionText)
    {
        input.text = null;
        n1 = numberOne[Random.Range(0, numberOne.Length)];
        n2 = numberTwo[Random.Range(0, numberTwo.Length)];

        //Change [Random.Range(0, question.Length)] to 0-4 for testing specific questions
        //Eg. [Random.Range(4,4)], only question 5 will show
        q = question[Random.Range(2, 2)];

        if (q == question[0])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "There are " + nT1 + " groups of flamingos." + "\n" +
                "If there are " + nT2 + " flamingos in each group, " + "\n" +
                "what is the total amount of flamingos?";
            realAnswer = n1 * n2;
        }

        else if (q == question[1])
        {
            isText = false;
            NumberToText();
            questionText.text = "There are " + nT1 + " bowls of oranges, " + "\n" +
                "and each bowl has " + nT2 + " oranges in it." + "\n" +
                "How many oranges are there in all?";
            realAnswer = n1 * n2;
        }

        if (q == question[2])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "An octopus has " + nT1 + " legs." + "\n" +
                "How many legs are there in all, " + "\n" +
                "for " + nT2 + " octopi?";
            realAnswer = n1 * n2;
        }

        if (q == question[3])
        {
            //isText should be true
            isText = false;
            NumberToText();
            questionText.text = "David has " + nT1 + " pairs of socks." + "\n" +
                "How many socks does he have altogether?";
            realAnswer = n1 * 2;
        }

        else if (q == question[4])
        {
            isText = false;
            NumberToText();
            questionText.text = "Coach Tracy bought " + nT1 + " pizzas " + "\n" +
                "to celebrate the tennis team's win. " + "\n" +
                "If each pizza has " + nT2 + " slices, " + "\n" +
                "how many total slices do the players have?";
            realAnswer = n1 * n2;
        }
        return realAnswer;
    }
}

