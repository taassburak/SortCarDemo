using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Behaviours;
namespace Scripts.Manager
{
    public class LevelManager : CustomBehaviour
    {
        [SerializeField] LevelBehaviour _currentLevel; //we have only one level so i dont create a list to hold levels;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _currentLevel.Initialize(gameManager);
        }

        private void ClearLevel()
        {
            foreach (var car in _currentLevel.CarsOnField)
            {
                Destroy(car.gameObject);
            }
        }

        
        

    }

   
}
