using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    public class PlayerInputReadSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref PlayerInputData input,
                ref PlayerInputSettings settings) =>
            {
                input.MouseX = Input.GetAxisRaw("Mouse X") * settings.MouseSensitivity;
                input.MouseY = Input.GetAxisRaw("Mouse Y") * settings.MouseSensitivity;
                input.Horizontal = Input.GetAxisRaw("Horizontal");
                input.Vertical = Input.GetAxisRaw("Vertical");
                input.IsJump = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;

            });
        }
    }
}
