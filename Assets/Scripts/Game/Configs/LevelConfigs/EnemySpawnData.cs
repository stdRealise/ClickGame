using System;

namespace Game.Configs.LevelConfigs
{
    [Serializable]
    public struct EnemySpawnData {
        public string Id;
        public float Hp;
        public float Time;
        public bool IsBoss;
    }
}