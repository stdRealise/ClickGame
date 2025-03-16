using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float _health;

    public event UnityAction<float> OnDamaged;
    public event UnityAction OnDead;
    public void Initialize(EnemyData enemyData)
    {
        _health = enemyData.Health;
        _image.sprite = enemyData.Sprite;
    }

    public void DoDamage(float damage)
    {
        if (damage >= _health)
        {
            _health = 0;
            OnDamaged?.Invoke(damage);
            OnDead?.Invoke();
            return;
        }
        _health -= damage;
        //_image.color = new Color(0.85f, 0.52f, 0.52f);
        OnDamaged?.Invoke(damage);
    }

    public float GetHealth() { return _health; }
    
}
