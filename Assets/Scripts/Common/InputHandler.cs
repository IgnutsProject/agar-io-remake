using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }
    
    public event Action<float> OnMouseX;
    public event Action<float> OnMouseY;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        OnMouseX?.Invoke(Input.GetAxis(InputConfig.MouseXAxis));
        OnMouseY?.Invoke(Input.GetAxis(InputConfig.MouseYAxis));
    }
}
