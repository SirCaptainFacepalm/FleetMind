using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileBase : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    public float _speed;
    public float _lifespan;
    public float FireRate;
    public int Damage;
    public int Faction;
    public bool ProjectileHit = false;
    private bool isNewTarget = true;
    private bool isInitialized = false;
    

    public Transform origin;
    public Transform target;
    private Transform FixedTarget;
    private Transform FixedOrigin;

    float _trailSpeed = 2000f;
    float instantiationTime;


    //int ProjectileDamage = GameManager.Instance.FighterDamage;

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    [SerializeField]
    Transform ProjectileTrail;
    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions
    private void Start()
    {
        ProjectileTrail.transform.Rotate(0f,0f,Random.Range(0f,1f));
        Damage = GameManager.Instance.FighterDamage;
        instantiationTime = Time.time;
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        TrailRotation();
    }
    void LifeSpan()
    {
        float elapsedTime = Time.time - instantiationTime;
        if (elapsedTime > _lifespan)
        {
            Destroy(gameObject);
        }
    } 
    private void OnEnable()
    {
        if (FixedOrigin != null || FixedTarget != null)
        {
            return;
        }
         direction = (FixedTarget.position - FixedOrigin.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    private void OnDisable()
    {
    }

    private Vector3 direction;
    private void MoveToTarget()
    {
      transform.position += direction * _speed*Time.fixedDeltaTime;
    }
    private void TrailRotation()
    {
       Vector3 taildirection = new Vector3(0f,0f,1f);
        ProjectileTrail.Rotate(taildirection * _trailSpeed * Time.deltaTime);
    }
    public void Activate()
    {
            FixedTarget = target;
            FixedOrigin = origin;
    }
/*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<UnitController>(out UnitController targetShip))
        {
            if (targetShip.Faction != Faction)
            {
                targetShip.TakeDamage(Damage , Faction);
            }
        }
    }
*/
    void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.TryGetComponent<UnitController>(out UnitController targetShip))
        {
            if (targetShip.Faction != Faction)
            {
                ProjectileHit = true;
                targetShip.TakeDamage(Damage , Faction);
                Debug.Log("HIT!. " + Damage +" Done");
                //this.gameObject.SetActive(false); WHAT THE FUCK!?!?!?!?!
            }
        }
    }
    #endregion
}
