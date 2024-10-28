using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AiCommander : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    // Descition making Variables
    int ImportanceResource;
    int ImportanceBuild;
    int ImportanceDefend;
    int ImportanceAttack;
    int ImportanceRetreat;
    int MyTotalValue;
    [SerializeField]
    int CurrentResource;

    bool isEnomeratorOn = false;
    bool isRetreat = false;
    public int MyFaction;

    List<int> EnemyFaction = new List<int>();

    float Inportance;

    List<UnitController> ShipList = new List<UnitController>();
    List<GameObject> SquadList = new List<GameObject>();
    //[SerializeField]



    RaycastHit _rayinfo;
    public LayerMask GameGrid;
    public LayerMask UnitSelection;


    // What the AI Knows

    // Resources
    public List<ResourceNode> ResourceNodes;
    List<GameObject> ResourceGroup = new List<GameObject>();

    // Enemies

    int[] EnemyPrecievedValue;
    Transform[] EnemySighted;
    Transform[] CollectorSighted;
    Transform[] EnemyBase;

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    // TMP VAR
    bool squadisreal = false;

    // TMP REF         <<<<<<---------------
    public GameObject LaserRef;
    public HomeBaseUnit BaseRef;
    public SquadManager squadManager;
    public Formation formation;
    public CollectorUnit[] CollectorRef;

    VisionMesh VisionMeshObject;
    public UnitController MyUnit;
    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions
    #region Subcountios Functions
    // Functions that happen automatically all the time
    void Start()
    {
        MyTotalValue += CurrentResource;
    }
    void Update()
    {
        // removes null Objects
        ResourceNodes.RemoveAll(node => node == null);
       DebugTesting();
        CalculateValue();
        //CollectorRef[0].SelectResource(ResourceNodes);
        ResourceCollectorManager();

        if (squadisreal)
        {
            //squadManager.KeepFormation(squadManager.Squad[0]);
        }

    }
    
    #endregion
    #region Consious Functions
    // Functions that happen as result of a descision
    private void ChooseTask(float _importance)
    {

    }
    private void AssignImportance()
    {

    }
    
    private void BuildShip(UnitController.UnitType _unitType)
    {
        switch (_unitType)
        {
            case UnitController.UnitType.Figher:

                if (CurrentResource >= GameManager.Instance.FighterCost)
                {
                    var myFighter = Instantiate(GameManager.Instance.FighterPrefab, BaseRef.GetComponent<HomeBaseUnit>().SpawnerSmall, Quaternion.identity);
                    GameManager.Instance.IDCount();
                    myFighter.ShipInit(1, MyFaction, GameManager.Instance.FighterName, UnitController.UnitType.Figher);
                    ShipList.Add(myFighter);
                    squadManager.NewShip(_unitType, myFighter);
                    CurrentResource -= GameManager.Instance.FighterCost;
                    squadisreal = true;
                }
                else
                {
                    CantAfford(_unitType);
                }

                break;

            case UnitController.UnitType.Collector:

                if (CurrentResource >= GameManager.Instance.CollectorCost)
                {
                    var myCollector = Instantiate(GameManager.Instance.CollectorPrefab, BaseRef.GetComponent<HomeBaseUnit>().SpawnerSmall, Quaternion.identity);
                    GameManager.Instance.IDCount();
                    myCollector.UnitID = int.Parse(GameManager.Instance.CollectorIDMarker + GameManager.Instance.IDCounter.ToString());
                    ShipList.Add(myCollector);
                    CurrentResource -= GameManager.Instance.CollectorCost;
                }
                else
                {
                    CantAfford(_unitType);
                }

                break;


            /*
        case UnitController.UnitType.Unselected:

            break;
        case UnitController.UnitType.HomeBase:

            break;

            */

            default:
                break;
        }

    }
    
    #endregion




    private void CantAfford(UnitController.UnitType _unitType)
    {
        Debug.Log("Cannot Afford " + _unitType.ToString());
    }

    private void ShipManager(List<GameObject> _shipList)
    {

    }

    void ResourceCollectorManager()
    {
        foreach (CollectorUnit _selectedCollector in CollectorRef)
        {
        float distance = Vector3.Distance(_selectedCollector.transform.position, BaseRef.DockingA.position);

            if (_selectedCollector.ResourceCollected >= 500 || ResourceNodes.Count == 0 || isRetreat)
            {
                _selectedCollector.MoveToPoint(_selectedCollector.MyAgent, BaseRef.DockingA.position);
                if (distance < 10)
                {
                    if (!isEnomeratorOn)
                    {

                StartCoroutine(ResourceDeposit());
                    }
                }
            }
            else
            {
                _selectedCollector.SelectResource(ResourceNodes);
            }
        }
    }

    private void UnitInstantiator(UnitController _myUnit)
    {

    }
    public void ResourceDetection(ResourceNode DetectedResource)
    {


    }

    private void CalculateValue()
    {
        MyTotalValue = CurrentResource;
        foreach (var ship in ShipList)
        {
            MyTotalValue += ship.Value;
        }


    }

    private void DebugTesting()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray _rayPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_rayPoint, out _rayinfo, 1000000000, GameGrid))
            {
                //debug testing
                //spawnAThing();
                // also moves ship
                SendInfo();

                //DEBUG
                
                
                foreach (Squad _squad in squadManager.Squad)
                {
                    for (int i = 0; i < _squad.NewSquad.Length; i++)
                    {
                        if (_squad.NewSquad[i] != null)
                        {

                        _squad.NewSquad[i].MoveToPoint(_squad.NewSquad[i].MyAgent, _squad.SquadFormationObject.transform.position);
                        }

                    }
                }
               /*
                Transform previousTransfrom = squadManager.Squad[0].SquadFormationObject.transform;
                squadManager.Squad[0].SquadFormationObject.transform.position = GameManager.Instance.RayPoint;
                squadManager.Squad[0].SquadFormationObject.transform.rotation = Quaternion.Euler(GameManager.Instance.RayPoint - previousTransfrom.position);
               */
            }
        }
        


        if (Input.GetKeyDown(KeyCode.Space) && MyFaction == 1)
        {
            BuildShip(UnitController.UnitType.Figher);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && MyFaction == 1)
        {
            BuildShip(UnitController.UnitType.Collector);
        }

        if (Input.GetKeyDown(KeyCode.H)) // CHEAT CODES
        {
            CurrentResource += 100;

        }

        if (Input.GetKeyDown(KeyCode.F)) // Toggle Follow
        {

            Squadfollow();
        }

        if (Input.GetKeyDown(KeyCode.R)) // Toggle Follow
        {
            CollectorRef[0].IsDIrectControl = !CollectorRef[0].IsDIrectControl;
        }


        Debug.Log("current value is " + MyTotalValue);
    }

    public void UnitDestroyed(UnitController DeadShip, int UnitID)
    {

    }

    void Squadfollow() // Debugging Tool
    {

        foreach (Squad _squad in squadManager.Squad)
        {
            for (int i = 0; i < _squad.NewSquad.Length; i++)
            {
                _squad.NewSquad[i].dofollow = !_squad.NewSquad[i].dofollow;
            }
        }
    }

    void SendInfo()
    {
        //Mouse Click Position
        GameManager.Instance.RayPoint = _rayinfo.point;
    }

    IEnumerator ResourceDeposit()
    {
        isRetreat = true;
        isEnomeratorOn = true;
        while (CollectorRef[0].ResourceCollected > 0)
        {
            CollectorRef[0].ResourceCollected -= 1;
            CurrentResource += 1;
            CollectorRef[0].resourceVisibility.ActivateVisibility(CollectorRef[0].ResourceCollected, 525);
            yield return new WaitForSeconds(0.02f);
        }
        isEnomeratorOn = false;
        isRetreat = false;
    }

    void RepairShip()
    {
        
    }
    #endregion
}
