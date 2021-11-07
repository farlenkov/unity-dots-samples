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
    [UpdateAfter(typeof(PlayerInputApplySystem))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class CharacterMoveSystem : ComponentSystem
    {
        PlayerInputSettings.SettingsData InputSettings;

        protected override void OnCreate()
        {
            base.OnCreate();
            InputSettings = PlayerInputSettings.Load();
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref PhysicsVelocity vel,
                ref CharacterMoveData move) =>
            {
                vel.Linear.xz += move.Velocity.xz * InputSettings.Acceleration;               
                vel.Linear.y += move.Velocity.y;
            });
        }
    }
}
