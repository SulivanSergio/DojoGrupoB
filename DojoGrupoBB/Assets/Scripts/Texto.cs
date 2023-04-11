using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto 
{

    public GameObject gameObject, textGO;
    Canvas canvas;
    Text text;

    public Texto(GameObject pai)
    {
        gameObject = new GameObject("Canvas");
        gameObject.AddComponent<RectTransform>();
        
        gameObject.AddComponent<Canvas>();
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
        


        textGO = new GameObject("Text");
        textGO.AddComponent<Text>();
        textGO.AddComponent<RectTransform>();

        textGO.GetComponent<Text>().font = (Font)Resources.GetBuiltinResource(typeof(Font),"Arial.ttf");
        textGO.GetComponent<Text>().text = "Nulo";
        textGO.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
        textGO.transform.parent = gameObject.transform;

        gameObject.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);

        
        gameObject.transform.parent = pai.transform;

        gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;

    }


    


}
