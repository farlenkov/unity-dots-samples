using System.Collections;
using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

namespace QuestSystem
{
    public abstract class BaseInfo : ScriptableObject
    {
        public int ID;

#if UNITY_EDITOR

        [MenuItem("QuestSystem/Generate IDs")]
        static void GenerateIDs()
        {
            var items = Resources.LoadAll<BaseInfo>("");

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                item.ID = i + 1;
                EditorUtility.SetDirty(item);
                Debug.LogFormat("{0} > {1}", item.name, item.ID);
            }
        }
#endif
    }

    public abstract class GoalTypeInfo : BaseInfo
    {
    }
}