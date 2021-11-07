using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [CreateAssetMenu(fileName = "PlayerInputSettings", menuName = "Unity Practice/PlayerInputSettings")]
    public class PlayerInputSettings : ScriptableObject
    {
        public SettingsData Settings;

        public static SettingsData Load()
        {
            var settings = Resources.Load<PlayerInputSettings>("PlayerInputSettings");

            if (settings != null)
            {
                return settings.Settings;
            }
            else
            {
                return new SettingsData()
                {
                    MouseSensitivity = 10,
                    Acceleration = 100,
                    MaxSpeed = 3,
                    JumpForce = 15,
                    VerticalLookMinMax = new float2(-0.97f, 0.97f)
                };
            }
        }

        [Serializable]
        public struct SettingsData
        {
            public float MouseSensitivity;
            public float Acceleration;
            public float MaxSpeed;
            public float JumpForce;
            public float2 VerticalLookMinMax;
        }
    }
}