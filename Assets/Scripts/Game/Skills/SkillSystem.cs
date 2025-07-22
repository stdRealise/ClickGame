using System;
using System.Collections.Generic;
using Game.Enemies;
using Global.SaveSystem.SavableObjects;
using Game.Configs.SkillsConfig;
using Game.Configs.AttackSyConfig;

namespace Game.Skills
{
    public class SkillSystem
    {
        private SkillScope _scope;
        private SkillsConfig _skillsConfig;
        private Dictionary<SkillTrigger, List<Skill>> _skillsByTrigger;
        public SkillSystem(OpenedSkills openedSkills, SkillsConfig skillsConfig, EnemyManager enemyManager, 
            AttackSyConfig attackSyConfig)
        {
            _skillsConfig = skillsConfig;
            _skillsByTrigger = new();
            _scope = new()
            {
                EnemyManager = enemyManager,
                AttackSyConfig = attackSyConfig
            };
            foreach (var skill in openedSkills.Skills)
            {
                RegisterSkill(skill);
            }
        }

        public void InvokeTrigger(SkillTrigger trigger)
        {
            if (!_skillsByTrigger.ContainsKey(trigger))
            {
                UnityEngine.Debug.Log("kk");
                return;
            }
            var skillsToActivate = _skillsByTrigger[trigger];
            foreach (var skill in skillsToActivate)
            {
                skill.SkillProcess();
            }
        }

        private void RegisterSkill(SkillWithLevel skill)
        {
            var skillData = _skillsConfig.GetSkillData(skill.Id, skill.Level);
            var skillType = Type.GetType($"Game.Skills.Variants.{skill.Id}");
            if (skillType == null)
            {
                throw new Exception($"Skill with id {skill.Id} not found");
            }
            if (Activator.CreateInstance(skillType) is not Skill skillInstance)
            {
                throw new Exception($"can not create skill with id {skill.Id}");
            } 
            skillInstance.Initialize(_scope, skillData);
            if(!_skillsByTrigger.ContainsKey(skillData.Trigger))
            {
                _skillsByTrigger[skillData.Trigger] = new();
            }
            _skillsByTrigger[skillData.Trigger].Add(skillInstance);
            skillInstance.OnSkillRegistered();
        }
    }
}
