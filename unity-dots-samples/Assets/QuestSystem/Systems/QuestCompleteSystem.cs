﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
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
            var counters = EntityManager.GetBuffer<QuestCounterElement>(quest_entity);
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

            PostUpdateCommands.RemoveComponent<QuestChangeEvent>(quest_entity);

            if (is_complete)
            {
                PostUpdateCommands.RemoveComponent<QuestActiveTag>(quest_entity);
                PostUpdateCommands.AddComponent<QuestCompleteTag>(quest_entity);
            }
        }
    }
}