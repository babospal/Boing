
using Unity.VectorGraphics;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public PlayerController playerControllerScript;
    private Vector3 offset;
    private float zCoordinate;
    private void Start()
    {
       playerControllerScript= GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void OnMouseDown()
    {
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;

        offset = transform.position -
                 GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (playerControllerScript.isInPlacementMode)
        {
            transform.position = GetMouseWorldPosition() + offset;

        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
