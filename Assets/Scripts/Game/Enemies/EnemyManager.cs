using UnityEngine;
using UnityEngine.Events;
using Game.Configs.EnemyConfigs;
using Game.Configs.AttackSyConfig;
using Game.Configs.LevelConfigs;

namespace Game.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;

        private Enemy _currentEnemy;
        private HealthBar.HealthBar _healthBar;
        private Timer.Timer _timer;
        private LevelData _levelData;
        private int _currentEnemyIndex;
        private DamageType _currentEnemyDamageType;
        public event UnityAction<bool, int> OnLevelCompleted;

        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer)
        {
            _healthBar = healthBar;
            _timer = timer;
        }

        public void StartLevel(LevelData levelData)
        {
            _levelData = levelData;
            _currentEnemyIndex = -1;
            if (_currentEnemy == null)
            {
                _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                _currentEnemy.OnDead += SpawnEnemy;
                _currentEnemy.OnDamaged += _healthBar.DecreaseValue;
                // last version: _currentEnemy.OnDead += _healthBar.Hide;
            }
            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            _currentEnemyIndex++;
            _timer.Stop();
            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                OnLevelCompleted?.Invoke(true, _currentEnemyIndex);
                _timer.Stop();
                return;
            }
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
            _timer.SetValue(currentEnemy.Time);
            _timer.OnTimerEnd += () => OnLevelCompleted?.Invoke(false, _currentEnemyIndex);
            var currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id);
            _currentEnemyDamageType = currentEnemyData.DamageType;
            InitHpBar(currentEnemy.Hp);
            _currentEnemy.Initialize(currentEnemyData.Sprite, currentEnemy.Hp);
        }

        private void InitHpBar(float health)
        {
            _healthBar.Show();
            _healthBar.SetMaxValue(health);

        }

        public void DamageCurrentEnemy(float damage)
        {
            _currentEnemy.DoDamage(damage);
        }

        public DamageType GetCurrentEnemyDamageType()
        {
            return _currentEnemyDamageType;
        }
    }
}