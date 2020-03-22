using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [GenerateAuthoringComponent]
    public struct PlayerInputData : IComponentData
    {
        public float MouseX;
        public float MouseY;
        public float Horizontal;
        public float Vertical;
    }
}