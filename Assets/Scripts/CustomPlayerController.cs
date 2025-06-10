using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    public float moveSpeed = 5f;
    public float turnSpeed = 300f;
    public bool allowJump = true;
    public float jumpSpeed = 4f;

    public AudioSource jumpSound; // <<<<< جديد: متغير لصوت القفز

    public bool IsGround { get; private set; }
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;
    private LayerMask groundLayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        if (_rigidbody == null || _capsuleCollider == null)
        {
            Debug.LogError("Rigidbody or CapsuleCollider missing on " + gameObject.name);
            enabled = false;
            return;
        }

        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        ForwardInput = Input.GetAxis("Vertical");
        TurnInput = Input.GetAxis("Horizontal");
        JumpInput = JumpInput || Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ProcessActions();
    }

    private void CheckGrounded()
    {
        IsGround = false;
        if (_capsuleCollider == null) return;

        float capsuleHeight = Mathf.Max(_capsuleCollider.radius * 2f, _capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(_capsuleCollider.center - Vector3.up * capsuleHeight / 2f + Vector3.up * 0.1f);
        float radius = _capsuleCollider.radius * 0.9f;

        IsGround = Physics.CheckSphere(capsuleBottom, radius, groundLayer);
    }

    private void ProcessActions()
    {
        if (_rigidbody == null) return;

        // دوران
        if (Mathf.Abs(TurnInput) > 0.01f)
        {
            float angle = TurnInput * turnSpeed * Time.fixedDeltaTime;
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, angle, 0f));
        }

        // حركة أمامية
        Vector3 velocity = _rigidbody.linearVelocity;
        Vector3 horizontalVelocity = transform.forward * ForwardInput * moveSpeed;
        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.z;

        // قفز
        if (JumpInput && allowJump && IsGround)
        {
            velocity.y = jumpSpeed;

            if (jumpSound != null)  // <<<<< تشغيل صوت القفز
                jumpSound.Play();

            JumpInput = false;
        }

        _rigidbody.linearVelocity = velocity;
    }
}
