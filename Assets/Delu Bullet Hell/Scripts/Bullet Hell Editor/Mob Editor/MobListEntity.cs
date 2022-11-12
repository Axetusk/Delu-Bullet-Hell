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

        public event Action<MobListEntity> onEntityDoubleClicked;

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
            if (eventData.clickCount == 2)
            {
                onEntityDoubleClicked.Invoke(this);
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