using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [GenerateAuthoringComponent]
    public struct CharacterLookData : IComponentData
    {
        public float LookY;
        public float LookX;
        public Entity Camera;
    }
}