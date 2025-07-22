using Game.Configs.AttackSyConfig;
using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;


namespace Game.Skills.Variants
{
    [Preserve]
    public class StableDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private AttackSyConfig _attackSyConfig;
        private SkillDataByLevel _skillData;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData)
        {
            _skillData = skillData;
            _enemyManager = scope.EnemyManager;
            _attackSyConfig = scope.AttackSyConfig;
        }
        public override void SkillProcess()
        {
            var toDamageType = _enemyManager.GetCurrentEnemyDamageType();
            var calculateDamage = _attackSyConfig.CalculateDamage(DamageType.Stable, toDamageType, _skillData.Value);
            _enemyManager.DamageCurrentEnemy(calculateDamage);
        }
    }





}
