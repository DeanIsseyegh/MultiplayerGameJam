using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class CameraTarget : MonoBehaviour
{
    public Transform Target;

    public bool LockX;
    public bool LockY;
    public bool LockZ;

    [Space]
    public bool FollowRotation;

    public void UpdatePosition()
    {
        transform.position =
            new Vector3(
                LockX ? transform.position.x : Target.position.x,
                LockY ? transform.position.y : Target.position.y,
                LockZ ? transform.position.z : Target.position.z
            );

        if (FollowRotation)
        {
            transform.rotation = Target.rotation;
        }
    }

    private void LateUpdate()
    {
        if (Target != null)
            UpdatePosition();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraTarget))]
[CanEditMultipleObjects]
public class CameraTargetEditor : MultiSupportEditor
{
    private CameraTarget _sCameraTarget;

    public override void OnEnable()
    {
        base.OnEnable();

        _sCameraTarget = target as CameraTarget;
    }

    public override void MainDrawGUI()
    {
        DrawDefaultInspector();
    }
}
#endif