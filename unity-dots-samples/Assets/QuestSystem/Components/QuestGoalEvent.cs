using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    public struct QuestGoalEvent : IComponentData
    {
        public int TypeID;
        public int LocationID;
    }
}