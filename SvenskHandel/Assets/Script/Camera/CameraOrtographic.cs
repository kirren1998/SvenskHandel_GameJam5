using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrtographic : MonoBehaviour
{
    public static Camera _camera;
    [SerializeField] private Transform player;
    [SerializeField] private float cameraSpeed = 11f;
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float minZoom = 0.2f, maxZoom = 4f;
    [SerializeField] private float yOffset = 5, zOffset = 5;
    private float _newZoom = 0;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _newZoom = _camera.orthographicSize;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            OnZoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            var cameraPosition = Vector3.Lerp(transform.position,  player.position, Time.deltaTime * cameraSpeed);
            transform.position = new Vector3(cameraPosition.x, yOffset, cameraPosition.z - zOffset);
        }
    } //updates the position with a Lerp function to smoothen the camera movement between its current position and _newPos
    
    //Zooms in or out within a range. 
    private void OnZoom(float zoom)
    {
        _newZoom -= zoom * scrollSpeed;

        _newZoom = Mathf.Clamp(_newZoom, minZoom, maxZoom);
        _camera.orthographicSize = _newZoom;
    }
}
