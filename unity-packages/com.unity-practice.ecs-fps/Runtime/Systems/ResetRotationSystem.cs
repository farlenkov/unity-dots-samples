using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Physics.Systems;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    [UpdateAfter(typeof(ExportPhysicsWorld))]
    public class ResetRotationSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref Rotation rotation,
                ref PhysicsVelocity velocity,
                ref PlayerInputData input) =>
            {
                rotation.Value = quaternion.identity;
                velocity.Angular = float3.zero;
                
            });
        }
    }
}