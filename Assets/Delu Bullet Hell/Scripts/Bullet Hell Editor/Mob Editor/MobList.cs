using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using DBH.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

namespace DBH.Editor
{
    public class MobList : MonoBehaviour
    {
        [SerializeField]
        private MobListEntity m_entityPrefab;

        [SerializeField]
        private RectTransform m_container;

        [SerializeField]
        private Button m_addNewMobButton;

        [SerializeField]
        private MobListEntityContextMenu m_mobListEntityContextMenu;

        private List<MobListEntity> m_entities = new List<MobListEntity>();

        private const string mobResourceFolder = "Assets/DBH/Resources/Mob Data";

        public event Action<MobData> onOpenMob;

        private void Awake()
        {
            CreateDefaultMobIfNoneExist();

            m_addNewMobButton.onClick.AddListener(HandleAddNewMobButtonClicked);
            MobDataEditorUtility.onSave += HandleSaveMob;
            MobDataEditorUtility.onDelete += HandleDelete;
        }

        private void OnDestroy()
        {
            m_addNewMobButton.onClick.RemoveListener(HandleAddNewMobButtonClicked);
        }

        // Start is called before the first frame update
        void Start()
        {
            Refresh();

            onOpenMob.Invoke(m_entities[0].data);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateNewMob(string mobName)
        {
            IEnumerable<MobListEntity> duplicateNames = m_entities.Where(e => e.data.name.StartsWith(mobName) ? int.TryParse(e.data.name.Substring(mobName.Length), out int unused) || e.data.name == mobName : false);
            if (duplicateNames.Count() > 0)
            {
                int number = duplicateNames.Count();
                string tempName = mobName + number;

                while (duplicateNames.Any(e => e.data.name == tempName))
                {
                    number++;
                    tempName = mobName + number;
                }

                mobName = tempName;
            }

            AssetDatabase.CreateAsset(MobData.CreateInstance<MobData>(), $"{mobResourceFolder}/{mobName}.asset");

            Refresh();
        }

        public void Refresh()
        {
            Clear();

            MobData[] mobs = Resources.LoadAll<MobData>("Mob Data");

            foreach (MobData mob in mobs)
            {
                MobListEntity entity = Instantiate(m_entityPrefab, m_container);

                entity.data = mob;
                entity.onEntityDoubleLeftClicked += HandleEntityDoubleClicked;
                entity.onEntityRightClicked += HandleEntityRightClicked;

                m_entities.Add(entity);
            }
        }

        private void HandleDelete(MobData mob)
        {
            try
            {
                MobListEntity entity = GetEntity(mob);
                m_entities.Remove(entity);
                Destroy(entity.gameObject);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to delete mob { mob.name }. Asset may already have been deleted, or moved");
                Debug.LogException(ex);
            }
        }

        private void Clear()
        {
            foreach (MobListEntity entity in m_entities)
            {
                Destroy(entity.gameObject);
            }

            m_entities.Clear();
        }

        private void CreateSaveFolderStructureIfNotExist()
        {
            if(!AssetDatabase.IsValidFolder("Assets/DBH"))
                AssetDatabase.CreateFolder("Assets", "DBH");

            if(!AssetDatabase.IsValidFolder("Assets/DBH/Resources"))
                AssetDatabase.CreateFolder("Assets/DBH", "Resources");

            if(!AssetDatabase.IsValidFolder("Assets/DBH/Resources/Mob Data"))
                AssetDatabase.CreateFolder("Assets/DBH/Resources", "Mob Data");

            AssetDatabase.Refresh();
        }

        private void CreateDefaultMobIfNoneExist()
        {
            CreateSaveFolderStructureIfNotExist();
            
            if(Resources.LoadAll<MobData>("Mob Data").Length == 0)
                AssetDatabase.CreateAsset(MobData.CreateInstance<MobData>(), $"{mobResourceFolder}/Default Mob.asset");
        }

        private void HandleEntityRightClicked(MobListEntity entity, PointerEventData data)
        {
            (m_mobListEntityContextMenu.transform as RectTransform).position = data.pressPosition;
            m_mobListEntityContextMenu.gameObject.SetActive(true);
            m_mobListEntityContextMenu.entity = entity;
        }

        private void HandleEntityDoubleClicked(MobListEntity entity, PointerEventData data)
        {
            onOpenMob.Invoke(entity.data);
        }

        private void HandleAddNewMobButtonClicked()
        {
            CreateNewMob("Default Mob");
            Refresh();
        }

        private void HandleSaveMob(MobData data)
        {
            GetEntity(data).data = data;
        }

        private MobListEntity GetEntity(MobData data)
        {
            GUID guid = GetMobGUID(data);
            return m_entities.Find(e => e.assetGUID == guid);
        }

        private GUID GetMobGUID(MobData data)
        {
            return AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(data));
        }
    }
}