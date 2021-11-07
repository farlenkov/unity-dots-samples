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
    [UpdateAfter(typeof(CharacterInputApplySystem))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class CharacterMoveSystem : ComponentSystem
    {
        CharacterInputSettings.SettingsData InputSettings;

        protected override void OnCreate()
        {
            base.OnCreate();
            InputSettings = CharacterInputSettings.Load();
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
