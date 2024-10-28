using UnityEngine;
using UnityEditor.UI;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class FighterUnit : UnitController
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables
    
    float _fireRate;
    float baseSpeed;
    

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References
   
    GameObject[] Projectile;
    [SerializeField]
    Weapon[] Weapons;
    GameObject ProtectedAlly;
 
    #endregion

    //---------------------------------------------------------------------------------------------Functions----------------------------------------------------------------------\
    #region Functions
    private void Awake()
    {
        //ShipInit();
    }
    void Start()
    {
        baseSpeed = MyAgent.speed;
        ProtectedAlly = GameManager.Instance.ShipList[0];
    }
    void Update()
    {
        //*********Debug Testing***********

        Follow(ProtectedAlly);
        //MoveToPoint( MyAgent,GameManager.Instance.RayPoint + _legalposition);
        
    }

   public void Follow(GameObject _followTarget)
    {
        float distance = Vector3.Distance(transform.position, _followTarget.transform.position);
        if (dofollow)
        {

            if (distance < 35)
            {

            MyAgent.speed = _followTarget.GetComponent<CollectorUnit>().MyAgent.speed;
            }
            else
            {
                MyAgent.speed = baseSpeed;
            }
        MoveToPoint(MyAgent, _followTarget.transform.position + _legalposition);
            
        }
    }

    private void ShootWeapon(Weapon.WeaponType weaponType , Transform _mytarget)
    {
        if (_isInCombat && _isInRange)
        {
            StartCoroutine(Attack(_mytarget));
        }
    }
    private void ChangeSpeed()
    {
        MyAgent.speed = _moveSpeed;
    }

    private void Hover()
    {

    }
    IEnumerator Attack(Transform _target)
    {
        


        yield return new WaitForSeconds(0.1f);
    }
    
    #endregion
}
