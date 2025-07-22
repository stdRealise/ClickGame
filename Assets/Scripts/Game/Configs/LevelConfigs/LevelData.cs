﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs.LevelConfigs
{
    [Serializable]
    public struct LevelData {
        public int Location;
        public int LevelNumber;
        public Sprite Background;
        public List<EnemySpawnData> Enemies;
        public int Reward;
    }
}