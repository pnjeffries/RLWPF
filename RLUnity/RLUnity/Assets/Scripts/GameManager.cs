using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nucleus.Game;
using RLWPF;

/// <summary>
/// The central unity game manager which takes charge of 
/// </summary>
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        GameEngine.Instance.LoadModule(new RLModule());
        GameEngine.Instance.StartUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
