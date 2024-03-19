using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject playerCamera;
    public Transform cameraTransform;
    public Joystick mapController;
    public float cameraHoverSpeed = 5f;
    private Vector3 position;

    void Update()
    {
        HandleMinimapCamera();
        MoveMap();
    }

    void MoveMap()
    {
        if (cameraTransform == null) return;

        position.x = mapController.Horizontal * cameraHoverSpeed * Time.deltaTime;
        position.y = mapController.Vertical * cameraHoverSpeed * Time.deltaTime;

        cameraTransform.Translate(new Vector2(position.x, position.y));
    }

    public void HandleMinimapCamera()
    {
        if (ModeChanger.currentCharacter == "Marco")
        {
            playerCamera = GameObject.FindGameObjectWithTag("MarcoMinimapCamera");
            if (playerCamera == null) return;

            cameraTransform = playerCamera.transform;
        }
        else
        {
            playerCamera = GameObject.FindGameObjectWithTag("IsabelMinimapCamera");
            if (playerCamera == null) return;

            cameraTransform = playerCamera.transform;
        }

    }

    public void ReturnToNormal()
    {
        position.x = 0f;
        position.y = 1f;
        position.z = -10f;
        mapController.DisableInput();
        if (cameraTransform == null) return;
        cameraTransform.localPosition = position;
    }
}
