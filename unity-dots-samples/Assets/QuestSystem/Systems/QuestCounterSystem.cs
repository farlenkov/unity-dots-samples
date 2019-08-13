using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    public class QuestCounterSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity event_entity, ref QuestGoalEvent ev) => {

                UpdateQuestCounters(ev);
                PostUpdateCommands.DestroyEntity(event_entity);
            });
        }

        void UpdateQuestCounters(QuestGoalEvent ev)
        {
            Entities.ForEach((
                    Entity quest_entity,
                    ref QuestComponent quest,
                    ref QuestActiveTag active) =>
            {
                if (EntityManager.HasComponent<QuestCounterElement>(quest_entity))
                    UpdateQuestCounter(quest_entity, quest, ev);
            });
        }

        void UpdateQuestCounter(Entity quest_entity, QuestComponent quest, QuestGoalEvent ev)
        {
            var counters = EntityManager.GetBuffer<QuestCounterElement>(quest_entity);

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

                    // так делать не нужно :)
                    QuestSystemTest.QuestUpdated(quest.QuestID);
                }
            }
        }
    }
}
