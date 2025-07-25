using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Game.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Slider _slider;
        private float _maxTime;
        private float _currentTime;
        private bool _isPlaying;
        public event UnityAction OnTimerEnd;

        public void SetValue(float maxTime)
        {
            _maxTime = maxTime;
            SetSliderMaxValue(maxTime);
            _currentTime = maxTime;
            Play();
        }

        public void Play()
        {
            _isPlaying = true;
        }

        public void Stop()
        {
            _isPlaying = false;
            OnTimerEnd = null;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void Resume()
        {
            _isPlaying = true;
        }

        public void SetSliderMaxValue(float value)
        {
            _slider.maxValue = value;
            _slider.value = value;
        }

        public void DecreaseValue(float value)
        {
            _slider.value -= value;
        }

        public void IncreaseValue(float value)
        {
            _slider.value += value;
        }

        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            var deltaTime = Time.fixedDeltaTime;
            if (_currentTime <= deltaTime)
            {
                OnTimerEnd?.Invoke();
                Stop();
                return;
            }
            _currentTime -= deltaTime;
            DecreaseValue(deltaTime);
            _timerText.text = _currentTime.ToString("00.00");
        }
    }
}