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
        public List<CarBehaviour> CarsOnField => _carsOnField;
        public Transform CarIdleAheadPosition => _carIdleAheadPosition;
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
        [SerializeField] private Transform _carIdleAheadPosition;
        [SerializeField] private Transform _carIdleBehindPosition;
        private List<GridBehaviour> _grids;
        private List<CarBehaviour> _carsOnField;

        private int _currentLeftGrid = 0;
        private int _currentRightGrid = 0;
        private int _currentLeftCar = 0;
        private int _currentRightCar = 0;
        private List<CarBehaviour> _cars;



        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            _carsOnField = new List<CarBehaviour>();

            _leftBarrier.Initialize(gameManager);
            _rightBarrier.Initialize(gameManager);


            SetGridList(_leftSideGrid, _rightSideGrid);
            SetCarList(_currentLeftCars, _currentRightCars);

            foreach(var car in _cars)
            {
                car.Initialize(_leftSideColor, _rightSideColor, gameManager);
            }
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
                _currentRightCar++;
                _currentRightCars[_currentRightCar + 1].MoveNextPosition(_carIdleAheadPosition);
                //CreateNewCar(_rightCarPrefab, )
            }
            else
            {
                _currentLeftCars[_currentLeftCar].CarMovement(_leftSideGrid[_currentLeftGrid], _leftSideGrid[_currentLeftGrid].PathForLeft, 1.5f);
                _leftSideGrid[_currentLeftGrid].IsFull = true;
                _currentLeftCar++;
                CreateNewCar(_leftCarPrefab, )
            }
        }

        private void CreateNewCar(CarBehaviour car, Transform instantiatePosition, Color color)
        {
           var instatiatedCar = Instantiate(car, instantiatePosition.position, Quaternion.identity);
            //instatiatedCar.Initialize()
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
