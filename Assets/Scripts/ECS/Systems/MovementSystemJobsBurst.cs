using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

namespace DOTSCompare.ECS
{
    // By default, the Burst Compiler is enabled. Unity's Burst Compiler
    // is built on top of the LLVM compiler, which is optimized for your
    // hardware.  This allows you continue using C# code but closer to the
    // performance of C++.  To the end-user, it's nearly invisible.  


    public class MovementSystemJobsBurst : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;
            float upperBounds = GameManager.Instance.UpperBounds;
            float lowerBounds = GameManager.Instance.BottomBounds;

            JobHandle jobHandle = Entities.
                ForEach((ref Translation trans, ref MoveForward moveForward) =>
                {

                    trans.Value += new float3(0f, 0f, moveForward.speed * deltaTime);

                    if (trans.Value.z >= upperBounds)
                    {
                        trans.Value.z = lowerBounds;
                    }

                }).Schedule(inputDeps);

            return jobHandle;
        }
    }
}
