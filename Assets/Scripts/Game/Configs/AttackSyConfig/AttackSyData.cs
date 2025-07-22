using Game.Configs.EnemyConfigs;
using System;

namespace Game.Configs.AttackSyConfig
{
    [Serializable]
    public struct AttackSyData
    {
        public DamageType From;
        public DamageType To;
        public float Multiplier;

    }
}