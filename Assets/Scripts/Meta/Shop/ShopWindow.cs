﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Global.SaveSystem.SavableObjects;
using Global.SaveSystem;
using Game.Configs.SkillsConfig;
using System;

namespace Meta.Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private List<GameObject> _blocks;
        [SerializeField] private List<ShopItem> _items;
        private int _currentBlock = 0;
        private Dictionary<string, ShopItem> _itemsMap;
        private OpenedSkills _openedSkills;
        private Wallet _wallet;
        private SaveSystem _saveSystem;
        private SkillsConfig _skillsConfig;

        public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig)
        {
            _saveSystem = saveSystem;
            _skillsConfig = skillsConfig;
            _openedSkills = (OpenedSkills) saveSystem.GetData(SavableObjectType.OpenedSkills);
            _wallet = (Wallet)saveSystem.GetData(SavableObjectType.Wallet);
            InitializeItemMap();
            InitializeBlockSwitching();
            ShowShopItems();
        }

        private void InitializeItemMap()
        {
            _itemsMap = new();
            foreach (var shopItem in _items)
            {
                _itemsMap[shopItem.SkillId] = shopItem;
            }
        }
        private void SkillUpgrade(string skillId, int cost)
        {
            var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillId);
            skillWithLevel.Level++;
            _wallet.Coins -= cost;
            _saveSystem.SaveData(SavableObjectType.Wallet);
            _saveSystem.SaveData(SavableObjectType.OpenedSkills);
            ShowShopItems();
        }

        public void ShowShopItems()
        {
            foreach (var skillData in _skillsConfig.Skills)
            {
                var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillData.SkillId);
                var skillDataByLevel = skillData.GetSkillDataByLevel(skillWithLevel.Level);
                if (!_itemsMap.ContainsKey(skillData.SkillId)) continue;
                _itemsMap[skillData.SkillId].Initialize(skillId => SkillUpgrade(skillId, skillDataByLevel.Cost),
                    skillData.SkillId,
                    "Увеличение урона",
                    skillDataByLevel.Cost,
                    _wallet.Coins >= skillDataByLevel.Cost,
                    skillData.IsMaxLevel(skillWithLevel.Level));
            }
        }

        public void InitializeBlockSwitching()
        {
            _prevButton.onClick.AddListener(() => ShowBlock(_currentBlock - 1));
            _nextButton.onClick.AddListener(() => ShowBlock(_currentBlock + 1));
            ShowBlock(_currentBlock);
        }

        private void ShowBlock(int idx)
        {
            for (var i = 0; i < _blocks.Count; i++)
            {
                _currentBlock = (idx + _blocks.Count) % _blocks.Count;
                _blocks[i].SetActive(i == _currentBlock);
            }
        }
    }
}
