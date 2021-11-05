using UnityEngine;
[CreateAssetMenu(fileName = "Ball_Name", menuName = "ScriptableObjects/BallType")]
public class BallType : ScriptableObject
{
    public int id;
    public new string name;
    public float mass;
    public float speed;
    public Material material;

    /// <summary>
    /// Create a ball type with a mass, speed, and a texture.
    /// </summary>
    /// <param name="speed">Mass decreases the speed, so if you increase mass you don't have to decrease speed.</param>
    public BallType(Material material, float mass = 0.75f, float speed = 7f)
    {
        this.mass = mass;
        this.speed = speed;
        this.material = material;
    }
    public BallType()
    {
        mass = 0.75f;
        speed = 7f;
    }
}