﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void LoadScene(string name)
    {
            SceneManager.LoadScene(name);

    }
}