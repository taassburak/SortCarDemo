using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Manager
{
    public class GameManager : CustomBehaviour
    {
        public UIManager UIManager => _uiManager;
        public EventManager EventManager => _eventManager;
        public LevelManager LevelManager => _levelManager;

        [SerializeField] private UIManager _uiManager;
        [SerializeField] private EventManager _eventManager;
        [SerializeField] private LevelManager _levelManager;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            _uiManager.Initialize(this);
            _eventManager.Initialize(this);
            _levelManager.Initialize(this);
        }
    }
}


