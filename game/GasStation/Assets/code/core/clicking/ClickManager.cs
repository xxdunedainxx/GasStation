using gasstation.code.core.objects;
using gasstation.code.core.logging;

using UnityEngine;
namespace gasstation.code.core.clicking
{
    public class ClickManager : SingletonMonobehaviour
    {

        public static ClickManager instance { get; private set; }
        public Camera cam = null;

        private void Start()
        {
            if (this.cam == null)
            {
                LogFactory.INFO("click manager cam is not set, defaulting to main camera");
                this.cam = Camera.main;
            }
        }

        private GameObject GetHighestRaycastTarget(Vector2 mousePos)
        {
            GameObject topLayer = null;
            RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero);

            foreach (RaycastHit2D ray in hit)
            {
                SpriteRenderer spriteRenderer = ray.transform.GetComponent<SpriteRenderer>();

                // Check if RaycastTarget has a SpriteRenderer and
                if (spriteRenderer != null)
                {
                    if (topLayer == null)
                    {
                        topLayer = spriteRenderer.transform.gameObject;
                    }

                    if (spriteRenderer.sortingOrder > topLayer.GetComponent<SpriteRenderer>().sortingOrder)
                    {
                        topLayer = ray.transform.gameObject;
                    }
                }

            }

            return topLayer;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                GameObject hit = this.GetHighestRaycastTarget(mousePosition);
                //RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                LogFactory.DEBUG("ClickManager is detecting if a clickable was clicked...");

                if (hit)
                {
                    LogFactory.INFO("something hit?");
                    IClickable clickable = hit.GetComponent<IClickable>();
                    LogFactory.INFO($"Clicked {clickable} { hit.gameObject} clickable? {clickable?.ClickEnabled()}");
                    if (clickable?.ClickEnabled() == true)
                    {
                        clickable?.click();
                    }
                }
            }
        }

        public override void AfterStart()
        {

        }
    }
}