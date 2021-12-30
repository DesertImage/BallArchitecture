using UnityEngine;
using UnityEngine.EventSystems;

namespace DesertImage.UI
{
    public class WorldSwipeScrollList : WorldScrollList, IEndDragHandler
    {
        [SerializeField] private float minDistanceToSwipe = 200;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            StartDragPos = eventData.position;
        }

        public override void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var delta = eventData.position - (Vector2)StartDragPos;

            var xAbs = Mathf.Abs(delta.x);
            var yAbs = Mathf.Abs(delta.y);

            if
            (
                Horizontal && (xAbs < minDistanceToSwipe || yAbs >= minDistanceToSwipe) ||
                Vertical && (yAbs < minDistanceToSwipe || xAbs >= minDistanceToSwipe)
            ) return;

            if (yAbs > xAbs)
            {
                if (delta.y > 0f)
                {
                    Next();
                }
                else
                {
                    Previous();
                }
            }
            else
            {
                if (delta.x > 0f)
                {
                    Previous();
                }
                else
                {
                    Next();
                }
            }
        }
    }
}