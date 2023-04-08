using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Main : MonoBehaviour
{

    public static Main instance;

    [Header("Mesh dos Objetos")]
    public Mesh meshCentro;
    public Mesh mesh;
    public Material material;

    const int MAX = 5;

    public Player[] player = new Player[MAX];
    Torre[] torre = new Torre[MAX];
    Centro centro;

    Texto[] texto = new Texto[MAX];

    [Header("Canvas dos Objetos")]
    public GameObject []canvas = new GameObject[MAX];
    [Header("Texto dos Objetos")]
    public GameObject[] text = new GameObject[MAX];

    [Header("Texto dos Ranking")]
    public TMP_Text textRanking;
    bool ranking = false;

    Color[] color = new Color[MAX];

    void Start()
    {
        instance = this;

        color[0] = Color.red;
        color[1] = Color.blue;
        color[2] = Color.black;
        color[3] = Color.green;
        color[4] = Color.grey;


        centro = new Centro(meshCentro, material,new Vector3(50,0,0));

        for (int i = 0; i < player.Length; i++)
        {
            texto[i] = new Texto();
            texto[i].canvas = canvas[i];
            texto[i].text = text[i];
            texto[i].text.GetComponent<TMP_Text>().SetText("Player" + i);

            torre[i] = new Torre(mesh, material, new Vector3(20 * i, 0, 10),i);
            player[i] = new Player(mesh, material, torre, centro.centro, texto[i],new Vector3(10 * i +20,0,1000 ),color[i] ,i);
        }


    }


    void Update()
    {
        if (ranking == false)
        {


            for (int i = 0; i < player.Length; i++)
            {
                player[i].Update(Time.deltaTime);
            }

            for (int i = 0; i < torre.Length; i++)
            {
                torre[i].Update(Time.deltaTime);

            }

            for (int i = 0; i < player.Length; i++)
            {
                int aux = 0;
                for (int j = 0; j < torre.Length; j++)
                {
                    if (player[i].color == torre[j].color)
                    {
                        aux++;
                    }
                    if (aux == player.Length)
                    {
                        Ranking();
                    }

                }


            }
            int auxRanking = 0;
            for (int i = 0; i < player.Length; i++)
            {

                if (player[i].player.activeSelf == false)
                {

                    auxRanking++;
                }
                if (auxRanking >= player.Length - 1)
                {

                    Ranking();
                }


            }

        }
    }


    private void Ranking()
    {
        if (ranking == false)
        {
            Player aux;
            Player[] vetorOrdenado = new Player[player.Length];

            for (int i = 0; i < player.Length; i++)
            {

                vetorOrdenado[i] = player[i];

            }

            for (int i = 0; i < player.Length; i++)
            {
                for (int j = 0; j < player.Length; j++)
                {
                    if (vetorOrdenado[i].score > vetorOrdenado[j].score)
                    {
                        aux = vetorOrdenado[i];
                        vetorOrdenado[i] = vetorOrdenado[j];
                        vetorOrdenado[j] = aux;


                    }


                }

            }
            for (int i = 0; i < player.Length; i++)
            {
                textRanking.text += "Nome: " + vetorOrdenado[i].player.name + " Score: " + vetorOrdenado[i].score + "\n";
            }
            ranking = true;
        }
    }
   



}
