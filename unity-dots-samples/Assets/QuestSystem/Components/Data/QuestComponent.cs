using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// компонент квеста
    /// </summary>
    public struct QuestComponent : IComponentData
    {
        public int QuestID;
    }
}
