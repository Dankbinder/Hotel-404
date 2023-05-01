using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
public class NavMesher : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface1;
    [SerializeField] private NavMeshSurface surface2;
    // Start is called before the first frame update
    void Start()
    {
        surface1.BuildNavMesh();
        surface2.BuildNavMesh();
    }

}
