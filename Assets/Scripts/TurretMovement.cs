using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace DapperDino.BuildingBlocks
{
    public class TurretMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private Vector2 limits = new Vector2(-4f, 4f);

        private bool Axis = false;
        private bool Vector2 = false;

        private float previousInputFloat;
        private Vector2 previousInputVector2;

        private void Update() => Move();

        public void MoveInput(CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (ctx.valueType.ToString() == "System.Single")
                {
                    previousInputFloat = ctx.ReadValue<float>();
                    if(!Axis)
                    {
                        Axis = true;
                    }
                }               
                else if (ctx.valueType.ToString() == "UnityEngine.Vector2")
                {
                    previousInputVector2 = ctx.ReadValue<Vector2>();
                    if (!Vector2)
                    {
                        Vector2 = true;
                    }
                }
                return;
            }

            if (ctx.canceled)
            {
                previousInputFloat = 0f;
                previousInputVector2 = UnityEngine.Vector2.zero;
            }
        }

        private void Move()
        {
            if (Axis)
            {
                Move1D();
            }
            else if (Vector2)
            {
                Move2D();
            }
        }

        private void Move1D()
        {
            float movement = previousInputFloat * movementSpeed * Time.deltaTime;
            float newXValue = Mathf.Clamp(transform.position.x + movement, limits.x, limits.y);

            transform.position = new Vector3(newXValue, transform.position.y, transform.position.z);
        }

        private void Move2D()
        {

        }
    }
}
