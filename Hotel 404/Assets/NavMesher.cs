using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMesher : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface1;
    

    private float waitTime = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
        surface1.BuildNavMesh();
    }
    IEnumerator timer()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(waitTime); // wait for the specified time
    }

}
