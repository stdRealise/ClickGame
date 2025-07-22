using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class Pin : MonoBehaviour 
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color _currentLevel;
        [SerializeField] private Color _completedLevel;
        [SerializeField] private Color _closedLevel;
        public void Initialize(int levelNumber, ProgressState pinType, UnityAction clickCallback)
        {
            _text.text = levelNumber.ToString();
            _image.color = pinType switch
            {
                ProgressState.Current => _currentLevel,
                ProgressState.Completed => _completedLevel,
                ProgressState.Closed => _closedLevel,
                _ => throw new ArgumentOutOfRangeException(nameof(pinType))
            };
            _button.onClick.AddListener(() => clickCallback?.Invoke());
        }
    }
}
