using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float  MaxMoveDistanceFromCenter = 2;
    [SerializeField] private Camera  camera;
    
    private Vector3 MousePosition;
    
    void Update()
    {
        MousePosition =  Mouse.current.position.ReadValue();
        Vector3 direction = (camera.WorldToScreenPoint(MousePosition) - transform.position).normalized;
        //Debug.Log(camera.WorldToScreenPoint(transform.position));
        Debug.Log(camera.ScreenToWorldPoint(MousePosition));
    }
}
