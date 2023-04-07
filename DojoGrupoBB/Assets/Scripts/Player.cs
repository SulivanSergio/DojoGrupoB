using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoonSharp.Interpreter;

public class Player
{
    enum STATE {
        TORRE,
        CENTRO,
        SEGUIR,
        PARADO

    }

    public GameObject player;
    Torre[] torre;
    GameObject centro;
    STATE state = STATE.TORRE;

    private float speed = 0.5f;
    public int item = 0;
    public bool arma = false;
    public Color color;

    public int qualTorre;
    public int qualPlayer;
    Texto texto;

    public Player(Mesh mesh, Material material, Torre[] torre, GameObject centro, Texto texto, Vector3 posInicial, Color color)
    {
        
        CreateGameObject(mesh, material);

        this.torre = torre;
        this.centro = centro;
        this.texto = texto;
        this.color = color;
        texto.canvas.transform.SetParent(player.transform);


        player.GetComponent<MeshRenderer>().material.color = color;
        player.transform.position = posInicial;

        qualTorre = Random.Range(0, torre.Length);
        for (int i = 0; i < Main.instance.player.Length; i++)
        {
            if(Main.instance.player[i] != this)
            {
                qualPlayer = Random.Range(0, Main.instance.player.Length);
            }

            
        }
    }

    public void Update(float gameTime)
    {


        UpdateState(gameTime);


    }

    private void CreateGameObject(Mesh mesh, Material material)
    {
        player = new GameObject("Player");
        player.AddComponent<MeshFilter>();
        player.AddComponent<MeshRenderer>();
        player.GetComponent<MeshFilter>().mesh = mesh;
        player.GetComponent<MeshRenderer>().material = material;

        player.AddComponent<BoxCollider>();
        player.GetComponent<BoxCollider>().isTrigger = true;
        player.AddComponent<Rigidbody>();
        player.GetComponent<Rigidbody>().useGravity = false;

        player.AddComponent<ColliderPlayer>();

        player.tag = "Player";

    }

    public void UpdateState(float gameTime)
    {

        switch(state)
        {

            case STATE.PARADO:
                item = Random.Range(0, 4);
                ChangeState(item);
                Debug.Log("ITEM: " + item + "  PARADO");
                break;

            case STATE.SEGUIR:
                Seguir(gameTime, player, Main.instance.player[qualPlayer].player);
                break;

            case STATE.TORRE:
                
                Seguir(gameTime, player, torre[qualTorre].torre);
                break;

            case STATE.CENTRO:
                Seguir(gameTime, player, centro);
                break;

        }

    }

    public void ChangeState(int item)
    {

        /* TORRE = 0
        CENTRO = 1
         SEGUIR = 2
        PARADO = 3
         
          */
        this.item = item;
        if(this.item == 0)
        {
            qualTorre = Random.Range(0, torre.Length);
        }
        if (this.item == 2)
        {
            qualPlayer = Random.Range(0, Main.instance.player.Length);
        }

        switch (state)
        {

            case STATE.PARADO:

                switch(item)
                {
                    case 1:
                        state = STATE.CENTRO;
                        break;
                    case 0:
                        state = STATE.TORRE;

                        break;
                    case 2:
                        state = STATE.SEGUIR;
                        break;

                }
                break;

            case STATE.CENTRO:

                switch (item)
                {
                    case 3:
                        state = STATE.PARADO;
                        break;
                    case 0:
                        state = STATE.TORRE;

                        break;
                    case 2:
                        state = STATE.SEGUIR;
                        break;

                }
                break;

            case STATE.TORRE:

                switch (item)
                {
                    case 3:
                        state = STATE.PARADO;
                        break;
                    case 1:
                        state = STATE.CENTRO;

                        break;
                    case 2:
                        state = STATE.SEGUIR;
                        break;

                }
                break;

            case STATE.SEGUIR:

                switch (item)
                {
                    case 3:
                        state = STATE.PARADO;
                        break;
                    case 1:
                        state = STATE.CENTRO;

                        break;
                    case 0:
                        state = STATE.TORRE;
                        break;

                }
                break;


        }

    }

    private void Seguir(float gameTime, GameObject objeto, GameObject alvo)
    {
        float distancia = Vector3.Distance(alvo.transform.position, objeto.transform.position);

        if(alvo.transform.position.x < objeto.transform.position.x)
        {
            objeto.transform.position -= new Vector3(distancia * speed * gameTime,0,0);
        }
        if (alvo.transform.position.x > objeto.transform.position.x)
        {
            objeto.transform.position += new Vector3(distancia * speed * gameTime, 0, 0);
        }

        if (alvo.transform.position.z < objeto.transform.position.z)
        {
            objeto.transform.position -= new Vector3(0, 0, distancia * speed * gameTime);
        }
        if (alvo.transform.position.z > objeto.transform.position.z)
        {
            objeto.transform.position += new Vector3(0, 0, distancia * speed * gameTime);
        }


    }

    public void OnCollisionEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Torre")
        {

            for (int i = 0; i< torre.Length; i++)
            {
                if (other.gameObject == torre[i].torre)
                {
                    torre[i].conquista = true;
                    torre[i].PlayerConquistando(this);
                }
                
            }
            
        }
        if (other.gameObject.tag == "Player")
        {

            if(this.arma == true && other.gameObject.GetComponent<ColliderPlayer>().arma == true)
            {
                player.SetActive(false);
                other.gameObject.SetActive(false);
                arma = false;
            }
            if(this.arma == true && other.gameObject.GetComponent<ColliderPlayer>().arma == false)
            {
                other.gameObject.SetActive(false);
                arma = false;
            }
            if (this.arma == false && other.gameObject.GetComponent<ColliderPlayer>().arma == true)
            {
                player.SetActive(false);
                other.gameObject.GetComponent<ColliderPlayer>().arma = false;
            }
            if (this.arma == false && other.gameObject.GetComponent<ColliderPlayer>().arma == false)
            {
                item = Random.Range(0, 4);
                ChangeState(item);
                Debug.Log("Fracos");
            }

        }
        if (other.gameObject.tag == "Centro")
        {
            arma = true;
            ChangeState(Random.Range(0, 4));
        }
    }
    public void OnCollisionExit(Collider other)
    {

    }

}
