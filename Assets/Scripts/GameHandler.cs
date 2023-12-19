using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject navMesh;

    private void Awake()
    {
        navMesh.SetActive(true);
    }

}
