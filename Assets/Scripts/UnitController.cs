using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    bool isCoroutineRunning = false;
    [SerializeField]
    Weapon[] MyWeapons;
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
    
    Transform myTarget;

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
    
    // void UpdateShoot()
    // {
    //     if (true)
    //     {
    //         if (true)
    //         {
    //             foreach (var shipWeapons in MyWeapons)
    //             {
    //             if (!isCoroutineRunning)
    //             {
    //                 if (myTarget != null)
    //                 {
    //                 isCoroutineRunning = true;
    //                 ProjectileBase weaponProjectile =
    //                     ObjectPooler.instace.SpawnProjectile(shipWeapons.WeaponTypes.ToString(), 0, shipWeapons.WeaponLocation, myTarget, tmpAngle);
    //                 StartCoroutine(ShootDelay(shipWeapons.WeaponDelay));
    //                 }
    //             }
    //             }
    //
    //
    //         }
    //     }
    // }
    void OnTriggerEnter(Collider other)
    {
        foreach (Collider _visionCone in VisionCones)
        {
            if (other.TryGetComponent<UnitController>(out UnitController _detectesShip))
            {
                if (Faction != _detectesShip.Faction)
                {
                    myTarget = _detectesShip.gameObject.transform;
                    _isInRange = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Collider _visionCone in VisionCones)
        {
            if (other.TryGetComponent<UnitController>(out UnitController _detectesShip))
            {
                if (Faction != _detectesShip.Faction)
                {
                    myTarget = null;
                    _isInRange = false;
                    
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

    IEnumerator ShootDelay(float delay)
    {

        yield return new WaitForSeconds(delay);

        isCoroutineRunning = false;
    }

    #endregion
}

