using System.Collections.Generic;
using UnityEngine;
using Game.Enemies;

namespace Game.Configs.EnemyConfigs
{
    [CreateAssetMenu(menuName = "Configs/EnemiesConfig", fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        public Enemy EnemyPrefab;
        public List<EnemyViewData> Enemies;
        public EnemyViewData GetEnemy(string id)
        {
            foreach (var enemyData in Enemies)
            {
                if (enemyData.Id == id) return enemyData;
            }
            Debug.LogError($"enemy {id} not found");
            return default;
        }
    }
}