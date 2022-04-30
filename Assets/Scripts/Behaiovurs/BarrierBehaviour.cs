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

        [SerializeField] LevelEnum.Side _side;
        [Range(0.1f, 5f)]
        [SerializeField] float _speed = 1f;


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

                    GameManager.InputManager.IsRightButtonAvailable = false;
                    transform.DORotate(new Vector3(0, 180, -90), 1f/_speed).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 180, 0), 1f/_speed).SetEase(Ease.InQuint).OnComplete(() => GameManager.InputManager.IsRightButtonAvailable = true);

                        GameManager.EventManager.RightSideCarStartToMove();

                    });
                
                
            }
            else
            {
                if (_side == LevelEnum.Side.Right)
                    return;

                    GameManager.InputManager.IsLeftButtonAvailable = false;
                    transform.DORotate(new Vector3(0, 180, -90), 1f/_speed).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 180, 0), 1f/_speed).SetEase(Ease.InQuint).OnComplete(() => GameManager.InputManager.IsLeftButtonAvailable = true);

                        GameManager.EventManager.LeftSideCarStartToMove();

                    });
                
            }



        }
    }
}
