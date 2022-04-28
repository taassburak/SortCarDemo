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
        [SerializeField]private LevelEnum.Side _side;
        GameManager _gamemanager;

        private Vector3 _startingPosition;
        Rigidbody m_Rb;
        public  void Initialize(Color colorLeft, Color colorRight, GameManager gameManager)
        {
            //SetMaterial(colorLeft, colorRight);
            _gamemanager = gameManager;
            _startingPosition = transform.position;
            m_Rb = GetComponent<Rigidbody>();
        }

        private void SetMaterial(Color colorLeft, Color colorRight)
        {
            var renderer = GetComponent<MeshRenderer>();
            if (_side == LevelEnum.Side.Left)
            {
                renderer.materials[0].color = colorLeft;
            }
            else
            {
                renderer.materials[0].color = colorRight;
            }
        }


        private void MoveNextPosition()
        {

        }

        private void FixedUpdate()
        {
            //Vector3 movement = new Vector3(_startingPosition.x - transform.position.x, 0, _startingPosition.z- transform.position.z).normalized;

            //if (movement == Vector3.zero)
            //{
            //    return;
            //}

            //Quaternion targetRotation = Quaternion.LookRotation(movement);
            //targetRotation = Quaternion.RotateTowards(
            //        transform.rotation,
            //        targetRotation,
            //        180);

            //m_Rb.MoveRotation(targetRotation);

        }

        public void CarMovement(GridBehaviour grid , Vector3[] pathPoins, float reachTime)
        {
            transform.DOPath(pathPoins, reachTime, PathType.Linear).SetLookAt(1f).SetEase(Ease.InOutSine).OnComplete(()=>CheckCarOnCorrectGrid(grid.CorrectSide));
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
