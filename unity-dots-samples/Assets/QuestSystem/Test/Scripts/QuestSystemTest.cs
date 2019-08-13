﻿using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem
{
    public class QuestSystemTest : MonoBehaviour
    {
        public GameObject LocationTemplate;
        public GameObject QuestTemplate;

        public Transform LocationsRoot;
        public Transform QuestRoot;

        public Color ActiveQuestColor;
        public Color CompletedQuestColor;

        Dictionary<int, Text> QuestTrackers = new Dictionary<int, Text>();
        Dictionary<int, Entity> QuestEntities = new Dictionary<int, Entity>();
        Dictionary<int, BaseInfo> Info = new Dictionary<int, BaseInfo>();

        EntityManager Manager => World.Active.EntityManager;
        public static Action<int> QuestUpdated;

        void Start()
        {
            BuildUI();
            LoadQuests();
            QuestUpdated = OnQuestUpdated;
        }

        void BuildUI()
        {
            var locations = Resources.LoadAll<LocationInfo>("Locations");
            var monsters = Resources.LoadAll<MonsterInfo>("Monsters");
            var items = Resources.LoadAll<ItemInfo>("Items");
            var events = Resources.LoadAll<SpecialEventInfo>("SpecialEvents");

            foreach (var location in locations)
            {
                var location_go = Instantiate(LocationTemplate, LocationsRoot);
                var location_el = location_go.GetComponent<LocationTemplate>();
                location_el.LocationNameText.text = location.name;

                foreach (var monster in monsters)
                    CreateButton(location_el.MonsterButtonTemplate, location, monster, "Kill ");

                foreach (var item in items)
                    CreateButton(location_el.ItemButtonTemplate, location, item, "Pickup ");

                foreach (var evnt in events)
                    CreateButton(location_el.TalkButtonTemplate, location, evnt, string.Empty);
            }
        }

        void CreateButton(Button template, LocationInfo location, GoalTypeInfo info, string text)
        {
            var btn_go = Instantiate(template.gameObject, template.transform.parent);
            btn_go.SetActive(true);
            btn_go.transform.Find("Text").GetComponent<Text>().text = text + info.name;

            HandleClick(btn_go.GetComponent<Button>(), location, info);
        }

        void HandleClick (Button btn, LocationInfo location, GoalTypeInfo info)
        {
            btn.onClick.AddListener(() => {

                var event_entity = World.Active.EntityManager.CreateEntity(typeof(QuestGoalEvent));

                World.Active.EntityManager.SetComponentData(
                    event_entity, 
                    new QuestGoalEvent() {
                        LocationID = location.ID,
                        TypeID = info.ID });
            });
        }

        void LoadQuests()
        {
            var quests = Resources.LoadAll<QuestInfo>("Quests");
            var world = World.Active;

            foreach (var quest in quests)
            {
                var quest_entity = world.EntityManager.CreateEntity(
                    typeof(QuestComponent),
                    typeof(QuestActiveTag));

                world.EntityManager.SetComponentData(quest_entity, new QuestComponent()
                {
                    QuestID = quest.ID
                });

                if (quest.Goals.Length > 0)
                    AddQuestCounters(quest, quest_entity, world.EntityManager);

                Info.Add(quest.ID, quest);
                QuestEntities.Add(quest.ID, quest_entity);
                AddQuestTracker(quest);
                UpdateQuestTracker(quest.ID);
            }
        }

        void AddQuestCounters (QuestInfo quest, Entity entity, EntityManager manager)
        {
            var counter_buffer = manager.AddBuffer<QuestCounterElement>(entity);

            for (var i = 0; i < quest.Goals.Length; i++)
            {
                counter_buffer.Add(new QuestCounterElement() {

                    LocationID = quest.Goals[i].Location.ID,
                    TypeID = quest.Goals[i].Type.ID,
                    TargetCount = quest.Goals[i].Count,
                    CurrentCount = 0
                });
            }
        }

        void AddQuestTracker(QuestInfo quest)
        {
            var quest_go = Instantiate(QuestTemplate, QuestRoot);
            var quest_text = quest_go.GetComponent<Text>();
            QuestTrackers.Add(quest.ID, quest_text);
        }

        void UpdateQuestTracker(int id)
        {
            var info = Info[id];
            var tracker = QuestTrackers[id];
            var entity = QuestEntities[id];

            tracker.text = info.name;

            if (!Manager.HasComponent<QuestCounterElement>(entity))
                return;

            var counters = Manager.GetBuffer<QuestCounterElement>(entity);
            tracker.color = counters.IsCompleted() ? CompletedQuestColor : ActiveQuestColor;

            for (var i=0;i< counters.Length;i++)
            {
                var counter = counters[i];
                tracker.text += string.Format(" {0}/{1}", counter.CurrentCount, counter.TargetCount);
            }
        }

        void OnQuestUpdated(int id)
        {
            UpdateQuestTracker(id);
        }
    }
}