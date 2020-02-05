using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

namespace DOTSCompare.ECS
{
    // Here we switch to using the JobComponentSystem.  Though the logic is nearly
    // identical to the MovementSystem, this takes advantages of multiple cores.
    //
    // To avoid race conditions, Unity breaks complex tasks into small jobs.
    // The Job System places them into a queue and does the heavy lifting of
    // multi-threaded code for you.

    public class MovementSystemJobs : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;
            float upperBounds = GameManager.Instance.UpperBounds;
            float lowerBounds = GameManager.Instance.BottomBounds;

            JobHandle jobHandle = Entities.
                WithoutBurst().
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