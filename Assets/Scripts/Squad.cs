using UnityEngine;
[System.Serializable]
public class Squad
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables
    public UnitController[] NewSquad = new UnitController[6];

    public Vector3[] FormationOffset = new Vector3[7];
    public int squadsize = 1;
    public int SquadStrength;
    public int SquadNumber = 1;

    public GameObject SquadFormationObject;

    public Squad()
    {
        // Formation A

        FormationOffset[0] = new Vector3(0, 0, 7);
        FormationOffset[1] = new Vector3(-7, 0, 0);
        FormationOffset[2] = new Vector3(7, 0, 0);
        FormationOffset[3] = new Vector3(-14, 0, -7);
        FormationOffset[4] = new Vector3(14, 0, -7);
        FormationOffset[5] = new Vector3(0, 0, -7);
        FormationOffset[6] = new Vector3(0, 0, 0);

        // Formation B

        // FOrmation C
    

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions

    #endregion
}
    }
