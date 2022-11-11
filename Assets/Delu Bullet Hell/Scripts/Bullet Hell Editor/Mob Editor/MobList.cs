using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DBH.Runtime;

namespace DBH.Editor
{
    public class MobList : MonoBehaviour
    {
        [SerializeField]
        private MobListEntity m_entityPrefab;

        [SerializeField]
        private RectTransform m_container;

        private List<MobListEntity> m_entities = new List<MobListEntity>();

        private const string MobResourceFolder = "Assets/DBH/Resources/Mob Data";

        private void Awake()
        {
            CreateDefaultMobIfNoneExist();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
                AssetDatabase.CreateAsset(MobData.CreateInstance<MobData>(), $"{MobResourceFolder}/Default Mob.asset");
        }
    }
}