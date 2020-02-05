using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Entities;
using DOTSCompare.ECS;

[System.Serializable]
public enum DemoMode
{
    Classic,
    ECSPure,
    ECSConversion
}

// This script compares the performance between the "classic" approach 
// (generating and moving simple objects with Monobehaviours) versus the
// DOTS/ECS approach (using Entities/Components/Systems)
//
// Press the spacebar to generate a number of randomly placed primitives.
// The field of cubes is in constant motion and "wraps" around the screen.

// Stayed tuned for future examples, where we introduce basic shmup-like gameplay!
// The included heads-up display will help you see what mode that the demo is
// currently using.  The SetSystemMode method toggles the various ECS Systems on/off
// as appropriate.

namespace DOTSCompare
{
    [RequireComponent(typeof(HUD))]
    public class GameManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private DemoMode mode;
        [SerializeField] bool useJobs;
        [SerializeField] bool useBurst;


        [Header("Spawn")]
        [SerializeField] private Spawner spawner;
        [SerializeField] private int spawnIncrement;

        [Header("Boundaries")]
        [SerializeField] private float upperBounds;
        [SerializeField] private float leftBounds;
        [SerializeField] private float rightBounds;
        [SerializeField] private float bottomBounds;

        private HUD hud;

        public float UpperBounds => upperBounds;
        public float BottomBounds => bottomBounds;
        public float LeftBounds => leftBounds;
        public float RightBounds => rightBounds;
        public DemoMode Mode => mode;
        public HUD HeadsUpDisplay => hud;

        public float moveSpeed;

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            spawner = Object.FindObjectOfType<Spawner>();
            hud = GetComponent<HUD>();

        }

        private void Start()
        {
            SetSystemMode();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spawner?.SpawnUnit(spawnIncrement);
            }
        }


        private void SetSystemMode()
        {
            hud.ShowBurstText(useBurst);
            hud.ShowJobsText(useJobs);

            World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<MovementSystem>().Enabled = false;
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<MovementSystemJobs>().Enabled = false;
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<MovementSystemJobsBurst>().Enabled = false;

            if (mode != DemoMode.Classic)
            {
                if (!useJobs)
                {
                    World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<MovementSystem>().Enabled = true;
                    Debug.Log("Enabling MovementSystem");
                }
                else if (useBurst)
                {
                    World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<MovementSystemJobsBurst>().Enabled = true;
                    Debug.Log("Enabling MovementSystemJobsBurst");
                }
                else if (!useBurst)
                {
                    World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<MovementSystemJobs>().Enabled = true;
                    Debug.Log("Enabling MovementSystemJobs");
                }

            }
        }
    }
}