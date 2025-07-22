using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private float _health;
        private Sequence _currentSequenceDamage;

        public event UnityAction<float> OnDamaged;
        public event UnityAction OnDead;
        public void Initialize(Sprite sprite, float health)
        {
            _health = health;
            _image.sprite = sprite;
            SetCurrentSequenceDamage();
        }

        private void SetCurrentSequenceDamage()
        {
            _currentSequenceDamage = DOTween.Sequence()
                .AppendCallback(() => {
                    transform.localScale = new(1, 1, 1);
                    _image.color = Color.white;
                })
                .Append(transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.2f))
                .Join(_image.DOColor(new Color(0.85f, 0.52f, 0.52f), 0.2f))
                .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f))
                .Join(_image.DOColor(Color.white, 0.1f))
                .SetAutoKill(false)
                .Pause();
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
            _currentSequenceDamage.Restart();
            OnDamaged?.Invoke(damage);
        }

        public float GetHealth() { return _health; }

    }
}