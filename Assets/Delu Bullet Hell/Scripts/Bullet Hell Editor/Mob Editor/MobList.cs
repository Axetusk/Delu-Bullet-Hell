using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using DBH.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

        private List<MobListEntity> m_entities = new List<MobListEntity>();

        private const string mobResourceFolder = "Assets/DBH/Resources/Mob Data";

        public event Action onAddNewMobButtonClicked;
        public event Action<MobListEntity> onOpenMob;
        public event Action<MobListEntity, PointerEventData> onMobRightClicked;

        private void Awake()
        {
            CreateDefaultMobIfNoneExist();

            m_addNewMobButton.onClick.AddListener(HandleAddNewMobButtonClicked);
        }

        private void OnDestroy()
        {
            m_addNewMobButton.onClick.RemoveListener(HandleAddNewMobButtonClicked);
        }

        // Start is called before the first frame update
        void Start()
        {
            Refresh();
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

        public void Delete(MobListEntity entity)
        {
            Destroy(entity.gameObject);
            AssetDatabase.DeleteAsset($"{mobResourceFolder}/{entity.data.name}.asset");
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
            onMobRightClicked(entity, data);
        }

        private void HandleEntityDoubleClicked(MobListEntity entity, PointerEventData data)
        {
            onOpenMob.Invoke(entity);
        }

        private void HandleAddNewMobButtonClicked()
        {
            onAddNewMobButtonClicked.Invoke();
        }
    }
}