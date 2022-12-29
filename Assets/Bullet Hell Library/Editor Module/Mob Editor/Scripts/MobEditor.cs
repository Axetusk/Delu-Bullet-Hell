using DBH.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DBH.Editor
{
    public class MobEditor : MonoBehaviour
    {
        [SerializeField]
        private MobList m_mobList;

        [SerializeField]
        private MobStatsPanel m_mobStats;

        [SerializeField]
        private MobPreviewPanel m_mobPreview;

        [SerializeField]
        private Button m_saveButton;

        private MobData m_selectedMob;
        private MobData m_selectedMobChanges;

        public event Action<MobData> onSelectedMobChanged;
        public event Action<MobData> onMobDataChanged;

        private void Awake()
        {
            m_mobList.onOpenMob += HandleMobOpened;
            MobDataEditorUtility.onDelete += HandleMobDeleted;
            m_saveButton.onClick.AddListener(HandleSaveMob);

            m_mobStats.Initialize(this);
            m_mobPreview.Initialize(this);
        }

        public void SetName(string name)
        {
            m_selectedMobChanges.name = name;
            onMobDataChanged(m_selectedMobChanges);
        }

        public void SetSprite(Sprite sprite)
        {
            m_selectedMobChanges.sprite = sprite;
            onMobDataChanged(m_selectedMobChanges);
        }

        public void SetHP(float HP)
        {
            m_selectedMobChanges.HP = HP;
            onMobDataChanged(m_selectedMobChanges);
        }

        public void SetHitboxRadius(float radius)
        {
            m_selectedMobChanges.hitboxRaidus = radius;
            onMobDataChanged(m_selectedMobChanges);
        }

        public string GetName()
        {
            return m_selectedMobChanges.name;
        }

        public Sprite GetSprite()
        {
            return m_selectedMobChanges.sprite;
        }

        public float GetHP()
        {
            return m_selectedMobChanges.HP;
        }

        public float GetHitboxRadius()
        {
            return m_selectedMobChanges.hitboxRaidus;
        }

        private void HandleMobOpened(MobData mob)
        {
            m_mobStats.gameObject.SetActive(true);
            m_selectedMob = mob;
            m_selectedMobChanges = m_selectedMob.CreateCopy();
            onSelectedMobChanged(m_selectedMobChanges);
        }

        private void HandleSaveMob()
        {
            m_selectedMob.Overwrite(m_selectedMobChanges);
            MobDataEditorUtility.Save(m_selectedMob);
        }

        private void HandleMobDeleted(MobData mob)
        {
            if (m_selectedMob == mob)
            {
                m_mobStats.gameObject.SetActive(false);
            }
        }
    }
}