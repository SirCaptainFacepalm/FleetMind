using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class UnitController : UnitBase
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables



    string _formation;

    public float _moveSpeed
    {
        get;
        protected set;
    }

    float _turnSpeed;
    float _acceleration;
    float _sightRadius;
    float[] _cooldown;
    float[] _range;

    Vector3 _destination;
    public Vector3 _legalposition = new Vector3(0, 0, 0);
    Vector3 NextPosition;

    public bool dofollow = true;
    public bool _isInCombat;
    public bool _isInRange;
    



    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References
    [SerializeField]
    protected Transform LookPosition;
    GameObject _HomeBase;
    GameObject _EnemyBase;
    GameObject _EnemyTarget;
    public GameObject ShipFormationObject;
    public NavMeshAgent MyAgent;
    [SerializeField]
    Transform Gun1;
    [SerializeField]
    Transform Gun2;

    [SerializeField] 
    List<Collider> VisionCones;

    // What the AI Knows

    List<GameObject> ResourceInVision;
    // Resources

    // Enemies
    List<GameObject> EnemiesInVision;
    #endregion

    //---------------------------------------------------------------------------------------------Functions----------------------------------------------------------------------\
    #region Functions

    void Update()
    {
        if (MyAgent != null)
        {
            NextPosition = MyAgent.nextPosition;
        }
        Death();
    }

    public void ShipInit(int _squadID, int _faction, string _name, UnitController.UnitType _unitType)
    {
        var myship = this;
        UnitID = int.Parse(GameManager.Instance.FighterIDMarker + GameManager.Instance.IDCounter.ToString());
        SquadID = _squadID;
        Faction = _faction;
        Name = _name;
        switch (_unitType)
        {
            case UnitController.UnitType.Figher:

                Health = GameManager.Instance.FighterHealth;
                Value = GameManager.Instance.FighterValue;
                break;

            case UnitController.UnitType.Collector:
                Health = GameManager.Instance.CollectorHealth;
                Value = GameManager.Instance.CollectorValue;
                break;
            default:
                break;
        }
    }

    public void MoveToPoint(NavMeshAgent _BaseAgent, Vector3 _waypoint)
    {
        _BaseAgent.SetDestination(_waypoint);
        if (_BaseAgent.nextPosition - transform.position == _legalposition)
        {
            //_BaseAgent.transform.rotation.Equals(0)
        }
        else
        {
            FaceTarget(LookPosition);
        }
        //KeepFormation();
    }
    private void FaceTarget(Transform _lookPosition)
    {

    }
    protected void Retreat()
    {

    }
    protected void Follow()
    {

    }
    protected void KeepFormation(bool inFormation, string formation)
    {

    }

    public void Attack()
    {
        if (true && _isInRange)
        {

        }
    }

    public void TakeDamage(int _damage , int enemyFaction)
    {
        Health -= _damage;
        Debug.Log(_damage+ " Damage Taken" + "Health is now " + Health);
        if (Health <= 0)
        {
            Death();
        }
    }
    protected void Death()
    {
        var myship = this;
        if (Health <= 0)
        {
            GameManager.Instance.ShipDestroyed(myship, Faction, UnitID);
            Debug.Log(Name + "ID" + UnitID + " Says " + "i die!!!");
            Destroy(gameObject);
            // need to remove from lists...
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision start");
        GameObject DetectedObject = collision.gameObject;
        if (!DetectedObject.CompareTag("Untagged"))
        {

            if (DetectedObject.CompareTag("Resource"))
            {
                // May need to Iptimize Remove Get Component
                ResourceNode DetectedResource = DetectedObject.GetComponent<ResourceNode>();
                GameManager.Instance.Commanders[0].ResourceDetection(DetectedResource);
                Debug.Log("Resource detected");
            }
            Debug.Log("MidTest");
            if (DetectedObject.CompareTag("Ship"))
            {
                if (!ResourceInVision.Contains(DetectedObject))
                {
                    ResourceInVision.Add(DetectedObject);
                }
            }


        }
    }

    IEnumerator FireWeapon(int _fireRate, Transform[] _weaponOrigin, GameObject _projectile , GameObject _target)
    {
        foreach (Transform _Weapon in _weaponOrigin)
        {



        }
        yield return new WaitForSeconds(_fireRate);
    }
    #endregion
}

