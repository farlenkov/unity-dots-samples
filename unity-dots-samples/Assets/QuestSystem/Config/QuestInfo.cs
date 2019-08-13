using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "QuestInfo", menuName = "QuestSystem/QuestInfo")]
    public class QuestInfo : BaseInfo
    {
        public GoalInfo[] Goals;

        

        [Serializable]
        public class GoalInfo
        {
            public LocationInfo Location;
            public GoalTypeInfo Type;
            public int Count;
        }
    }
}