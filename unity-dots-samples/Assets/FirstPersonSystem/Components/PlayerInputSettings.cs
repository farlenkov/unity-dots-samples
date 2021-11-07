using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [GenerateAuthoringComponent]
    public struct PlayerInputSettings : IComponentData
    {
        public float MouseSensitivity;
        public float Acceleration;
        public float MaxSpeed;
        public float JumpForce;
        public float2 VerticalLookMinMax;
    }
}