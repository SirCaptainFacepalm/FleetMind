using UnityEngine;

public class VisionMesh : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References


    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions
  
    void Start()
    {
        DefineMesh();
    }

    private void DefineMesh()
    {
        Vector3[] _verteces = new Vector3[3];
        Vector2[] _uv = new Vector2[3];
        int[] _triangles = new int[3]; 

        Mesh _visionMesh = new Mesh();
        _visionMesh.vertices = _verteces;
        _visionMesh.uv = _uv;
        _visionMesh.triangles = _triangles;


        GetComponent<MeshFilter>().mesh = _visionMesh;
    }
    #endregion
}
