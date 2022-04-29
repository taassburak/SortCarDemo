using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Manager
{

    public class InputManager : CustomBehaviour
    {
        [SerializeField] Material _leftButtonMaterial;
        [SerializeField] Material _rightButtonMaterial;


        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            GameManager.EventManager.OnLevelStarted += SetButtonColor;
            
        }

        private void OnDestroy()
        {
            GameManager.EventManager.OnLevelStarted -= SetButtonColor;
        }

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
                            GameManager.EventManager.LeftBarrierMove();
                        }
                        else if (hit.transform.CompareTag("RightButton"))
                        {
                            //GameManager.EventManager.RightSideCarStartToMove();
                            GameManager.EventManager.RightBarrierMove();
                        }
                    }
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
