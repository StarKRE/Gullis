using UnityEngine;

namespace Gameknit
{
    public sealed class MonoMotor : MonoBehaviour
    {
        #region Const

        private const float MIN_TOLERANCE = 0.001f;
        
        private const float ZERO = 0.0f;

        #endregion
        
        [Header("Speed")]
        [SerializeField]
        public float maxSpeed;

        [SerializeField]
        public float currentSpeed;

        [SerializeField]
        public float minSpeed;

        [Header("Acceleration")]
        [SerializeField]
        public float maxAcceleration;

        [SerializeField]
        public float currentAcceleration;

        [Header("Drag")]
        [SerializeField]
        public float linearDrag;

        public void AddForce(float coefficient)
        {
            this.currentAcceleration += this.maxAcceleration * coefficient;
            if (Mathf.Abs(this.currentAcceleration) >= this.maxAcceleration)
            {
                var accelerationSign = Mathf.Sign(this.currentAcceleration);
                this.currentAcceleration = accelerationSign * this.maxAcceleration;
            }
        }

        public float NextSpeed()
        {
            this.currentSpeed += this.currentAcceleration;
            if (Mathf.Abs(this.currentSpeed) >= this.maxSpeed)
            {
                this.currentSpeed = Mathf.Sign(this.currentSpeed) * this.maxSpeed;
            }

            if (Mathf.Abs(this.currentAcceleration) <= MIN_TOLERANCE)
            {
                var speedSign = Mathf.Sign(this.currentSpeed);
                if (Mathf.Abs(this.currentSpeed) - this.linearDrag > this.minSpeed)
                {
                    this.currentSpeed -= speedSign * this.linearDrag;
                }
                else
                {
                    this.currentSpeed = speedSign * this.linearDrag;
                }
            }

            this.currentAcceleration = ZERO;
            return this.currentSpeed;
        }

        public void Reset()
        {
            this.currentAcceleration = ZERO;
            this.currentSpeed = ZERO;
        }
    }
}