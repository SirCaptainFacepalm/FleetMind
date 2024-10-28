using UnityEngine;
using System.Collections.Generic;


public class SquadManager : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    int maxSquadSize = 6;
    public List<Squad> Squad = new List<Squad>();





    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions

    public void NewShip(UnitController.UnitType _unitType, UnitController newShip)
    {
        bool isSquadFull = true;

        switch (_unitType)
        {
            case UnitController.UnitType.Figher:
                // Checks is Squad List is Empty
                if (Squad.Count > 0)
                {
                    foreach (Squad _squad in Squad)
                    {
                        if (_squad.squadsize < maxSquadSize)
                        {
                            for (int i = 0; i < _squad.NewSquad.Length + 1; i++)
                            {
                                if (_squad.NewSquad[i] == null)
                                {
                                    _squad.NewSquad[i] = newShip;
                                    _squad.NewSquad[i]._legalposition = _squad.FormationOffset[i];
                                    Squad.Add(_squad);
                                    _squad.NewSquad[i].ShipFormationObject = Squad[i].SquadFormationObject;
                                    _squad.squadsize++;
                                    isSquadFull = false;
                                    break;
                                }
                            }
                        }
                    }
                    // Adds a new squad to the list if no available space in existing squad
                    if (isSquadFull)
                    {
                        NewSquad(newShip);
                    }
                }
                else // gets here first time when the Squad list is empty and adds in its first Squad
                {
                    NewSquad(newShip);
                }

                break;
            case UnitController.UnitType.Collector:

                break;

            default:
                break;
        }
    }

    void NewSquad(UnitController newShip)
    {
        Squad _squad = new Squad();
        _squad.squadsize = 1;
        Squad.Add(_squad);
        _squad.NewSquad[0] = newShip;
        _squad.NewSquad[0]._legalposition = _squad.FormationOffset[0];
        // Generates and assigns a new formation object to the new squad 
        var NewFormationObject = Instantiate(GameManager.Instance.FormationPrefab, newShip.transform.position, Quaternion.identity);
        _squad.SquadFormationObject = NewFormationObject;
        _squad.NewSquad[0].ShipFormationObject = NewFormationObject;
    }

    public void KeepFormation(Squad _squad)
    {

        /*
        float rotationAngle = Vector3.Angle(_squad.NewSquad[0].transform.position.normalized, GameManager.Instance.RayPoint.normalized);
        Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
        for (int i = 0; i < _squad.NewSquad.Length; i++)
        {
            // FormationOffset[6] is the formation Pivot Point
            Vector3 translatedPoint = _squad.FormationOffset[i] - _squad.FormationOffset[6];

            Vector3 rotatedPoint = rotation * translatedPoint;

            _squad.FormationOffset[i] = rotatedPoint + _squad.FormationOffset[6];
        }
        */

        Vector3 targetDirection = GameManager.Instance.RayPoint;
        // Calculate the direction vectors
        Vector3 currentDirection = _squad.NewSquad[0].transform.forward; // Assuming forward is the direction to rotate from
        Vector3 targetDir = (targetDirection - _squad.NewSquad[0].transform.position).normalized;

        // Calculate the angle between the current direction and the target direction
        float rotationAngle = Vector3.Angle(currentDirection, targetDir);

        // Calculate the rotation axis
        Vector3 rotationAxis = Vector3.Cross(currentDirection, targetDir).normalized;

        // Create the quaternion for rotation
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);

        // Rotate the formation around the pivot point
        for (int i = 0; i < _squad.NewSquad.Length; i++)
        {
            // FormationOffset[6] is the formation Pivot Point
            Vector3 translatedPoint = _squad.FormationOffset[i] - _squad.FormationOffset[6];

            // Apply the rotation
            Vector3 rotatedPoint = rotation * translatedPoint;

            // Translate the point back to its original position
            _squad.FormationOffset[i] = rotatedPoint + _squad.FormationOffset[6];
        }




    }

    public void ShipDeath()
    {



    }

    #endregion
}
