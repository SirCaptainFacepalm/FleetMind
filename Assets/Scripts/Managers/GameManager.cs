using UnityEngine;
using System.Collections.Generic;

public class GameManager : VariablePool
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    public int RelevamtFactopn;
    public int IDCounter;
    public int[] TransferAmount; 
    
    public Vector3 RayPoint;
    [SerializeField]
    public GameObject[] ShipList;

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References
    public List<AiCommander> Commanders;
        
    public FighterUnit FighterPrefab;
    public CollectorUnit CollectorPrefab;
    public GameObject FormationPrefab;  
    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions


    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Null Madafakaaaa");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

    }
    void Update()
    {

    }

    public void IDCount()
    {
        IDCounter++;
    }

    public void ShipDestroyed(UnitController Deadship , int Faction , int shipID)
    {
        
    }
    #endregion
}
