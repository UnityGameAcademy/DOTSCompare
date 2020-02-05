using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace DOTSCompare.ECS
{
    // This demonstrates a simple System for moving the cubes forward in DOTS/ECS.
    // The ComponentSystem only runs on the main thread, so this demonstrates ECS without Unity Jobs.
    public class MovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.
                ForEach((ref Translation trans, ref MoveForward moveForward) =>
                {
                    float deltaTime = Time.DeltaTime;

                    trans.Value += new float3(0f, 0f, moveForward.speed * deltaTime);

                    if (trans.Value.z >= GameManager.Instance.UpperBounds)
                    {
                        trans.Value.z = GameManager.Instance.BottomBounds;
                    }
                });
        }
    }
}
