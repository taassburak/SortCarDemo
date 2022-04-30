using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Behaviours
{

    public class LevelBehaviour : CustomBehaviour
    {
        public Color LeftSideColor => _leftSideColor;
        public Color RightSideColor => _rightSideColor;
        public int RequiredCorrectMove => _requiredCorrectMove;

        [Header("CARS")]
        [SerializeField] private List<CarBehaviour> _currentLeftCars;
        [SerializeField] private List<CarBehaviour> _currentRightCars;

        [SerializeField] private CarBehaviour _leftCarPrefab;
        [SerializeField] private CarBehaviour _rightCarPrefab;

        [SerializeField] private List<Transform> _carIdleRightPositions;
        [SerializeField] private List<Transform> _carIdleLeftPositions;

        [Header("LEFT RIGHT GRIDS")]
        [SerializeField] private List<GridBehaviour> _availableGridsForLeftSide;
        [SerializeField] private List<GridBehaviour> _availableGridsForRightSide;


        [Header("SIDE COLOR")]
        [SerializeField] private Color _leftSideColor;
        [SerializeField] private Color _rightSideColor;

        [Header("ENVIRONMENT")]
        [SerializeField] private BarrierBehaviour _leftBarrier;
        [SerializeField] private BarrierBehaviour _rightBarrier;
        private List<GridBehaviour> _grids;
        
        
        [SerializeField]private int _requiredCorrectMove;

        private int _currentLeftGridIndex = 0;
        private int _currentRightGridIndex = 0;
        private int _currentLeftCarIndex = 0;
        private int _currentRightCarIndex = 0;



        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _leftBarrier.Initialize(gameManager);
            _rightBarrier.Initialize(gameManager);

            GridsInitialize();
            CreateDefaultTwoCarsOnStart();

            GameManager.EventManager.OnCarStartToMove += SendCurrentCar;

        }
        private void OnDestroy()
        {
            GameManager.EventManager.OnCarStartToMove -= SendCurrentCar;
        }

        private void GridsInitialize()
        {
            foreach (var grid in _availableGridsForLeftSide)
            {
                grid.Initialize(_leftSideColor, _rightSideColor);
            }

            foreach (var grid in _availableGridsForRightSide)
            {
                grid.Initialize(_leftSideColor, _rightSideColor);
            }
        }

        private void CreateDefaultTwoCarsOnStart()
        {
            var rightCar1 = Instantiate(_rightCarPrefab,_carIdleRightPositions[0].position, Quaternion.Euler(0, 180, 0), transform);
            var rightCar2 = Instantiate(_rightCarPrefab, _carIdleRightPositions[1].position, Quaternion.Euler(0, 180, 0), transform);

            var leftCar1 = Instantiate(_leftCarPrefab, _carIdleLeftPositions[0].position, Quaternion.Euler(0, 180, 0), transform);
            var leftCar2 = Instantiate(_leftCarPrefab, _carIdleLeftPositions[1].position, Quaternion.Euler(0, 180, 0), transform);

            rightCar1.Initialize(RightSideColor, GameManager);
            rightCar2.Initialize(RightSideColor, GameManager);
            leftCar1.Initialize(LeftSideColor, GameManager);
            leftCar2.Initialize(LeftSideColor, GameManager);

            _currentRightCars.Add(rightCar1);
            _currentRightCars.Add(rightCar2);
            _currentLeftCars.Add(leftCar1);
            _currentLeftCars.Add(leftCar2);

        }

        private void FindFirstAvaiableGrid()
        {
            for (int i = 0; i < _availableGridsForLeftSide.Count; i++)
            {
                if (!_availableGridsForLeftSide[i].IsFull)
                {
                    _currentLeftGridIndex = i;
                    break;
                }
            }

            for (int i = 0; i < _availableGridsForRightSide.Count; i++)
            {
                if (!_availableGridsForRightSide[i].IsFull)
                {
                    _currentRightGridIndex = i;
                    break;
                }
            }
        }
        private void SendCurrentCar(bool isRightCar)
        {
            FindFirstAvaiableGrid();
            if (isRightCar)
            {
                _currentRightCars[_currentRightCarIndex].CarMovement(_availableGridsForRightSide[_currentRightGridIndex], _availableGridsForRightSide[_currentRightGridIndex].PathForRight);
                _availableGridsForRightSide[_currentRightGridIndex].IsFull = true;
                _currentRightCars[_currentRightCarIndex + 1].MoveNextPosition(_carIdleRightPositions[0]);
                _currentRightCarIndex++;
                CreateNewCar(_rightCarPrefab, _carIdleRightPositions[1], RightSideColor, true);
            }
            else
            {
                _currentLeftCars[_currentLeftCarIndex].CarMovement(_availableGridsForLeftSide[_currentLeftGridIndex], _availableGridsForLeftSide[_currentLeftGridIndex].PathForLeft);
                _availableGridsForLeftSide[_currentLeftGridIndex].IsFull = true;
                _currentLeftCars[_currentLeftCarIndex + 1].MoveNextPosition(_carIdleLeftPositions[0]);
                _currentLeftCarIndex++;
                CreateNewCar(_leftCarPrefab, _carIdleLeftPositions[1], LeftSideColor, false);
            }
        }

        private void CreateNewCar(CarBehaviour car, Transform instantiatePosition, Color color, bool isRightSideCar)
        {
            var instatiatedCar = Instantiate(car, instantiatePosition.position, Quaternion.Euler(0,180,0), transform);
            instatiatedCar.Initialize(color, GameManager);
            if (isRightSideCar)
            {
                _currentRightCars.Add(instatiatedCar);
            }
            else
            {
                _currentLeftCars.Add(instatiatedCar);
            }
        }

        //testing
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameManager.EventManager.LeftSideCarStartToMove();
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                GameManager.EventManager.RightSideCarStartToMove();
            }
        }


    }
}
