using System.Collections.Generic;
using UnityEngine;
using Game.Skills.Data;

namespace Game.Configs.SkillsConfig
{
    [CreateAssetMenu(menuName = "Configs/SkillsConfig", fileName = "SkillsConfig")]
    public class SkillsConfig : ScriptableObject
    {
        public List<SkillData> Skills;
        private Dictionary<string, Dictionary<int, SkillDataByLevel>> _skillDataByLevelMap;
        public SkillDataByLevel GetSkillData(string skillId, int level) {
            if (_skillDataByLevelMap == null || _skillDataByLevelMap.Count == 0)
            {
                FillSkillDataMap();
            }
            return _skillDataByLevelMap[skillId][level];
        }

        private void FillSkillDataMap()
        {
            _skillDataByLevelMap = new();
            foreach (var skillData in Skills)
            {
                if(!_skillDataByLevelMap.ContainsKey(skillData.SkillId))
                {
                    _skillDataByLevelMap[skillData.SkillId] = new();
                }
                foreach (var skillDataByLevel in skillData.SkillLevels)
                {
                    _skillDataByLevelMap[skillData.SkillId][skillDataByLevel.Level] = skillDataByLevel;
                }
            }
        }
    }
}