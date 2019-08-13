using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    struct QuestCounterElement : IBufferElementData
    {
        public int TypeID;
        public int LocationID;
        public int TargetCount;
        public int CurrentCount;
    }
}