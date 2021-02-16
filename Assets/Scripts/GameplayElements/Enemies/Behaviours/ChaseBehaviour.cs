using System;

namespace GameplayElements.Enemies.Behaviours
{
    public class ChaseBehaviour : Behaviour
    {
        private float _enemySpeed;
        private EnemyView _view;

        public ChaseBehaviour(float enemySpeed, EnemyView view)
        {
            _enemySpeed = enemySpeed;
            _view = view;
        }
        
        public void Execute()
        {
            if(_view != null)
                _view.ToggleMovement(_enemySpeed);
        }

        public void Stop()
        {
            if(_view != null)
                _view.ToggleMovement(0);
        }
    }
}