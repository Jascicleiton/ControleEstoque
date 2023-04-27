using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        if(canvas == null )
        {
            canvas = GetComponent<Canvas>();
        }
        canvas.worldCamera = Camera.main;
    }
}
