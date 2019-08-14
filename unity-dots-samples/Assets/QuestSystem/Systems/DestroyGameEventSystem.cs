
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// Система удаляет игровые события
    /// </summary>
    public class DestroyGameEventSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity event_entity, 
                ref GameEvent ev) => {

                PostUpdateCommands.DestroyEntity(event_entity);
            });
        }
    }
}

