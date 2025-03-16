using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Timer _timer;
    [SerializeField] private EndLevelWindow _endLevelWindow;

    private void Awake()
    {
        _clickButtonManager.Initialize();
        _enemyManager.Initialize(_healthBar);
        _endLevelWindow.Initialize();

        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
        _endLevelWindow.OnRestartClicked += StartLevel;
        _enemyManager.OnLevelCompleted += LevelCompleted;
        
        StartLevel();
    }

    private void LevelCompleted()
    {
        _endLevelWindow.ShowWinWindow();
        _timer.Stop();
    }


    private void StartLevel()
    {
        _timer.Initialize(5f);
        
        _enemyManager.SpawnEnemy();
        
        _timer.OnTimerEnd += _endLevelWindow.ShowLoseWindow;
    }
}