using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBPlayerController : MonoBehaviour
{
    
    public float Speed;
    public Transform Cam;
    private Rigidbody _rigidbody;
    private Vector3 input = new Vector3();
    private bool _isGrounded = true;
    private Renderer _renderer;
    private Vector3 _groundedCheckMiddle;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float _rotationSpeed = 20f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _groundCheckRadius = 0.6f;


  
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y)
                , ForceMode.VelocityChange);
        }
        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
           


    }

    private void FixedUpdate()
    {
        _groundedCheckMiddle = _renderer.bounds.center;
        _groundedCheckMiddle.y -= _renderer.bounds.extents.y * 0.60f;
        _isGrounded = Physics.CheckSphere(_groundedCheckMiddle, _groundCheckRadius,
            groundMask, QueryTriggerInteraction.Ignore);

        _rigidbody.MovePosition(_rigidbody.position + input.normalized * (speed * Time.deltaTime));
        
        if (input != Vector3.zero)
        { 
           Quaternion toRotation = Quaternion.LookRotation(input, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(_groundedCheckMiddle, _groundCheckRadius);
    }
}
