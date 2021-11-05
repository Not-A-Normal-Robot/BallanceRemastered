using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Material))]
[RequireComponent(typeof(Collider))]
public class MaterialTiler : MonoBehaviour
{
    private Material material;
    private new Collider collider;
    public Vector3 tileMultiplier;
    private void Awake()
    {
        material = new Material((gameObject.GetComponent(typeof(Renderer)) as Renderer).material);
        collider = gameObject.GetComponent(typeof(Collider)) as Collider;
        material.mainTextureScale = collider.bounds.size;

        (gameObject.GetComponent(typeof(Renderer)) as Renderer).material = material;
    }
}
