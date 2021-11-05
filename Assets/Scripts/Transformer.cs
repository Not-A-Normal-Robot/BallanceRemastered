using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Transformer : MonoBehaviour
{
    public BallType type;
    public new Collider collider;
    public Renderer blockRenderer;
    public GameObject lightning;
    public new Light light;
    public GameObject mechanism;
    public Sound sound;
    void Awake()
    {
        if (collider == null)
        {
            collider = gameObject.GetComponent(typeof(Collider)) as Collider;
        }
        if(light == null)
        {
            light = lightning.GetComponent(typeof(Light)) as Light;
        }
        blockRenderer.material = type.material;
        gameObject.tag = type.material.name;
        lightning.SetActive(false);
        light.intensity = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransformPlayer(other));
        }
    }
    public IEnumerator TransformPlayer(Collider player)
    {
        Vector3 oriPos = mechanism.transform.position;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = transform.position;
        float t = 0;
        PlayerController controller = player.GetComponent(typeof(PlayerController)) as PlayerController;
        if(controller.type == type.id)
        {
            yield break;
        }
        sound.Play(gameObject);
        // Move player to transformer
        Rigidbody playerRb = player.GetComponent(typeof(Rigidbody)) as Rigidbody;
        controller.paralyzed = true;
        playerRb.isKinematic = true;
        while(t < 1)
        {
            t += Time.deltaTime * 5;
            player.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        // Move mechanism up
        lightning.SetActive(true);
        light.intensity = 1;
        t = 0;
        startPos = mechanism.transform.position;
        endPos = mechanism.transform.position + Vector3.up;
        while (t < 1)
        {
            t += Time.deltaTime;
            mechanism.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        mechanism.transform.position = endPos;
        yield return new WaitForSeconds(0.3f);
        // Change ball type
        controller.type = type.id;
        controller.OnTransform();
        // Move mechanism down
        t = 0;
        startPos = mechanism.transform.position;
        while (t < 1)
        {
            t += Time.deltaTime;
            mechanism.transform.position = Vector3.Lerp(startPos, oriPos, t);
            yield return null;
        }
        mechanism.transform.position = oriPos;
        lightning.SetActive(false);
        light.intensity = 0;
        playerRb.isKinematic = false;
        controller.paralyzed = false;
    }
}
