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
        public InputManager InputManager => _inputManager;

        [SerializeField] private UIManager _uiManager;
        [SerializeField] private EventManager _eventManager;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private InputManager _inputManager;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            _eventManager.Initialize(this);
            _uiManager.Initialize(this);
            _levelManager.Initialize(this);
            _inputManager.Initialize(this);

            _eventManager.LevelStarted();
        }
    }
}


