using DBH.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

namespace DBH.Editor
{
    public class MobDataEditorUtility
    {
        public static event Action<MobData> onSave;
        public static event Action<MobData> onDelete;

        public static void Save(MobData mob)
        {
            string assetPath = AssetDatabase.GetAssetPath(mob);
            AssetDatabase.RenameAsset(assetPath, mob.name);
            AssetDatabase.SaveAssetIfDirty(mob);
            AssetDatabase.Refresh();

            onSave?.Invoke(mob);
        }

        public static void Delete(MobData mob)
        {
            onDelete?.Invoke(mob);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(mob));
        }
    }
}
