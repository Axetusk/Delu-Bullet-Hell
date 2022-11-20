using DBH.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DBH.Editor
{
    public class MobListEntity : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image m_sprite;

        [SerializeField]
        private TextMeshProUGUI m_nameText;

        private MobData m_data;

        public event Action<MobListEntity, PointerEventData> onEntityRightClicked;
        public event Action<MobListEntity, PointerEventData> onEntitySingleLeftClicked;
        public event Action<MobListEntity, PointerEventData> onEntityDoubleLeftClicked;

        public MobData data
        {
            get { return m_data; }
            set
            {
                m_data = value;
                m_sprite.sprite = value.sprite;
                m_nameText.text = value.name;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onEntityRightClicked.Invoke(this, eventData);
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (eventData.clickCount == 2)
                {
                    onEntityDoubleLeftClicked.Invoke(this, eventData);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}