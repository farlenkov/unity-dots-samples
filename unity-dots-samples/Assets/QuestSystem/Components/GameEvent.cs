using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// игровое событие
    /// </summary>
    public struct GameEvent : IComponentData
    {
        public int TypeID;
        public int LocationID;
    }
}