using UnityEngine;
using gasstation.code.core.player;
using gasstation.code.core.objects;
using gasstation.code.core.logging;

namespace gasstation.code.core.camera
{
    public class FollowPlayerCamera : SingletonMonobehaviour
    {
        public float xOffset = 0f;
        public float yOffset = 0f;
        public bool followEnabled = true;
        private float originalSize = 0f;
        private float zoomFactor = 1.0f;
        private float zoomSpeed = 5.0f;

        private void Start()
        {
            this.originalSize = Camera.main.orthographicSize;
        }

        public static FollowPlayerCamera GetCamera()
        {
            return (FollowPlayerCamera)FollowPlayerCamera.getInstance("FollowPlayerCamera");
        }

        private void FixedUpdate()
        {
            if (followEnabled && Player.getInstance("Player") != null)
            {
                Camera.main.transform.position = new Vector3(
                    Player.getInstance("Player").gameObject.transform.position.x + this.xOffset,
                    Player.getInstance("Player").gameObject.transform.position.y + this.yOffset,
                    Camera.main.transform.position.z
                );
            }
            //ScrollZoom();

        }

        private void ScrollZoom()
        {

            float targetSize = originalSize * zoomFactor;
            if (targetSize != Camera.main.orthographicSize)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
                        targetSize, Time.deltaTime * zoomSpeed);
            }

        }

        public override void AfterStart()
        {

        }

        public void DisablePlayerFollow()
        {
            this.followEnabled = false;
        }

        public void EnablePlayerFollow()
        {
            this.followEnabled = true;
        }
    }
}