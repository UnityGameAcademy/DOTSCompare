using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using DOTSCompare.ECS;
using Unity.Collections;

namespace DOTSCompare
{
    // This class highlights the differences in syntax between spawning with:
    //      *Classic - using Object.Instantiate
    //      *Pure ECS - generating Entities directly from code
    //      *ECS with Conversion - use a GameObject prefab as a template for your Entities
    
    public class Spawner : MonoBehaviour
    {
        [Header("Options")]

        [SerializeField] private int initialNumToSpawn;
        [SerializeField] private float HeightRange = 1;


        [Header("Unit")]
        [SerializeField] private Mesh unitMesh;
        [SerializeField] private Material unitMaterial;
        [SerializeField] private float unitScale;
        [Space]
        [SerializeField] private GameObject ECSPrefab;
        [SerializeField] private GameObject classicPrefab;

        private int totalSpawned;
        private EntityManager entityManager;
        private World defaultWorld;
        private EntityArchetype archetype;
        private Entity unitEntityPrefab;
        private DemoMode mode;

        void Start()
        {
            if (GameManager.Instance != null)
            {
                mode = GameManager.Instance.Mode;
            }

            if (mode == DemoMode.ECSConversion)
            {
                SetupECSConversion();
            }

            if (mode == DemoMode.ECSPure)
            {
                SetupECSPure();
            }

            SpawnUnit(initialNumToSpawn);
        }

        private void SetupECSPure()
        {
            if (mode == DemoMode.ECSPure)
            {
                defaultWorld = World.DefaultGameObjectInjectionWorld;
                entityManager = defaultWorld.EntityManager;
                archetype = entityManager.CreateArchetype
                (
                    typeof(Translation),
                    typeof(Rotation),
                    typeof(RenderMesh),
                    typeof(LocalToWorld),
                    typeof(MoveForward),
                    typeof(Scale)
                );
            }
        }

        private void SetupECSConversion()
        {
            if (mode == DemoMode.ECSConversion)
            {
                defaultWorld = World.DefaultGameObjectInjectionWorld;
                entityManager = defaultWorld.EntityManager;
                GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
                unitEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ECSPrefab, settings);
            }
        }

        // not use NativeArray
        public void SpawnUnit(int numToSpawn)
        {
            totalSpawned += numToSpawn;
            GameManager.Instance.HeadsUpDisplay.ShowTotalText(totalSpawned);

            switch (mode)
            {
                case DemoMode.Classic:
                    SpawnClassic(numToSpawn);
                    break;
                case DemoMode.ECSConversion:
                    SpawnECSConversion(numToSpawn);
                    break;
                case DemoMode.ECSPure:
                    SpawnECSPure(numToSpawn);
                    break;
            }
        }

        private void SpawnClassic(int numToSpawn)
        {
            if (classicPrefab == null)
                return;

            for (int i = 0; i < numToSpawn; i++)
            {
                GameObject instance = Instantiate(classicPrefab);
                instance.transform.position = GetRandomPosition();
                instance.transform.localScale = new float3(GetRandomScale(unitScale));
            }
        }

        private void SpawnECSPure(int numToSpawn)
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                Entity myEntity = entityManager.CreateEntity(archetype);

                entityManager.AddComponentData(myEntity, new Translation { Value = GetRandomPosition() });
                entityManager.AddComponentData(myEntity, new Scale { Value = GetRandomScale(unitScale) });

                entityManager.AddSharedComponentData(myEntity, new RenderMesh
                {
                    mesh = unitMesh,
                    material = unitMaterial
                });

                entityManager.AddComponentData(myEntity, new MoveForward { speed = GameManager.Instance.moveSpeed });
            }

        }

        private void SpawnECSConversion(int numToSpawn)
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                Entity myEntity = entityManager.Instantiate(unitEntityPrefab);
                entityManager.SetComponentData(myEntity, new Translation { Value = GetRandomPosition() });

                entityManager.AddComponentData(myEntity, new MoveForward { speed = GameManager.Instance.moveSpeed });
                entityManager.AddComponentData(myEntity, new Scale { Value = GetRandomScale(unitScale) });
            }
        }

        private float GetRandomScale(float scaleMax)
        {
            const float scaleMin = 0.1f;
            return UnityEngine.Random.Range(scaleMin, scaleMax);
        }

        private float3 GetRandomPosition()
        {
            float randomX = UnityEngine.Random.Range(GameManager.Instance.LeftBounds, GameManager.Instance.RightBounds);
            float randomY = UnityEngine.Random.Range(-1f, 1f) * HeightRange;
            float randomZ = UnityEngine.Random.Range(GameManager.Instance.UpperBounds, GameManager.Instance.BottomBounds);
            return new float3(randomX, randomY, randomZ);
        }


    }
}