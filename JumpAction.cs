using UnityEngine;

namespace Apps.Scripts
{
    public struct JumpInfo
    {
        public Rigidbody rb;
        public float JumpForce;
        public bool IsGrounded;
    }

    public class JumpAction
    {
        public void Execute(JumpInfo info)
        {
            if (!info.IsGrounded) return;

            info.rb.linearVelocity = new Vector3(
                info.rb.linearVelocity.x,
                0f,
                info.rb.linearVelocity.z
            );

            info.rb.AddForce(
                Vector3.up * info.JumpForce,
                ForceMode.Impulse
            );
        }
    }
}