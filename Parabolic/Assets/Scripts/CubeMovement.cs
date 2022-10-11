using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float parabolaParameter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parabolaParameter += Time.deltaTime;
        parabolaParameter = parabolaParameter % 5;
        transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10f, 5f, parabolaParameter / 5f);
    }
}
