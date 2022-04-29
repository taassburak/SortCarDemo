using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enums;
using DG.Tweening;
namespace Scripts.Behaviours
{
    public class CarBehaviour : MonoBehaviour
    {
        [SerializeField] private LevelEnum.Side _side;
        GameManager _gamemanager;


        public void Initialize(Color color, GameManager gameManager)
        {
            SetMaterial(color);
            _gamemanager = gameManager;
        }

        private void SetMaterial(Color color)
        {
            var renderer = GetComponentInChildren<MeshRenderer>();

            renderer.materials[0].color = color;
        }


        public void MoveNextPosition(Transform nextPosition)
        {
            transform.DOMove(nextPosition.position, 0.5f).SetEase(Ease.InQuint);
        }

        public void CarMovement(GridBehaviour grid , Vector3[] pathPoins, float reachTime)
        {
            transform.DOPath(pathPoins, reachTime, PathType.Linear).SetLookAt(-1f).SetEase(Ease.InOutSine).OnComplete(()=>CheckCarOnCorrectGrid(grid.CorrectSide));
            Debug.Log("car moving " + transform.name);
        }


        public void CheckCarOnCorrectGrid(LevelEnum.Side reachedGridSide)
        {
            if (this._side == reachedGridSide)
            {
                _gamemanager.EventManager.CarReachedCorrectGrid();
            }
            else
            {
                _gamemanager.EventManager.CarReachedWrongGrid();
            }
        }


      
    }
}
