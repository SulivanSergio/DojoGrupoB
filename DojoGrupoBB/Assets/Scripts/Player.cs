using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoonSharp.Interpreter;
using System.IO;
using System;

public class Player
{
    enum STATE {
        TORRE,
        CENTRO,
        SEGUIR,
        PARADO

    }

    //Codigo em C#
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



    //Codigo da liguagem LUA

    Script script = new Script();
    DynValue dynValue;
    string lua =
@"

--TORRE = 0
--CENTRO = 1
--SEGUIR = 2
--PARADO = 3


function Start()
    ChangeState(0)	
end

function Update(gameTime)
	if (ColidiuPlayer() == true) then
		ChangeState(1)
	end
	if (ConquistouTorre() == true) then
		ChangeState(1)
	end
	if (PegouArma() == true) then
		ChangeState(1)
	end

end"
        ;

    string caminho = "";

    //Variaveis que serão utilizadas para auxiliar o arquivo lua
    public bool colidiuPlayer = false;
    public bool conquistouTorre = false;
    public bool pegouArma = false;

    


    public Player(Mesh mesh, Material material, Torre[] torre, GameObject centro, Texto texto, Vector3 posInicial, Color color)
    {
        
        

        this.torre = torre;
        this.centro = centro;
        this.texto = texto;
        this.color = color;
        CreateGameObject(mesh, material);

        texto.canvas.transform.SetParent(player.transform);


        player.GetComponent<MeshRenderer>().material.color = color;
        player.transform.position = posInicial;

        qualTorre = UnityEngine.Random.Range(0, torre.Length);

        for (int i = 0; i < Main.instance.player.Length; i++)
        {
            if(Main.instance.player[i] != this)
            {
                qualPlayer = UnityEngine.Random.Range(0, Main.instance.player.Length);
            }

            
        }

        caminho = @"D:\Documentos\Ifrj\Setimo periodo\IA\DojoIA\DojoGrupoB\DojoGrupoB\Lua\" + texto.text.GetComponent<TMP_Text>().text +".lua";
        LoadLua();
        DeclaraFunction();
        dynValue = script.Call(script.Globals.Get("Start"));

    }

    public void Update(float gameTime)
    {

        


        UpdateState(gameTime);

        LoadLua();
        dynValue = script.Call(script.Globals.Get("Update"), gameTime);

    }



    private void LoadLua()
    {

        if(File.Exists(caminho))
        {
            lua = File.ReadAllText(caminho);
            script.DoString(lua);
        }
        else
        {
            File.WriteAllText(caminho,lua);
            script.DoString(lua);

        }

    }


    private void DeclaraFunction()
    {

        script.Globals["ColidiuPlayer"] = (Func<bool>)ColidiuPlayer;
        script.Globals["ConquistouTorre"] = (Func<bool>)ConquistouTorre;
        script.Globals["PegouArma"] = (Func<bool>)PegouArma;
        script.Globals["ChangeState"] = (Func<int,int>)ChangeState;


    }



    private void CreateGameObject(Mesh mesh, Material material)
    {
        player = new GameObject(texto.text.GetComponent<TMP_Text>().text);
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

    public int ChangeState(int item)
    {

        /* TORRE = 0
        CENTRO = 1
         SEGUIR = 2
        PARADO = 3
         
          */
        this.item = item;
        if(this.item == 0)
        {
            qualTorre = UnityEngine.Random.Range(0, torre.Length);
        }
        if (this.item == 2)
        {
            qualPlayer = UnityEngine.Random.Range(0, Main.instance.player.Length);
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

        return 0;

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
                
                
            }
            colidiuPlayer = true;
        }
        if (other.gameObject.tag == "Centro")
        {
            arma = true;
            pegouArma = true;
            
        }

        //limpando as variaveis
        pegouArma = false;
        colidiuPlayer = false;
        conquistouTorre = false;


    }

    public void OnCollisionExit(Collider other)
    {

    }

    
    public bool ColidiuPlayer()
    {
        return this.colidiuPlayer;
    }

    public bool ConquistouTorre()
    {
        return this.conquistouTorre;
    }

    public bool PegouArma()
    {
        return this.pegouArma;
    }



}
