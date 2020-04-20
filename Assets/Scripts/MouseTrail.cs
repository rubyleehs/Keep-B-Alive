using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    public GameObject trail;
    public float trailSelfDestructDuration;

    private Transform currentTrail;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            currentTrail = Instantiate(trail, MainCamera.mouseWorldPos, Quaternion.identity, this.transform).transform;
        }
        if (currentTrail == null) return;

        if(Input.GetButton("Fire1")) currentTrail.position = MainCamera.mouseWorldPos;

        if(Input.GetButtonUp("Fire1") && currentTrail != null)
        {
            Destroy(currentTrail.gameObject, trailSelfDestructDuration);
            currentTrail = null;
        }
    }
}
