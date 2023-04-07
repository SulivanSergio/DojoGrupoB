using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Main : MonoBehaviour
{

    public static Main instance;

    public Mesh meshCentro;
    public Mesh mesh;
    public Material material;

    const int MAX = 5;

    public Player[] player = new Player[MAX];
    Torre[] torre = new Torre[MAX];
    Centro centro;

    Texto[] texto = new Texto[MAX];
    public GameObject []canvas = new GameObject[MAX];
    public GameObject[] text = new GameObject[MAX];

    Color[] color = new Color[MAX];

    void Start()
    {
        instance = this;

        color[0] = Color.red;
        color[1] = Color.blue;

        

        
        centro = new Centro(meshCentro, material,new Vector3(50,0,0));

        for (int i = 0; i < player.Length; i++)
        {
            texto[i] = new Texto();
            texto[i].canvas = canvas[i];
            texto[i].text = text[i];
            texto[i].text.GetComponent<TMP_Text>().SetText("Player" + i);

            torre[i] = new Torre(mesh, material, new Vector3(20 * i, 0, 10*1));
            player[i] = new Player(mesh, material, torre, centro.centro, texto[i],new Vector3(100 * i +20,0,100 *i +20 ),color[i%2] );
        }


    }

    
    void Update()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i].Update(Time.deltaTime);
        }

        for (int i = 0; i < torre.Length; i++)
        {
            torre[i].Update(Time.deltaTime);
        }

    }
}
