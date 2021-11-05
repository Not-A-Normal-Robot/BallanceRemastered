using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Controls controls;
    public GameObject orbitPoint;
    public Camera cam;
    // default: 0.4
    public float distanceMult;
    // default: 15
    public float xzDistance;
    /// <summary>
    /// 0: +x, 1: +z, 2: -x, 3: -z
    /// </summary>
    [Range(0,3)]
    public int state;
    public bool zoomedOut;
    public Vector3 CamTargetPos
    {
        get
        {
            return orbitPoint.transform.position + ((state == 0 ? new Vector3(xzDistance, 10, 0) : state == 1 ? new Vector3(0, 10, xzDistance) : state == 2 ? new Vector3(-xzDistance, 10, 0) : new Vector3(0, 10, -xzDistance)) * distanceMult);
        }
    }
    void Awake()
    {
        controls = new Controls();
        controls.Game.ZoomOut.performed += _ => ZoomOut();
        controls.Game.CameraLeft.performed += _ => RotateLeft();
        controls.Game.CameraRight.performed += _ => RotateRight();
        if (orbitPoint == null)
        {
            Debug.LogError("orbitPoint is null");
            return;
        }
        if (cam == null)
        {
            cam = gameObject.GetComponent(typeof(Camera)) as Camera;
            if (cam == null)
            {
                Debug.LogError("Camera not found!");
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        cam.transform.position = CamTargetPos;
        if(orbitPoint == null)
        {
            cam.transform.LookAt(Vector3.zero);
        }
        else
        {
            cam.transform.LookAt(orbitPoint.transform);
        }
    }
    void ZoomOut()
    {
        if (zoomedOut)
        {
            distanceMult = 0.4f;
            xzDistance = 15f;
            zoomedOut = false;
        }
        else
        {
            distanceMult = 0.7f;
            xzDistance = 5f;
            zoomedOut = true;
        }
    }
    void RotateLeft()
    {
        if(--state < 0)
        {
            state = 3;
        }
    }
    void RotateRight()
    {
        if(++state > 3)
        {
            state = 0;
        }
    }
    public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }
}