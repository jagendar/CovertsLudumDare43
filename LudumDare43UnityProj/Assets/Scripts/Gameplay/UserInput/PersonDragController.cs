using Assets.Scripts.Gameplay.People;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class PersonDragController : MonoBehaviour
    {
        [SerializeField] private LayerMask droppableLayers;

        private PersonAI dragTarget;

        public bool IsDragging { get; private set; }

        public void SubComponentUpdate(PlayerController controller)
        {
            // TODO: Refactor this short circuit logic so its not so repetitive
            if (Input.GetMouseButtonDown(0))
            {
                dragTarget = controller.UnderCursor.Person;
                IsDragging = dragTarget != null;

                if (dragTarget != null)
                {
                    dragTarget.Grabbed();
                }
            }

            if (dragTarget == null)
            {
                IsDragging = false;
            }

            if (!IsDragging) return;

            Vector3 personOffset = new Vector3(0, -50, 0);
            Ray personRay = Camera.main.ScreenPointToRay(Input.mousePosition + personOffset);
            dragTarget.transform.position = personRay.origin + personRay.direction * 5;

            if (Input.GetMouseButtonUp(0))
            {
                dragTarget.DroppedOn(controller.UnderCursor);

                IsDragging = false;
            }
        }
    }
}
