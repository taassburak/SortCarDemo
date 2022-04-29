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


        public void Initialize(Color colorLeft, Color colorRight, GameManager gameManager)
        {
            SetMaterial(colorLeft, colorRight);
            _gamemanager = gameManager;
        }

        private void SetMaterial(Color colorLeft, Color colorRight)
        {
            var renderer = GetComponentInChildren<MeshRenderer>();
            if (_side == LevelEnum.Side.Left)
            {
                renderer.materials[0].color = colorLeft;
            }
            else
            {
                renderer.materials[0].color = colorRight;
            }
        }


        public void MoveNextPosition(Transform nextPosition)
        {
            transform.DOMove(nextPosition.position, 0.5f);
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
