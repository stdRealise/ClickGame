using Game.Configs.EnemyConfigs;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Game.Configs.AttackSyConfig
{
    [CreateAssetMenu(menuName = "Configs/AttackSyConfig", fileName = "AttackSyConfig")]
    public class AttackSyConfig : ScriptableObject
    {
        [SerializeField] private List<AttackSyData> _data;
        private Dictionary<DamageType, Dictionary<DamageType, float>> _dataMap;
        public float CalculateDamage(DamageType from, DamageType to, float damage)
        {
            if(_dataMap.IsNullOrEmpty())
            {
                FillDataMap();
            }
            return _dataMap[from][to] * damage;

        }
        private void FillDataMap()
        {
            _dataMap = new();
            foreach (var data in _data)
            {
                if (!_dataMap.ContainsKey(data.From))
                {
                    _dataMap[data.From] = new();
                }
                _dataMap[data.From][data.To] = data.Multiplier;
            }
        }
    }
}