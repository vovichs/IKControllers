using UnityEngine;

public class TwoBoneIK : MonoBehaviour
{
    public Transform upperArm;
    public Transform forearm;
    public Transform hand;
    public Transform target;
    public Transform elbowTarget;

    void Update()
    {
        SolveIK();
    }

    void SolveIK()
    {
        float upperArmLength = Vector3.Distance(upperArm.position, forearm.position);
        float forearmLength = Vector3.Distance(forearm.position, hand.position);

        Vector3 targetPosition = target.position;
        Vector3 elbowTargetPosition = elbowTarget.position;

        Vector3 upperArmToTarget = targetPosition - upperArm.position;
        float upperArmToTargetLength = upperArmToTarget.magnitude;

        float angleAtElbow = Mathf.Acos((upperArmLength * upperArmLength + forearmLength * forearmLength - upperArmToTargetLength * upperArmToTargetLength) / (2 * upperArmLength * forearmLength)) * Mathf.Rad2Deg;

        Vector3 elbowDirection = (elbowTargetPosition - upperArm.position).normalized;
        Vector3 elbowPosition = upperArm.position + upperArmLength * elbowDirection;

        Vector3 forearmDirection = (targetPosition - elbowPosition).normalized;
        Vector3 forearmPosition = elbowPosition + forearmLength * forearmDirection;

        upperArm.LookAt(elbowPosition, Vector3.Cross(elbowDirection, forearmDirection));
        forearm.LookAt(targetPosition, Vector3.Cross(forearmDirection, elbowDirection));
    }
}
