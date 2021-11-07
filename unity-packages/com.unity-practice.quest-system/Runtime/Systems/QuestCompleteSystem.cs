using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// Система ловит события изменения счетчиков в квестах
    /// и помечает завершенные квесты
    /// </summary>
    [UpdateAfter(typeof(QuestCounterSystem))]
    [UpdateBefore(typeof(DestroyQuestChangeEventSystem))]
    public class QuestCompleteSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity quest_entity,
                ref QuestComponent quest,
                ref QuestActiveTag active,
                ref QuestChangeEvent change) =>
            {
                CheckQuestCompletion(quest_entity);
            });
        }

        void CheckQuestCompletion(Entity quest_entity)
        {
            var counters = EntityManager.GetBuffer<QuestCounterComponent>(quest_entity);
            var is_complete = counters.IsCompleted();

            if (is_complete)
            {
                PostUpdateCommands.RemoveComponent<QuestActiveTag>(quest_entity);
                PostUpdateCommands.AddComponent<QuestCompleteTag>(quest_entity);
            }
        }
    }
}