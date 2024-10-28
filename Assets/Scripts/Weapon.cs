using UnityEngine;
using System;
[Serializable]
public class Weapon : ItemBase
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    public float FireRate;
    public enum WeaponType {None , Gatling , Laser , Rocket , MiningDrill };

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
