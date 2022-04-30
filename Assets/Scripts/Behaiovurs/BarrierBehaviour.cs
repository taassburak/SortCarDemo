using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Scripts.Manager;
using Scripts.Enums;
namespace Scripts.Behaviours
{
    public class BarrierBehaviour : CustomBehaviour
    {
        public bool IsLeftButtonAvaiable => _isLeftButtonAvaiable;
        public bool IsRightButtonAvaiable => _isRightButtonAvaiable;

        [SerializeField] LevelEnum.Side _side;
        [Range(0.1f, 5f)]
        [SerializeField] float _speed = 1f;

        private bool _isLeftButtonAvaiable = true;
        private bool _isRightButtonAvaiable = true;



        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            GameManager.EventManager.OnBarrierMove += OpenBarrierCloseBarrier;
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnBarrierMove -= OpenBarrierCloseBarrier;
        }

        private void OpenBarrierCloseBarrier(bool isRightSide)
        {

            if (isRightSide)
            {
                if (_side == LevelEnum.Side.Left)
                    return;

                //if (_isRightButtonAvaiable)
                //{
                    _isRightButtonAvaiable = false;
                    GameManager.InputManager.IsRightButtonAvaiable = false;
                    transform.DORotate(new Vector3(0, 180, -90), 1f/_speed).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 180, 0), 1f/_speed).SetEase(Ease.InQuint).OnComplete(() => GameManager.InputManager.IsRightButtonAvaiable = true);

                        GameManager.EventManager.RightSideCarStartToMove();

                    });
                //}
                
            }
            else
            {
                if (_side == LevelEnum.Side.Right)
                    return;

                //if (_isLeftButtonAvaiable)
                //{
                    _isLeftButtonAvaiable = false;
                    GameManager.InputManager.IsLeftButtonAvaiable = false;
                    transform.DORotate(new Vector3(0, 180, -90), 1f/_speed).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 180, 0), 1f/_speed).SetEase(Ease.InQuint).OnComplete(() => GameManager.InputManager.IsLeftButtonAvaiable = true);

                        GameManager.EventManager.LeftSideCarStartToMove();

                    });
                //}
            }



        }
    }
}
