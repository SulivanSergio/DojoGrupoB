using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public Mesh meshCentro;
    public Mesh mesh;
    public Material material;

    

    Player player;
    Torre torre;
    Centro centro;



    void Start()
    {

        torre = new Torre(mesh, material);
        centro = new Centro(meshCentro, material);
        player = new Player(mesh, material, torre.torre, centro.centro);

    }

    
    void Update()
    {
        
         player.Update(Time.deltaTime);

    }
}
