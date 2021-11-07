using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [GenerateAuthoringComponent]
    public struct CharacterMoveData : IComponentData
    {
        public float3 Up;
        public float3 Velocity;
    }
}