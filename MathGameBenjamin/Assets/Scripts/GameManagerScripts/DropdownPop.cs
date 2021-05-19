using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DropdownPop : MonoBehaviour
{
    List<string> myList = new List<string>();
    Dropdown m_Dropdown;
    string check;

    public void myString()
    {
        check = m_Dropdown.options[m_Dropdown.value].text;
        PlayerPrefs.SetString("name", check);
    }

    public string GetMyFileName()
    {
        return check;
    }

    void Start()
    {
        m_Dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above

        if (Directory.Exists(Application.persistentDataPath))
        {
            string[] m_DropOptions = Directory.GetFiles(Application.persistentDataPath);
            foreach (string c in m_DropOptions)
            {
                myList.Add(Path.GetFileName(c));
            }

            m_Dropdown.AddOptions(myList);
        }
        
    }
}
