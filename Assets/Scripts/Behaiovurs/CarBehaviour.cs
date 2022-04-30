using Scripts.Manager;
using Scripts.Behaviours;
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
        [SerializeField] private CrashDetectorBehaviour _crashDetector;
        [SerializeField] private GameObject _correctMarkSprite;

        [Range(0.1f, 5f)]
        [SerializeField] private float _speed = 1;
        
        private GameManager _gamemanager;
        public void Initialize(Color color, GameManager gameManager)
        {
            SetMaterial(color);
            _gamemanager = gameManager;
            if (_crashDetector != null)
            {
                _crashDetector.Initialize(_gamemanager);
            }
            _correctMarkSprite.SetActive(false);
        }

        private void SetMaterial(Color color)
        {
            var renderer = GetComponentInChildren<MeshRenderer>();

            renderer.materials[0].color = color;
        }


        public void MoveNextPosition(Transform nextPosition)
        {
            transform.DOMove(nextPosition.position, 1f).SetEase(Ease.InOutSine);
        }

        public void CarMovement(GridBehaviour grid , Vector3[] pathPoins)
        {
            transform.DOPath(pathPoins, 1.5f/_speed, PathType.Linear).SetLookAt(-1f).SetEase(Ease.InOutSine).OnComplete(()=>CheckCarOnCorrectGrid(grid.CorrectSide));
            Debug.Log("car moving " + transform.name);
        }


        public void CheckCarOnCorrectGrid(LevelEnum.Side reachedGridSide)
        {
            if (this._side == reachedGridSide)
            {
                _gamemanager.EventManager.CarReachedCorrectGrid();
                _correctMarkSprite.SetActive(true);
                _correctMarkSprite.transform.eulerAngles = new Vector3(60, 3.8f, 0);
            }
            else
            {
                _gamemanager.EventManager.CarReachedWrongGrid();
            }
        }


      
    }
}
