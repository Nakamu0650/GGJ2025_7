using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbColorChange : MonoBehaviour
{
    [SerializeField] OrbScript orbScript;

    Material material;
    MeshRenderer mesh;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Material>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Lerp(0, 1, orbScript.HpAmount());
        mesh.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, alpha));  
    }
}
