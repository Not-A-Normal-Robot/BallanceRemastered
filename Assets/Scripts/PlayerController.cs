using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(BallAudio))]
public class PlayerController : MonoBehaviour
{
    public Controls controls;
    public GameCamera cam;
    public new Renderer renderer;
    public new BallAudio audio;
    public float movementSpeed;
    public bool paralyzed;
    public Vector2 RotDirection 
    {get{
            if (paralyzed)
            {
                return Vector2.zero;
            }
            if(upForce < 0.01f && downForce < 0.01f && leftForce < 0.01f && rightForce < 0.01f)
            {
                return Vector2.zero;
            }
            else if(upForce < 0.01f && downForce < 0.01f)
            {
                return cam.state == 0 ? new Vector2(0, rightForce - leftForce).normalized:
                       cam.state == 1 ? new Vector2(leftForce - rightForce, 0).normalized:
                       cam.state == 2 ? new Vector2(0, leftForce - rightForce).normalized:
                                        new Vector2(rightForce - leftForce, 0).normalized;
            }
            else if(leftForce < 0.01f && rightForce < 0.01f)
            {
                return cam.state == 0 ? new Vector2(downForce - upForce, 0).normalized:
                       cam.state == 1 ? new Vector2(0, downForce - upForce).normalized:
                       cam.state == 2 ? new Vector2(upForce - downForce, 0).normalized:
                                        new Vector2(0, upForce - downForce).normalized;
            }
            return cam.state == 0 ? new Vector2(downForce - upForce, rightForce - leftForce).normalized:
                   cam.state == 1 ? new Vector2(leftForce - rightForce, downForce - upForce).normalized:
                   cam.state == 2 ? new Vector2(upForce - downForce, leftForce - rightForce).normalized:
                                    new Vector2(rightForce - leftForce, upForce - downForce).normalized;
        }}
    public new Rigidbody rigidbody;
    public float upForce = 0f;
    public float downForce = 0f;
    public float leftForce = 0f;
    public float rightForce = 0f;
    private int prevType = 0;
    public BallType _type;
    public int type = 0;
    public BallType[] types;
    public Vector3 respawnPos;

    void Awake()
    {
        controls = new Controls();
        controls.Game.Up   .performed += Up;
        controls.Game.Down .performed += Down;
        controls.Game.Left .performed += Left;
        controls.Game.Right.performed += Right;
        if (rigidbody == null)
        {
            rigidbody = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        }
        if(renderer == null)
        {
            renderer = gameObject.GetComponent(typeof(Renderer)) as Renderer;
        }
        if(audio == null)
        {
            audio = gameObject.GetComponent(typeof(BallAudio)) as BallAudio;
        }
        rigidbody.maxAngularVelocity = float.PositiveInfinity;
        respawnPos = transform.position;
        for(int i = 0; i < types.Length; i++)
        {
            if(types[i].id == type)
            {
                _type = types[i];
            }
        }
    }
    void Update()
    {
        if(RotDirection != new Vector2(0, 0))
        {
            rigidbody.AddForce(Time.deltaTime * 60 * movementSpeed * new Vector3(RotDirection.x, 0, RotDirection.y), ForceMode.Force);
        }
        if(Mathf.Abs(rigidbody.angularVelocity.x) <= 0.01) { rigidbody.angularVelocity = new Vector3(0, rigidbody.angularVelocity.y, rigidbody.angularVelocity.z); }
        if(Mathf.Abs(rigidbody.angularVelocity.y) <= 0.01) { rigidbody.angularVelocity = new Vector3(rigidbody.angularVelocity.x, 0, rigidbody.angularVelocity.z); }
        if(Mathf.Abs(rigidbody.angularVelocity.z) <= 0.01) { rigidbody.angularVelocity = new Vector3(rigidbody.angularVelocity.x, rigidbody.angularVelocity.y, 0); }

        if(Mathf.Abs(rigidbody.velocity.x) <= 0.01) { rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z); }
        if(Mathf.Abs(rigidbody.velocity.y) <= 0.01) { rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z); }
        if(Mathf.Abs(rigidbody.velocity.z) <= 0.01) { rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0); }

        if (gameObject.transform.position.y < -10)
        {
            transform.position = respawnPos;
        }
        if(type != prevType)
        {
            prevType = type;
            for (int i = 0; i < types.Length; i++)
            {
                if(types[i].id == type)
                {
                    rigidbody.mass = types[i].mass;
                    movementSpeed = types[i].speed;
                    renderer.material = types[i].material;
                    break;
                }
            }
        }
    }
    private void Up(InputAction.CallbackContext ctx)
    {
        upForce = ctx.ReadValue<float>();
    }

    private void Down(InputAction.CallbackContext ctx)
    {
        downForce = ctx.ReadValue<float>();
    }

    private void Left(InputAction.CallbackContext ctx)
    {
        leftForce = ctx.ReadValue<float>();
    }

    private void Right(InputAction.CallbackContext ctx)
    {
        rightForce = ctx.ReadValue<float>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    public void OnTransform()
    {
        for (int i = 0; i < types.Length; i++)
        {
            if (types[i].id == type)
            {
                _type = types[i];
            }
        }
        audio.OnTransform();
    }
}
