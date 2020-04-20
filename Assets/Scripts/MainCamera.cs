using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static new Camera camera;
    public static Vector2 mouseWorldPos;

    private void Awake()
    {
        camera = this.GetComponent<Camera>();
    }

    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
