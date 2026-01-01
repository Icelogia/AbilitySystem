using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities.Projectiles
{
    using Attributes;
    using Data;

    public class SimpleProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;

        [SerializeField] private SimpleProjectileData data;

        private AttributeSet owner;
        private Vector3 newPosition;
        private float currentSpeed = 0f;
        private float lifeTimer = 0f;

        private void Update()
        {
            if (data.Lifetime > 0f && lifeTimer >= data.Lifetime)
                Destroy();

            lifeTimer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if(currentSpeed < data.MaxSpeed)
            {
                currentSpeed = Mathf.Clamp(currentSpeed + data.Acceleration, 0f, data.MaxSpeed);
            }

            newPosition = rb.position;
            newPosition += transform.forward * currentSpeed;
            rb.MovePosition(newPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            var attributeSet = other.GetComponent<AttributeSet>();

            if (attributeSet != null)
                attributeSet.Apply(data.Effector, owner);

            Destroy();
        }

        public void SetDirection(Vector3 direction)
        {
            transform.forward = direction;
        }

        public void SetOwner(AttributeSet owner)
        {
            this.owner = owner;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}