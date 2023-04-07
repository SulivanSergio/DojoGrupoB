using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centro 
{

    public GameObject centro;
    
    public Centro(Mesh mesh, Material material)
    {

        centro = new GameObject("Centro");
        centro.AddComponent<MeshFilter>();
        centro.AddComponent<MeshRenderer>();
        centro.GetComponent<MeshFilter>().mesh = mesh;
        centro.GetComponent<MeshRenderer>().material = material;

        centro.transform.localScale = new Vector3(5,0.1f,5);

    }

    public void Update(float gameTime)
    {
        
    }




}
