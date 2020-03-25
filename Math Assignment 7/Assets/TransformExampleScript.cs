using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformExampleScript : MonoBehaviour
{
    public Vector3 translation;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;

    MeshFilter mymesh;
    Vector3[] originalVertices;
    Vector3[] newVertices;

    Matrix4x4 m;
    // Start is called before the first frame update
    void Start()
    {
        mymesh = GetComponent<MeshFilter>();
        originalVertices = mymesh.mesh.vertices;
        newVertices = new Vector3[originalVertices.Length];
    }

    // Update is called once per frame
    void Update()
    {
        m = Matrix4x4.identity;
        Quaternion qrotation = Quaternion.Euler(rotation);
        m.SetTRS(translation, qrotation, scale);

        for (int i = 0; i < originalVertices.Length; i++){
            newVertices[i] = m.MultiplyPoint3x4(originalVertices[i]);
        }
        mymesh.mesh.vertices = newVertices;
    }
}
