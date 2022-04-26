using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public UIManager UIManager => _uiManager;
    
    [SerializeField]private UIManager _uiManager;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _uiManager.Initialize(this);
    }
}


