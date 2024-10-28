using UnityEngine;

public class HomeBaseUnit : UnitController
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables
    [SerializeField]
    GameObject SpawnerT1;
    public Vector3 SpawnerSmall;
    public Transform DockingA;
    public Transform DockingB;
    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions
    void Start()
    {
        SpawnerSmall = SpawnerT1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
