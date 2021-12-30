using DesertImage.Extensions;
using Framework.Entities;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class ClickableExtension : EntityExtension, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Entity?.SendEvent(new PointerDownEvent {Value = monoEntity});

            this.SendGlobalEvent(new PointerDownEvent {Value = monoEntity});
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Entity?.SendEvent(new PointerUpEvent {Value = monoEntity});

            this.SendGlobalEvent(new PointerUpEvent {Value = monoEntity});
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Entity?.SendEvent(new ClickedEvent {Value = monoEntity});

            this.SendGlobalEvent(new ClickedEvent {Value = monoEntity});
        }
    }
}