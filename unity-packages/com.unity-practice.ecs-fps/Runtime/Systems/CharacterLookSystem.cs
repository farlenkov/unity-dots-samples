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
    public class CharacterLookSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var mng = EntityManager;
            
            Entities.ForEach((ref CharacterLookData look) =>
            {
                var cam_rot = mng.GetComponentData<Rotation>(look.Camera);
                cam_rot.Value = quaternion.Euler(look.LookX, look.LookY, 0f);
                mng.SetComponentData(look.Camera, cam_rot);
            });
        }
    }
}