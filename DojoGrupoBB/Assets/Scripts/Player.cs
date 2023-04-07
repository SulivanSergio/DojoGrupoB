using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player
{
    enum STATE {
        TORRE,
        CENTRO,
        SEGUIR,
        PARADO

    }

    GameObject player;
    GameObject torre;
    GameObject centro;
    STATE state = STATE.TORRE;

    private float speed = 0.5f;
    private int item = 0;

    

    public Player(Mesh mesh, Material material, GameObject torre, GameObject centro)
    {
        player = new GameObject("Player");
        player.AddComponent<MeshFilter>();
        player.AddComponent<MeshRenderer>();
        player.GetComponent<MeshFilter>().mesh = mesh;
        player.GetComponent<MeshRenderer>().material = material;

        

        this.torre = torre;
        this.centro = centro;


        



    }

    public void Update(float gameTime)
    {


        UpdateState(gameTime);


    }

    public void UpdateState(float gameTime)
    {

        switch(state)
        {

            case STATE.PARADO:

                break;

            case STATE.SEGUIR:
                
                break;

            case STATE.TORRE:
                Seguir(gameTime, player, torre);
                break;

            case STATE.CENTRO:
                Seguir(gameTime, player, centro);
                break;

        }

    }


    public void ChangeState()
    {

        /* TORRE = 0
        CENTRO = 1
         SEGUIR = 2
        PARADO = 3
         
          */

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

}
