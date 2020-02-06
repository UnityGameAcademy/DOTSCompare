using UnityEngine;
using TMPro;

namespace DOTSCompare
{
    // Shows basic text on-screen with TextMeshPro.
    public class HUD : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TextMeshPro spawnTotalTextMesh;
        [SerializeField] private TextMeshPro modeTextMesh;
        [SerializeField] private TextMeshPro jobsTextMesh;
        [SerializeField] private TextMeshPro burstTextMesh;

        private void Start()
        {
            ShowModeText();
        }

        public void ShowTotalText(int totalSpawned)
        {
            if (spawnTotalTextMesh != null)
                spawnTotalTextMesh.text = totalSpawned.ToString();
        }

        public void ShowModeText()
        {
            if (modeTextMesh != null)
            {
                switch (GameManager.Instance.Mode)
                {
                    case DemoMode.Classic:
                        modeTextMesh.text = "Classic";
                        break;
                    case DemoMode.ECSConversion:
                        modeTextMesh.text = "ECS Conversion";
                        break;
                    case DemoMode.ECSPure:
                        modeTextMesh.text = "ECS Pure";
                        break;
                }
            }
        }

        public void ShowBurstText(bool state)
        {
            if (burstTextMesh != null)
            {
                burstTextMesh.text = (state) ? "true" : "false";
            }

            burstTextMesh.transform.parent.gameObject.SetActive((GameManager.Instance.Mode != DemoMode.Classic));

        }

        public void ShowJobsText(bool state)
        {
            if (jobsTextMesh != null)
            {
                jobsTextMesh.text = (state) ? "true" : "false";
            }

            jobsTextMesh.transform.parent.gameObject.SetActive((GameManager.Instance.Mode != DemoMode.Classic));

            
        }



    }
}