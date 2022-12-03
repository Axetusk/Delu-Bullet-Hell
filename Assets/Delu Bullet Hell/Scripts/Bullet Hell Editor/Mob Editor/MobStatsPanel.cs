using DBH.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DBH.Editor
{
    public class MobStatsPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField m_nameField;

        [SerializeField]
        private TMP_InputField m_healthField;

        [SerializeField]
        private TMP_InputField m_hitboxRadiusField;

        [SerializeField]
        private TMP_Dropdown m_spriteDropdown;

        [SerializeField]
        private Button m_saveButton;

        public event Action<MobListEntity, MobData> onSaveMob;

        private MobListEntity m_selectedMobEntity;
        private MobData m_selectedMobDataCopy;

        public void SetMobData(MobListEntity mob)
        {
            m_selectedMobEntity = mob;
            m_selectedMobDataCopy = mob.data.CreateCopy();

            m_nameField.text = m_selectedMobDataCopy.name;
            m_healthField.text = m_selectedMobDataCopy.HP.ToString();
            m_hitboxRadiusField.text = m_selectedMobDataCopy.hitboxRaidus.ToString();
        }

        private void Awake()
        {
            m_saveButton.onClick.AddListener(HandleSaveMob);
            m_nameField.onEndEdit.AddListener(HandleNameChanged);
            m_healthField.onEndEdit.AddListener(HandleHPChanged);
            m_hitboxRadiusField.onEndEdit.AddListener(HandleHitboxRadiusChanged);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleNameChanged(string name)
        {
            m_selectedMobDataCopy.name = name;
        }

        private void HandleHPChanged(string name)
        {
            if (int.TryParse(name, out int result))
            {
                m_selectedMobDataCopy.HP = result;
            }
            else
            {
                m_healthField.text = m_selectedMobDataCopy.HP.ToString();
            }
        }

        private void HandleHitboxRadiusChanged(string name)
        {
            if (int.TryParse(name, out int result))
            {
                m_selectedMobDataCopy.hitboxRaidus = result;
            }
            else
            {
                m_hitboxRadiusField.text = m_selectedMobDataCopy.hitboxRaidus.ToString();
            }
        }

        private void HandleSaveMob()
        {
            onSaveMob(m_selectedMobEntity, m_selectedMobDataCopy);
            m_selectedMobDataCopy = m_selectedMobEntity.data.CreateCopy();
        }
    }
}