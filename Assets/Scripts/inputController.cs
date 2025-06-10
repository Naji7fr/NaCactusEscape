using UnityEngine;

public class InputController : MonoBehaviour
{
    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;

    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;

    [Tooltip("Turn speed in degrees/second")]
    public float turnSpeed = 300f;

    [Tooltip("Whether the character can jump")]
    public bool allowJump = true;

    [Tooltip("Upward speed to apply when jumping")]
    public float jumpSpeed = 4f;

    public bool IsGround { get; private set; }
    public float ForwardInput { get; private set; }
    public float TurnInput { get; private set; }
    public bool JumpInput { get; private set; }

    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
            enabled = false;
        }

        if (_capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider component is missing on " + gameObject.name);
            enabled = false;
        }
    }

    private void Update()
    {
        ForwardInput = Input.GetAxis("Vertical");
        TurnInput = Input.GetAxis("Horizontal");
        JumpInput = Input.GetKeyDown(KeyCode.Space);

        Debug.Log($"Forward: {ForwardInput}, Turn: {TurnInput}, Jump: {JumpInput}");
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
        Vector3 capsuleBottom = transform.TransformPoint(_capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = _capsuleCollider.radius;

        if (Physics.SphereCast(capsuleBottom, radius * 0.9f, -transform.up, out RaycastHit hit, 0.3f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                IsGround = true;
            }
        }
    }

    private void ProcessActions()
    {
        if (_rigidbody == null) return;

        // دوران اللاعب
        if (TurnInput != 0f)
        {
            float angle = TurnInput * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, angle);
        }

        // معالجة الحركة
        Vector3 verticalVelocity = _rigidbody.linearVelocity.y * Vector3.up;
        Vector3 horizontalVelocity = transform.forward * ForwardInput * moveSpeed;

        if (JumpInput && allowJump && IsGround)
        {
            verticalVelocity = Vector3.up * jumpSpeed;
        }

        _rigidbody.linearVelocity = horizontalVelocity + verticalVelocity;

        Debug.Log($"Velocity: {_rigidbody.linearVelocity}");
    }
}