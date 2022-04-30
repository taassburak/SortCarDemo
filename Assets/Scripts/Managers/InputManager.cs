using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Scripts.Manager
{

    public class InputManager : CustomBehaviour
    {
       


        [SerializeField] GameObject _leftButton;
        [SerializeField] GameObject _rightButton;

        private Material _rightButtonMaterial;
        private Material _leftButtonMaterial;


        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            GameManager.EventManager.OnLevelStarted += SetButtonColor;

            _leftButtonMaterial = _leftButton.GetComponent<MeshRenderer>().material;
            _rightButtonMaterial = _rightButton.GetComponent<MeshRenderer>().material;

            IsLeftButtonAvaiable = true;
            IsRightButtonAvaiable = true;
            
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnLevelStarted -= SetButtonColor;
        }

        public bool IsLeftButtonAvaiable { get; set; }
        public bool IsRightButtonAvaiable { get; set ; }

        private void Update()
        {
            if (!GameManager.LevelManager.IsCurrentLevelFinished)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.CompareTag("LeftButton"))
                        {

                            //GameManager.EventManager.LeftSideCarStartToMove();
                            if (IsLeftButtonAvaiable)
                            {

                                GameManager.EventManager.LeftBarrierMove();
                                _leftButton.transform.DOLocalMoveY(-4, 0.3f);
                            }

                        }
                        else if (hit.transform.CompareTag("RightButton"))
                        {

                            if (IsRightButtonAvaiable)
                            {

                                //GameManager.EventManager.RightSideCarStartToMove();
                                GameManager.EventManager.RightBarrierMove();
                                _rightButton.transform.DOLocalMoveY(-4, 0.3f);
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _leftButton.transform.DOLocalMoveY(0, 0.25f);
                    _rightButton.transform.DOLocalMoveY(0, 0.25f);
                }

            }
        }

        public void SetButtonColor()
        {
            _leftButtonMaterial.color = GameManager.LevelManager.CurrentLevel.LeftSideColor;
            _rightButtonMaterial.color = GameManager.LevelManager.CurrentLevel.RightSideColor;

        }

    }
}
