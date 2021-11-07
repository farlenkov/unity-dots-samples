using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// Система ловит игровые события и 
    /// увеличивает счетчики на соответствующих квестах
    /// </summary>
    [UpdateBefore(typeof(DestroyGameEventSystem))]
    public class QuestCounterSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity event_entity, 
                ref GameEvent ev) => {

                UpdateQuestCounters(ev);
            });
        }

        void UpdateQuestCounters(GameEvent ev)
        {
            Entities.ForEach((
                Entity quest_entity,
                ref QuestComponent quest,
                ref QuestActiveTag active) =>
            {
                if (EntityManager.HasComponent<QuestCounterComponent>(quest_entity))
                    UpdateQuestCounter(quest_entity, quest, ev);
            });
        }

        void UpdateQuestCounter(Entity quest_entity, QuestComponent quest, GameEvent ev)
        {
            var counters = EntityManager.GetBuffer<QuestCounterComponent>(quest_entity);

            for (var i = 0; i < counters.Length; i++)
            {
                var counter = counters[i];

                if (counter.LocationID == ev.LocationID &&
                    counter.TypeID == ev.TypeID &&
                    counter.CurrentCount < counter.TargetCount)
                {
                    counter.CurrentCount++;
                    counters[i] = counter;

                    if (!EntityManager.HasComponent<QuestChangeEvent>(quest_entity))
                        PostUpdateCommands.AddComponent<QuestChangeEvent>(quest_entity);                    
                }
            }
        }
    }
}
