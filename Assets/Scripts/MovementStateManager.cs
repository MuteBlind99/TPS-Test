using UnityEngine;


public class MovementStateManager : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 10f;
    [HideInInspector] public Vector3 movementDirection;
    float _inputX, _inputZ;


    // Rigidbody _rb;

    [SerializeField] float groundYOffset;
    [SerializeField] private LayerMask groundLayer;

    //Earth gravity ~9,81 m/s2
    [SerializeField] float gravity = -9.81f;
    Vector3 _velocity;

    private Vector3 _spherePosition;

    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float fastTurnSpeed = 200f;

    [SerializeField] private bool isRootMotionned = false;
    [SerializeField] private Transform rootCharacter;

    private CharacterInputController _inputs;
    private CharacterController _characterController;
    private Animator _animator;

    private float _angleVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = GetComponent<CharacterInputController>();
        _characterController = GetComponentInChildren<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        // _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRootMotionned)
        {
            if (_inputs.Move.magnitude >= Mathf.Epsilon)
            {
                if (!_inputs.IsAiming)
                {
                    // Not aiming : Rotation
                    // float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
                    // targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;
                    //
                    // float actualAngle =
                    //     Mathf.SmoothDampAngle(rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);
                    //
                    // rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);
                    //
                     float horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;
                    _animator.SetFloat("Speed", _inputs.Move.magnitude * horizontalSpeed);
                }
                else
                {
                    _animator.SetFloat("Strafe", _inputs.Move.x);
                    _animator.SetFloat("Speed", _inputs.Move.y * walkSpeed);
                }
            }
            else
            {
                _animator.SetFloat("Strafe", 0f);
                _animator.SetFloat("Speed", 0f);
            }
        }
        else
        {
            float _turnSpeed = _inputs.IsRunning ? fastTurnSpeed : this.turnSpeed;
            transform.Rotate(Vector3.up, _inputs.Move.x * _turnSpeed * Time.deltaTime);

            float horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;
            _characterController.SimpleMove(transform.forward * (_inputs.Move.y * horizontalSpeed));

            _animator.SetFloat("Speed", _characterController.velocity.magnitude);
        }

        GetDirectionAndMove();
        Gravity();
    }

    void GetDirectionAndMove()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");

        float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
        targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;
        
        float actualAngle =
            Mathf.SmoothDampAngle(rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);
        
        rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);


        movementDirection = Camera.main.transform.forward * _inputZ + Camera.main.transform.right * _inputX;
        _characterController.Move(movementDirection * (walkSpeed * Time.deltaTime));
        // _rb.MovePosition(transform.position + movementDirection * (speed * Time.deltaTime));
    }

    bool IsGrounded()
    {
        _spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        //Player touching ground
        if (Physics.CheckSphere(_spherePosition, _characterController.radius, groundLayer)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) _velocity.y += gravity * Time.deltaTime;

        else if (_velocity.y < 0) _velocity.y = -2;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spherePosition, _characterController.radius);
    }
}