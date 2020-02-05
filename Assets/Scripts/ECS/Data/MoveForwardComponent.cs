using Unity.Entities;

// Components in ECS are simple structs that hold data
namespace DOTSCompare.ECS
{
    public struct MoveForward : IComponentData
    {
        public float speed;
    }

}