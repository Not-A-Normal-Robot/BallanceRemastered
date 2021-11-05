using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class NPController : MonoBehaviour
{
    public BallType type;
    public new Rigidbody rigidbody;
    public new Renderer renderer;
    void Awake()
    {
        if(rigidbody == null)
        {
            rigidbody = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        }
        if(renderer == null)
        {
            renderer = gameObject.GetComponent(typeof(Renderer)) as Renderer;
        }
        if(type == null)
        {
            Debug.LogError($"Ball type not set. Location: {transform.position.x}, {transform.position.y}, {transform.position.z}");
        }
        else
        {
            rigidbody.mass = type.mass;
            renderer.material = type.material;
        }
        Destroy(this);
    }
}
