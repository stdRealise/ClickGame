﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<Pin> _pins;
        public void Initialize(ProgressState locationState, int currentLevel, UnityAction<int> levelStartCallback)
        {
            for (var i = 0; i < _pins.Count; i++)
            {
                var level = i + 1;
                var pinState = locationState switch
                {
                    ProgressState.Completed => ProgressState.Completed,
                    ProgressState.Closed => ProgressState.Closed,
                    _ => currentLevel > level ? 
                        ProgressState.Completed : 
                        currentLevel == level ? 
                            ProgressState.Current : 
                            ProgressState.Closed
                };
                if (pinState == ProgressState.Closed)
                    _pins[i].Initialize(level, pinState, null);
                else
                    _pins[i].Initialize(level, pinState, () => levelStartCallback?.Invoke(level));
            }
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
