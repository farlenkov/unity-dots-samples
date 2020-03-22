using System;
using System.Collections.Generic;
using TMPro;
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

        readonly Dictionary<int, TextMeshProUGUI> QuestTrackers = new Dictionary<int, TextMeshProUGUI>();
        readonly Dictionary<int, Entity> QuestEntities = new Dictionary<int, Entity>();
        readonly Dictionary<int, BaseInfo> Info = new Dictionary<int, BaseInfo>();

        EntityManager Manager => World.DefaultGameObjectInjectionWorld.EntityManager;
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

                var event_entity = Manager.CreateEntity(typeof(GameEvent));

                Manager.SetComponentData(
                    event_entity, 
                    new GameEvent() {
                        LocationID = location.ID,
                        TypeID = info.ID });
            });
        }

        void LoadQuests()
        {
            var quests = Resources.LoadAll<QuestInfo>("Quests");

            foreach (var quest in quests)
            {
                var quest_entity = Manager.CreateEntity(
                    typeof(QuestComponent),
                    typeof(QuestActiveTag));

                Manager.SetComponentData(quest_entity, new QuestComponent()
                {
                    QuestID = quest.ID
                });

                if (quest.Goals.Length > 0)
                    AddQuestCounters(quest, quest_entity, Manager);

                Info.Add(quest.ID, quest);
                QuestEntities.Add(quest.ID, quest_entity);
                AddQuestTracker(quest);
                UpdateQuestTracker(quest.ID);
            }
        }

        void AddQuestCounters (QuestInfo quest, Entity entity, EntityManager manager)
        {
            var counter_buffer = manager.AddBuffer<QuestCounterComponent>(entity);

            for (var i = 0; i < quest.Goals.Length; i++)
            {
                counter_buffer.Add(new QuestCounterComponent() {

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
            var quest_text = quest_go.GetComponentInChildren<TextMeshProUGUI>();
            QuestTrackers.Add(quest.ID, quest_text);
        }

        void UpdateQuestTracker(int id)
        {
            var info = Info[id];
            var tracker = QuestTrackers[id];
            var entity = QuestEntities[id];

            tracker.text = info.name;

            if (!Manager.HasComponent<QuestCounterComponent>(entity))
                return;

            var counters = Manager.GetBuffer<QuestCounterComponent>(entity);
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