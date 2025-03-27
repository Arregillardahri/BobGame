using Unity.Mathematics;
using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField] AudioSource Bobjumpsound;
    
    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private new Collider2D collider;
    
    private Vector2 Velocity;
    private float inputaxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;


    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded {  get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(Velocity.x) > 0.25f || Mathf.Abs(inputaxis) > 0.25f;
    public bool sliding => (inputaxis > 0f && Velocity.x < 0f)|| (inputaxis < 0f && Velocity.x > 0f);


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        camera = Camera.main;
    }
    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        Velocity = Vector2.zero;
        jumping = true;
    }
    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        Velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {

        horizontalmovement();

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded) 
        {
            
            GroundedMovement();
        
        }

        ApplyGravity();
        
    }
    private void horizontalmovement()
    {
        inputaxis = Input.GetAxis("Horizontal");
        Velocity.x = Mathf.MoveTowards(Velocity.x, inputaxis * moveSpeed, moveSpeed * Time.deltaTime);
    
        if(rigidbody.Raycast(Vector2.right * Velocity.x))
        {
            Velocity.x = 0f;
        }

        if (Velocity.x >= 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (Velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement() 
    {
        Velocity.y = Mathf.Max(Velocity.y, 0f);
        jumping = Velocity.y > 0f;

        if (Input.GetButtonDown("Jump")) 
        {
            Bobjumpsound.Play();
            Velocity.y = jumpForce;
            jumping = true;
        }
    
    }

    private void ApplyGravity() 
    {
        bool falling = Velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        Velocity.y += gravity * multiplier * Time.deltaTime;
        Velocity.y = Mathf.Max(Velocity.y, gravity / 2f);
    
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += Velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.HeadHit(collision.transform, Vector2.down))
            {
                Velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }

        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.HeadHit(collision.transform, Vector2.up))
            {
                Velocity.y = 0f;
            }
        }

    }
     

}
