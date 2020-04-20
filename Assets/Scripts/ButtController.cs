using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtController : MonoBehaviour
{
    public Transform origin;
    public Vector2 buttBounds;
    public LayerMask buttMask;

    private new Transform transform;
    private Vector2 topRightBound, bottomLeftBound;
    private bool isBeingDragged = false;
    private Vector2 clickDelta;

    private void Awake()
    {
        transform = this.GetComponent<Transform>();
        topRightBound = (Vector2)origin.position + buttBounds;
        bottomLeftBound = (Vector2)origin.position - buttBounds;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Physics2D.Raycast(MainCamera.mouseWorldPos, Vector3.forward, 100, buttMask))
        {
            isBeingDragged = true;
            clickDelta = (Vector2)transform.position - MainCamera.mouseWorldPos;
        }
        else if (!Input.GetButton("Fire1")) isBeingDragged = false;
        

        if (isBeingDragged)
        {
            transform.position = MainCamera.mouseWorldPos + clickDelta;
        }

        EnsureButtInBounds();
    }


    private void EnsureButtInBounds()
    {
        if (transform.position.x > topRightBound.x || transform.position.x < bottomLeftBound.x)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftBound.x, topRightBound.x), transform.position.y, transform.position.z);
        }

        if (transform.position.y > topRightBound.y || transform.position.y < bottomLeftBound.y)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottomLeftBound.y, topRightBound.y), transform.position.z);
        }
    }

}
