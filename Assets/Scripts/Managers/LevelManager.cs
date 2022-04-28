using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
namespace Scripts.Manager
{
    public class LevelManager : CustomBehaviour
    {
        public bool IsCurrentLevelFinished => _isCurrentLevelFinished;

        [SerializeField] LevelBehaviour _levelPrefab; //we have only one level so i dont create a list to hold levels;

        private LevelBehaviour _currentLevel;
        private int _requiredCorrectMove;
        private int _currentCorrectMove;
        private bool _isCurrentLevelFinished = false;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _requiredCorrectMove = 9;
            
            GameManager.EventManager.OnCarReachedAGrid+= CheckLevelFinished;
            GameManager.EventManager.OnLevelStarted += StartLevel;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnCarReachedAGrid -= CheckLevelFinished;
            GameManager.EventManager.OnLevelStarted -= StartLevel;
        }

        private void ClearLevel()
        {
            Destroy(_currentLevel.gameObject);
        }

        private void StartLevel()
        {
            if (_currentLevel != null)
                ClearLevel();
            _currentLevel = Instantiate(_levelPrefab);
            _currentLevel.Initialize(GameManager);
            _currentCorrectMove = 0;
            _isCurrentLevelFinished = false;
            //_requiredCorrectMove = _levelPrefab.GridCounts;
        }

        private void CheckLevelFinished (bool isCorrectMove)
        {
            if (isCorrectMove)
            {
                _currentCorrectMove++;
                if (_currentCorrectMove >= _requiredCorrectMove)
                {
                    GameManager.EventManager.LevelCompleted();
                    _isCurrentLevelFinished = true;
                    Debug.Log("levelCompleted");
                }
            }
            else
            {
                GameManager.EventManager.LevelFailed();
                _isCurrentLevelFinished = true;
                Debug.Log("levelfailed");
            }
        }
    }

   
}
