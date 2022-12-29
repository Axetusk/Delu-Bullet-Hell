using DBH.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private MobEditor m_editor;

        public void Initialize(MobEditor editor)
        {
            m_editor = editor;
            m_editor.onSelectedMobChanged += HandleSelectedMobChanged;
        }

        private void HandleSelectedMobChanged(MobData data)
        {
            m_nameField.text = data.name;
            m_healthField.text = data.HP.ToString();
            m_hitboxRadiusField.text = data.hitboxRaidus.ToString();

            int spriteOption = m_spriteDropdown.options.FindIndex(option => option.image == data.sprite);
            if (spriteOption == -1)
                spriteOption = 0;
            m_spriteDropdown.value = spriteOption;
        }


        private void Awake()
        {
            m_nameField.onEndEdit.AddListener(HandleNameChanged);
            m_healthField.onEndEdit.AddListener(HandleHPChanged);
            m_hitboxRadiusField.onEndEdit.AddListener(HandleHitboxRadiusChanged);
            m_spriteDropdown.onValueChanged.AddListener(HandleSpriteChanged);
            SpawnSpriteOptions();
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
            m_editor.SetName(name);
        }

        private void HandleHPChanged(string name)
        {
            if (int.TryParse(name, out int result))
            {
                m_editor.SetHP(result);
            }
            else
            {
                m_healthField.text = m_editor.GetHP().ToString();
            }
        }

        private void HandleHitboxRadiusChanged(string name)
        {
            if (int.TryParse(name, out int result))
            {
                m_editor.SetHitboxRadius(result);
            }
            else
            {
                m_hitboxRadiusField.text = m_editor.GetHitboxRadius().ToString();
            }
        }

        private void HandleSpriteChanged(int option)
        {
            m_editor.SetSprite(m_spriteDropdown.options[option].image);
        }

        private void SpawnSpriteOptions()
        {
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            foreach (Sprite sprite in Resources.LoadAll<Sprite>("Character Sprites"))
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = sprite.name;
                option.image = sprite;
                options.Add(option);
            }
            m_spriteDropdown.AddOptions(options);
        }
    }
}