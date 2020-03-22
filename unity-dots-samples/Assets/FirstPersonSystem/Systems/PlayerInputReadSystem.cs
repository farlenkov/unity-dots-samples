using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    public class PlayerInputReadSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Entities.ForEach((
                ref PlayerInputData input,
                in PlayerInputSettings settings) =>
            {
                input.MouseX = Input.GetAxisRaw("Mouse X") * settings.MouseSensitivity;
                input.MouseY = Input.GetAxisRaw("Mouse Y") * settings.MouseSensitivity;
                input.Horizontal = Input.GetAxisRaw("Horizontal");
                input.Vertical = Input.GetAxisRaw("Vertical");
                
            }).Run();

            return default;
        }
    }
}
