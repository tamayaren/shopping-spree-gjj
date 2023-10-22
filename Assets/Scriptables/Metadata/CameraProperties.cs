using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraDefaults", menuName = "Defaults/Camera", order = 1)]
public class CameraProperties : ScriptableObject
{
    public float fieldOfView = 40f;
    public float smoothness = 4f;
    public float distance = 8f;
    public float size = 5f;
}
