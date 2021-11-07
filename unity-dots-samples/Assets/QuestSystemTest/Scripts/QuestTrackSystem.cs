using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace QuestSystem
{
    /// <summary>
    /// Система ловит события изменения счетчиков в квестах
    /// и кидает запрос на обновление UI
    /// </summary>
    [UpdateAfter(typeof(QuestCounterSystem))]
    [UpdateBefore(typeof(DestroyQuestChangeEventSystem))]
    public class QuestTrackSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                Entity quest_entity,
                ref QuestComponent quest,
                ref QuestChangeEvent change) =>
            {
                // хм.. а как правильно кидать запросы в UI ?
                QuestSystemTest.QuestUpdated(quest.QuestID);
            });
        }
    }
}
