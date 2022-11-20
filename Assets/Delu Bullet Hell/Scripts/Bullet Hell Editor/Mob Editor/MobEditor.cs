using DBH.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DBH.Editor
{
    public class MobEditor : MonoBehaviour
    {
        [SerializeField]
        private MobList m_mobList;

        [SerializeField]
        private MobListEntityContextMenu m_mobListEntityContextMenu;

        private void Awake()
        {
            m_mobList.onAddNewMobButtonClicked += HandleAddNewMob;
            m_mobList.onMobRightClicked += HandleMoveRightClicked;

            m_mobListEntityContextMenu.onDeleteButtonClicked += HandleDeleteMob;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleAddNewMob()
        {
            m_mobList.CreateNewMob("Default Mob");
            m_mobList.Refresh();
        }

        private void HandleDeleteMob()
        {
            m_mobList.Delete(m_mobListEntityContextMenu.entity);
        }

        private void HandleMoveRightClicked(MobListEntity entity, PointerEventData data)
        {
            (m_mobListEntityContextMenu.transform as RectTransform).position = data.pressPosition;
            m_mobListEntityContextMenu.gameObject.SetActive(true);
            m_mobListEntityContextMenu.entity = entity;
        }
    }
}