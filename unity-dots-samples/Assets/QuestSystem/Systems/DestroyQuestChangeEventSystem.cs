
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// Система удаляет события изменения квестов
    /// </summary>
    public class DestroyQuestChangeEventSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity quest_entity,
                ref QuestChangeEvent change) =>
            {
                PostUpdateCommands.RemoveComponent<QuestChangeEvent>(quest_entity);
            });
        }
    }
}

