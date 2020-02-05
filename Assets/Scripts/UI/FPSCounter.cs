using UnityEngine;
using TMPro;

namespace DOTSCompare
{
    // A basic FPS counter; when building, the FPS maxes at 60fps
    // A good benchmark is to add enough objects on-screen until you
    // drop the FPS to a specific goal (i.e. 30 fps). 

    public class FPSCounter : MonoBehaviour
    {
        private float deltaTime = 0.0f;

        public TextMeshPro textMesh;

        // Update is called once per frame
        void Update()
        {
            // average the deltaTime for roughly ten frames (seconds per frame)
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            // frame per second
            float fps = 1.0f / deltaTime;

            // round up
            string text = Mathf.Ceil(fps).ToString();

            // set the text mesh 
            textMesh.text = text;
        }
    }

}