using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre 
{
    
    public GameObject torre;
    public bool conquista;
    public Color color = Color.white;
    public float tempo = 0;
    
    List<Player> player = new List<Player>();
    
    

    public Torre(Mesh mesh, Material material, Vector3 posInicial,Color color, int id)
    {
        this.color = color;


        torre = new GameObject("Torre"+ id);
        torre.AddComponent<MeshFilter>();
        torre.AddComponent<MeshRenderer>();
        torre.GetComponent<MeshFilter>().mesh = mesh;
        torre.GetComponent<MeshRenderer>().material = material;
        torre.AddComponent<BoxCollider>();
        torre.GetComponent<BoxCollider>().isTrigger = true;
        torre.GetComponent<MeshRenderer>().material.color = color;
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
            

            color = this.player[0].color;
            torre.GetComponent<MeshRenderer>().material.color = color;
            tempo = 0;
            conquista = false;
            player[0].conquistouTorre = true;
            player[0].score++;
            player.RemoveAt(0);


            if (player.Count > 0)
            {
                if (player[0] != null)
                {
                    conquista = true;
                }
            }
        }
        

    }
    public void PlayerConquistando(Player player)
    {
        this.player.Add(player);


    }


}
