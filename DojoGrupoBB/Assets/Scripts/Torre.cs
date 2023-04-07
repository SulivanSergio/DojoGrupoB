using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre 
{

    public GameObject torre;

    public Torre(Mesh mesh, Material material)
    {

        torre = new GameObject("Torre");
        torre.AddComponent<MeshFilter>();
        torre.AddComponent<MeshRenderer>();
        torre.GetComponent<MeshFilter>().mesh = mesh;
        torre.GetComponent<MeshRenderer>().material = material;

        torre.transform.position = new Vector3(10,0,10);
        torre.transform.localScale = new Vector3(1, 3, 1);



    }

    
    public void Update(float gameTime)
    {


        
    }



}
