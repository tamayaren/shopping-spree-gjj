using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Camera camera;

    [SerializeField]
    private CameraProperties properties;

    [SerializeField]
    private GameObject cameraSubject;

    [SerializeField, Range(1f, 6f)]
    private float smoothness = 4f;

    [SerializeField, Range(15f, 120f)]
    private float fieldOfView = 60f;

    [SerializeField, Range(3f, 15f)]
    private float distance = 8f;

    [SerializeField, Range(5f, 35f)]
    private float size = 5f;

    public Vector2 offset = new Vector2(0f, 0f);

    private void Start()
    {
        camera = GetComponent<Camera>();

        this.fieldOfView = properties.fieldOfView;
        this.smoothness = properties.smoothness;
        this.distance = properties.distance;
        this.size = properties.size;
    }

    private void FixedUpdate()
    {
        if (this.cameraSubject == null) return;

        Vector3 computed = new Vector3(this.offset.x, this.offset.y, -this.distance);
        Vector3 interpolated = Vector3.Lerp(this.transform.position, this.cameraSubject.transform.position + computed, this.smoothness * Time.deltaTime);

        camera.fieldOfView = this.fieldOfView;
        camera.orthographicSize = this.size;
        this.transform.position = interpolated;
    }

    public void SetSubject(GameObject? gameObject = null)
    {
        this.cameraSubject = gameObject;
    }
}
