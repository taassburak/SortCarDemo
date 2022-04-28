using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
namespace Scripts.Manager
{
    public class LevelManager : CustomBehaviour
    {
        [SerializeField] LevelBehaviour _currentLevel; //we have only one level so i dont create a list to hold levels;
        private int _requiredCorrectMove;
        private int _currentCorrectMove;
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _currentLevel.Initialize(gameManager);
            GameManager.EventManager.OnCarReachedAGrid+= CheckLevelCompleted;
            _requiredCorrectMove = 9;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnCarReachedAGrid -= CheckLevelCompleted;
        }

        private void ClearLevel()
        {
            Destroy(_currentLevel);
        }

        private void StartLevel()
        {
            Instantiate(_currentLevel);
            _requiredCorrectMove = _currentLevel.GridCounts;
        }

        private void CheckLevelCompleted(bool isCorrectMove)
        {
            if (isCorrectMove)
            {
                _currentCorrectMove++;
                if (_currentCorrectMove >= _requiredCorrectMove)
                {
                    GameManager.EventManager.LevelCompleted();
                    Debug.Log("levelCompleted");
                }
            }
        }

        private void LevelFailed()
        {
            GameManager.EventManager.LevelFailed();
        }
        

    }

   
}
