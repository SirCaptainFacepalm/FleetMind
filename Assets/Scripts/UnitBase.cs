using UnityEngine;

public class UnitBase : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables
    public int UnitID { get; set; }
    public int SquadID { get; set; }
    public int Faction;
    public int Value { get; set; }
    public int Power { get; set; }
    public int Health
    {
        get => _health;
        set => _health = value;
    }
    public string Name { get; set; }
    [SerializeField]
    private int _health;

    public enum UnitType { Unselected, Figher, Collector, HomeBase }

    [SerializeField]
    private UnitType _unitType = UnitType.Figher;

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions

    #endregion
}
