using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbTransformController : MonoBehaviour
{
    public Transform positiveAnchor, negativeAnchor;
    public Vector2 scaleAdjuster;
    public Vector2 lengthBounds, widthBounds;
    public float widthLerpPower;

    private float anchorDisplacement, boundedLength, boundedWidth;

    private new Transform transform;

    private void Awake()
    {
        transform = this.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        anchorDisplacement = Vector3.Distance(positiveAnchor.position, negativeAnchor.position);
        boundedLength = Mathf.Clamp(anchorDisplacement, lengthBounds.x, lengthBounds.y);
        boundedWidth = Mathf.Lerp(widthBounds.x, widthBounds.y, 1 - Mathf.Pow(Mathf.Lerp(0,1,(boundedLength - lengthBounds.x)/lengthBounds.y),widthLerpPower));

        transform.position = (positiveAnchor.position + negativeAnchor.position) * 0.5f;
        transform.localScale = new Vector3(boundedLength *scaleAdjuster.x, boundedWidth *scaleAdjuster.y, 1);
        transform.rotation = Quaternion.LookRotation(transform.forward, Quaternion.Euler(0, 0, 90) * (positiveAnchor.position - negativeAnchor.position));
    }
}
