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

        [Header("LEFT RIGHT GRIDS")]
        [SerializeField] private List<GridBehaviour> _leftSideGrid;
        [SerializeField] private List<GridBehaviour> _rightSideGrid;

        [Header("SIDE COLOR")]
        [SerializeField] private Color _leftSideColor;
        [SerializeField] private Color _rightSideColor;

        [Header("ENVIRONMENT")]
        [SerializeField] private BarrierBehaviour _leftBarrier;
        [SerializeField] private BarrierBehaviour _rightBarrier;

        [SerializeField]private int _requiredCorrectMove;
        [SerializeField] private  List<Transform> _carIdleRightPositions;
        [SerializeField] private List<Transform> _carIdleLeftPositions;
        private List<GridBehaviour> _grids;

        private int _currentLeftGrid = 0;
        private int _currentRightGrid = 0;
        private int _currentLeftCar = 0;
        private int _currentRightCar = 0;
        private List<CarBehaviour> _cars;



        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _leftBarrier.Initialize(gameManager);
            _rightBarrier.Initialize(gameManager);


            SetGridList(_leftSideGrid, _rightSideGrid);
            CreateDefaultTwoCarsOnStart();


            foreach (var grid in _grids)
            {
                grid.Initialize(_leftSideColor, _rightSideColor);
            }

            GameManager.EventManager.OnCarStartToMove += SendCurrentCar;

        }
        private void OnDestroy()
        {
            GameManager.EventManager.OnCarStartToMove -= SendCurrentCar;
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


        private void SetGridList(List<GridBehaviour> leftSideGrid, List<GridBehaviour> rightSideGrid)
        {
            _grids = new List<GridBehaviour>();
            for (int i = 0; i < leftSideGrid.Count; i++)
            {
                _grids.Add(leftSideGrid[i]);
            }
            for (int i = 0; i < rightSideGrid.Count; i++)
            {
                _grids.Add(rightSideGrid[i]);
            }
        }
        private void SetCarList(List<CarBehaviour> leftCars, List<CarBehaviour> rightCars)
        {
            _cars = new List<CarBehaviour>();
            for (int i = 0; i < leftCars.Count; i++)
            {
                _cars.Add(leftCars[i]);
            }
            for (int i = 0; i < rightCars.Count; i++)
            {
                _cars.Add(rightCars[i]);
            }
        }
        private void FindFirstAvaiableGrid()
        {
            for (int i = 0; i < _leftSideGrid.Count; i++)
            {
                if (!_leftSideGrid[i].IsFull)
                {
                    _currentLeftGrid = i;
                    break;
                }
            }

            for (int i = 0; i < _rightSideGrid.Count; i++)
            {
                if (!_rightSideGrid[i].IsFull)
                {
                    _currentRightGrid = i;
                    break;
                }
            }
        }
        private void SendCurrentCar(bool isRightCar)
        {
            FindFirstAvaiableGrid();
            if (isRightCar)
            {
                _currentRightCars[_currentRightCar].CarMovement(_rightSideGrid[_currentRightGrid], _rightSideGrid[_currentRightGrid].PathForRight, 1.5f);
                _rightSideGrid[_currentRightGrid].IsFull = true;
                _currentRightCars[_currentRightCar + 1].MoveNextPosition(_carIdleRightPositions[0]);
                _currentRightCar++;
                CreateNewCar(_rightCarPrefab, _carIdleRightPositions[1], RightSideColor, true);
            }
            else
            {
                _currentLeftCars[_currentLeftCar].CarMovement(_leftSideGrid[_currentLeftGrid], _leftSideGrid[_currentLeftGrid].PathForLeft, 1.5f);
                _leftSideGrid[_currentLeftGrid].IsFull = true;
                _currentLeftCars[_currentLeftCar + 1].MoveNextPosition(_carIdleLeftPositions[0]);
                _currentLeftCar++;
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
