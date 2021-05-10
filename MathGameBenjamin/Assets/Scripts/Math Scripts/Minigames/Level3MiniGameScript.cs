using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Level3MiniGameScript : MonoBehaviour
{
    public Text firstNumberText;
    public Text secondNumberText;
    public Text multText;
    public Text equalsText;
    public Text thirdNumberText;

    private int f;
    private int s;
    private int a;
    public int boxChoice;

    public float first;
    public float second;
    public float mult;
    public float equal;
    public float third;

    public GameObject SurpriseTramp;
    public GameObject TrampPlatform;


    void Start()
    {
        f = Random.Range(2, 10);
        s = Random.Range(2, 10);
        a = f * s;
        boxChoice = Random.Range(0, 3);

        List<float> myList = new List<float>() { f, s, a };

        //assigning values to the textboxes
        firstNumberText.text = myList[boxChoice].ToString();
        myList.RemoveAt(boxChoice);
        boxChoice = Random.Range(0, 2);
        secondNumberText.text = myList[boxChoice].ToString();
        myList.RemoveAt(boxChoice);
        thirdNumberText.text = myList[0].ToString();
    }

    void Update()
    {
        //finding the parent left-right position
        first = firstNumberText.transform.position.x;
        second = secondNumberText.transform.position.x;
        mult = multText.transform.position.x;
        equal = equalsText.transform.position.x;
        third = thirdNumberText.transform.position.x;

        //sorting for correct order
        List<float> tests = new List<float>() { first, second, mult, equal, third };
        tests.Sort();

        if (tests[1] == mult && tests[3] == equal)
        {
            if (first > second && first > third)
            {
                if (int.Parse(firstNumberText.text) > int.Parse(secondNumberText.text) && int.Parse(firstNumberText.text) > int.Parse(thirdNumberText.text))
                {
                    TrampPlatform.SetActive(true);
                    SurpriseTramp.SetActive(true);
                    Destroy(gameObject);
                }
            }

            if (second > first && second > third)
            {
                if (int.Parse(secondNumberText.text) > int.Parse(firstNumberText.text) && int.Parse(secondNumberText.text) > int.Parse(thirdNumberText.text))
                {
                    TrampPlatform.SetActive(true);
                    SurpriseTramp.SetActive(true);
                    Destroy(gameObject);
                }
            }

            if (third > first && third > second)
            {
                if (int.Parse(thirdNumberText.text) > int.Parse(firstNumberText.text) && int.Parse(thirdNumberText.text) > int.Parse(secondNumberText.text))
                {
                    TrampPlatform.SetActive(true);
                    SurpriseTramp.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }

        
        
    }
}
