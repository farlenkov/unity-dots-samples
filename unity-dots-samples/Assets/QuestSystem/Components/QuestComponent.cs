using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    public struct QuestComponent : IComponentData
    {
        public int QuestID;
    }
}
