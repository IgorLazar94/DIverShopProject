using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerFOV))]
public class Editor_FOV : Editor
{
    //private void OnSceneGUI()
    //{
    //    PlayerFOV fow = (PlayerFOV)target;
    //    Handles.color = Color.white;
    //    Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
    //    Vector3 viewAngleA = fow.DirForAngle(-fow.viewAngle / 2, false);
    //    Vector3 viewAngleB = fow.DirForAngle(fow.viewAngle / 2, false);
    //    Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
    //    Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

    //    Handles.color = Color.red;
    //    foreach (Transform visibleTarget in fow.visibleTargets)
    //    {
    //        Handles.DrawLine(fow.transform.position, visibleTarget.position);
    //    }
    //}
}
