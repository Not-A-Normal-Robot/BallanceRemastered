using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject fan;
    void Update()
    {
        fan.transform.rotation = Quaternion.Euler(fan.transform.rotation.eulerAngles.x, fan.transform.rotation.eulerAngles.y + Time.deltaTime * 1500, fan.transform.rotation.eulerAngles.z);
    }
    private void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddForce(new Vector3(-other.attachedRigidbody.velocity.x * 0.05f, 0, -other.attachedRigidbody.velocity.z * 0.05f), ForceMode.Force);
        other.attachedRigidbody.AddForce(Vector3.up * 0.5f, ForceMode.Force);
    }
}
