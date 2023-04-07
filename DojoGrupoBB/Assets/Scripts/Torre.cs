using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre 
{

    public GameObject torre;
    public bool conquista;
    public Color color = Color.white;
    public float tempo = 0;
    public Player player;

    public Torre(Mesh mesh, Material material, Vector3 posInicial)
    {

        torre = new GameObject("Torre");
        torre.AddComponent<MeshFilter>();
        torre.AddComponent<MeshRenderer>();
        torre.GetComponent<MeshFilter>().mesh = mesh;
        torre.GetComponent<MeshRenderer>().material = material;
        torre.AddComponent<BoxCollider>();
        torre.GetComponent<BoxCollider>().isTrigger = true;

        torre.tag = "Torre";

        torre.transform.position = posInicial;
        torre.transform.localScale = new Vector3(1, 3, 1);



    }

    
    public void Update(float gameTime)
    {
        if(conquista == true)
        {
            tempo += 0.01f;
        }
        if(tempo > 15f)
        {
            color = player.color;
            torre.GetComponent<MeshRenderer>().material.color = color;
            tempo = 0;
            conquista = false;
            player.conquistouTorre = true;
            Debug.Log(player.conquistouTorre);
        }
        

    }
    public void PlayerConquistando(Player player)
    {
        this.player = player;
    }


}
