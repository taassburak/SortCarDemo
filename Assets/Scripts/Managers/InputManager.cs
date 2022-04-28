using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Manager
{

    public class InputManager : CustomBehaviour
    {
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
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








    }
}
