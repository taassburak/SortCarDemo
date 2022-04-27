using System;

namespace Scripts.Manager
{

    public class EventManager : CustomBehaviour
    {
        public event Action<bool> OnLevelFinished;
        public event Action<bool> OnCarStartToMove;
        public event Action<bool> OnCarReachedAGrid;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
        }

        public void LevelCompleted()
        {
            OnLevelFinished?.Invoke(true);
        }

        public void LevelFailed()
        {
            OnLevelFinished?.Invoke(false);
        }

        public void RightSideCarStartToMove()
        {
            OnCarStartToMove?.Invoke(true);
        }

        public void LeftSideCarStartToMove()
        {
            OnCarStartToMove?.Invoke(false);
        }

        public void CarReachedCorrectGrid()
        {
            OnCarReachedAGrid?.Invoke(true);
        }

        public void CarReachedWrongGrid()
        {
            OnCarReachedAGrid?.Invoke(true);
        }



    }

}

