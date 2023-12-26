using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayerMask;

    public event System.Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
           OnClicked?.Invoke();        
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI() 
        => EventSystem.current.IsPointerOverGameObject();
    
    public Vector3 GetSelectedMapPosition()
    {
      Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
