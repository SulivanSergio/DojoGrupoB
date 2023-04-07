using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centro 
{

    public GameObject centro;
    
    public Centro(Mesh mesh, Material material, Vector3 posInicial)
    {

        centro = new GameObject("Centro");
        centro.AddComponent<MeshFilter>();
        centro.AddComponent<MeshRenderer>();
        centro.GetComponent<MeshFilter>().mesh = mesh;
        centro.GetComponent<MeshRenderer>().material = material;
        centro.AddComponent<CapsuleCollider>();
        centro.GetComponent<CapsuleCollider>().isTrigger = true;

        centro.tag = "Centro";

        centro.transform.localScale = new Vector3(5,0.1f,5);

        centro.transform.position = posInicial;

    }

    public void Update(float gameTime)
    {
        
    }




}
