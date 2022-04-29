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

                if (_isRightButtonAvaiable)
                {
                    _isRightButtonAvaiable = false;
                    transform.DORotate(new Vector3(0, 180, -90), 1f).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 180, 0), 1f).SetEase(Ease.InQuint).OnComplete(() => _isRightButtonAvaiable = true);

                        GameManager.EventManager.RightSideCarStartToMove();

                    });
                }
                
            }
            else
            {
                if (_side == LevelEnum.Side.Right)
                    return;

                if (_isLeftButtonAvaiable)
                {
                    _isLeftButtonAvaiable = false;
                    transform.DORotate(new Vector3(0, 180, -90), 1f).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 180, 0), 1f).SetEase(Ease.InQuint).OnComplete(() => _isLeftButtonAvaiable = true);

                        GameManager.EventManager.LeftSideCarStartToMove();

                    });
                }
            }



        }
    }
}
