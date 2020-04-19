using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace DapperDino.BuildingBlocks
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private float offset = 0f;
        [SerializeField] private float speed = 0f;
        [SerializeField] private float distance = 0f;
        [SerializeField] private GameObject cursor;

        private Ray aim;

        private void Awake()
        {

        }

        public void Rotate(CallbackContext ctx)
        {
            if (!ctx.performed) { return; }

            RaycastHit hit;
            var aimInput = ctx.ReadValue<Vector2>();
            
            aim = Camera.main.ScreenPointToRay(aimInput);
            Vector3 mousePosition = aimInput;
            mousePosition.y += offset;
            mousePosition.z = distance;
            cursor.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

            if(Physics.Raycast(aim, out hit, Mathf.Infinity))
            {
                float finalOffset;
                finalOffset = (transform.position - hit.point).magnitude * offset;
                cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y + finalOffset, cursor.transform.position.z);
                transform.LookAt(cursor.transform);
            }
            else
            {
                transform.LookAt(cursor.transform);
            }
        }
    }
}
