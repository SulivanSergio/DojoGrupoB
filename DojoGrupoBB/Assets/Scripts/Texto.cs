using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto 
{

    public GameObject gameObject, textGO;
    Canvas canvas;
    Text text;

    public Texto(GameObject pai, RenderMode renderMode, Vector3 scale)
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

        gameObject.GetComponent<Canvas>().renderMode = renderMode;
        gameObject.GetComponent<RectTransform>().localScale = scale;

        
        gameObject.transform.parent = pai.transform;

        gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;

    }


    


}
