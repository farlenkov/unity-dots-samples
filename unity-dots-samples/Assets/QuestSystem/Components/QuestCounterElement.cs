using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    public struct QuestCounterElement : IBufferElementData
    {
        public int TypeID;
        public int LocationID;
        public int TargetCount;
        public int CurrentCount;
    }

    public static class QuestCounterElementBufferExt
    {
        public static bool IsCompleted (this DynamicBuffer<QuestCounterElement> counters)
        {
            var is_complete = true;

            for (var i = 0; i < counters.Length; i++)
            {
                var counter = counters[i];

                if (counter.TargetCount != counter.CurrentCount)
                {
                    is_complete = false;
                    break;
                }
            }

            return is_complete;
        }
    }
}