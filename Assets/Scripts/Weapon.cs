using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
    public enum WeaponType {None , Gatling , Laser , Rocket , MiningDrill };
public class Weapon : ItemBase
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    
    public Transform WeaponLocation;
    public WeaponType WeaponTypes;
    
    [SerializeField]
    float fireRate ;

    public float FireRate
    {
        get { return fireRate; }
        set
        {
            fireRate = value; 
            weaponDelay = 1/fireRate;
        }
    }
    float weaponDelay; 
    public float WeaponDelay=> weaponDelay;

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    [SerializeField]
    Mesh WeaponModel;
    [SerializeField]
    AudioSource WeaponSound;
    [SerializeField]
    private WeaponType _weaponType = WeaponType.None;

    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions

  
    
    #endregion
}
