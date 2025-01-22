
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [HideInInspector] public Vector3 movementDirection;
    float _inputX, _inputZ;
    CharacterController _characterController;
    
    // Rigidbody _rb;
    
    [SerializeField] float groundYOffset;
    [SerializeField] private LayerMask groundLayer;
    
    //Earth gravity ~9,81 m/s2
    [SerializeField] float gravity=-9.81f;
    Vector3 _velocity;

    private Vector3 _spherePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        // _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }

    void GetDirectionAndMove()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");

        movementDirection = transform.forward * _inputZ + transform.right * _inputX;
        _characterController.Move(movementDirection * (speed * Time.deltaTime));
        // _rb.MovePosition(transform.position + movementDirection * (speed * Time.deltaTime));
    }

    bool IsGrounded()
    {
        _spherePosition = new Vector3(transform.position.x,transform.position.y - groundYOffset, transform.position.z);
        //Player touching ground
        if (Physics.CheckSphere(_spherePosition,_characterController.radius-0.05f, groundLayer)) return true;
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
        Gizmos.DrawWireSphere(_spherePosition,_characterController.radius-0.05f);
    }
}