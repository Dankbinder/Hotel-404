using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMesher : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface1;
    // Start is called before the first frame update
    
    private void Start()
    {
        Invoke("ActivateMesh", 4f); // calls ActivateMethod() after 4 seconds
    }
    
    void ActivateMesh()
    {
        surface1.BuildNavMesh();
    }
}
