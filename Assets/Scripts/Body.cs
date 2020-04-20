using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Body : MonoBehaviour
{
    [Header("Joint Transforms")]
    public Transform buttJoint;
    public Transform leftHipJoint, rightHipJoint;
    public Transform leftKneeJoint, rightKneeJoint;
    public Transform leftFeetJoint, rightFeetJoint;

    [Header("History Logging")]
    public int maxHistoryLogs = 50;
    public float logInterval = 0.5f;

    [HideInInspector]
    public List<BodyJointsPositions> historyLog;

    public Transform butt;
    public Vector2 buttScaleBounds;

    public BodyJointsPositions current;
    private BodyJointsPositions sum;
    private BodyJointsPositions? mean;
    private float lastLogTime;

    private void Awake()
    {
        historyLog = new List<BodyJointsPositions>();
        sum = new BodyJointsPositions(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
        mean = null;
    }

    private void FixedUpdate()
    {
        if (Time.time - lastLogTime < logInterval) return;

        lastLogTime = Time.time;
        current = GetJointsPositions();
        AddToJointsPosHistory(current);
        GameManager.instance.danceManager.CheckAnyDanceMoveSuccess(this);
    }

    public void ChangeButtSize(Slider slider)
    {
        butt.transform.localScale = Vector3.one * Mathf.Lerp(buttScaleBounds.x, buttScaleBounds.y, slider.value);
    }

    private BodyJointsPositions GetJointsPositions()
    { 
        return new BodyJointsPositions(
            buttJoint.position,
            leftHipJoint.position, rightHipJoint.position,
            leftKneeJoint.position, rightKneeJoint.position,
            leftFeetJoint.position, rightFeetJoint.position);
    }

    private void AddToJointsPosHistory(BodyJointsPositions positions)
    {
        sum += positions;
        historyLog.Add(positions);

        if(historyLog.Count > maxHistoryLogs)
        {
            sum -= historyLog[0];
            historyLog.RemoveAt(0);
        }

        mean = null;
    }

    public BodyJointsPositions GetMeanJointsPositions()
    {
        if (mean != null) return mean.Value;

        mean = sum * (1f / Mathf.Min(historyLog.Count,maxHistoryLogs));
        return mean.Value;
    }
}
