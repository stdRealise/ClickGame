using UnityEngine;
using System;
using Game.Configs.AttackSyConfig;

namespace Game.Configs.EnemyConfigs
{
    [Serializable]
    public struct EnemyViewData
    {
        public string Id;
        public Sprite Sprite;
        public DamageType DamageType;
    }
}