using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Mesh> Meshes;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshFilter>().mesh = this.Meshes[Random.Range(0, this.Meshes.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
