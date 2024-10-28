using UnityEngine;
using System.Collections.Generic;

public class ResourceVisibility : MonoBehaviour
{

    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    float minSize = 0.1f;
    float maxSize = 1f;
    [SerializeField]
    List<Transform> ResourceParent;



    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    #endregion

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions
    public void ActivateVisibility(int _currentAmount, int _MaxAmount)
    {
        int index = Mathf.FloorToInt(_currentAmount / (float)_MaxAmount * ResourceParent.Count);

        index = Mathf.Clamp(index, 0, ResourceParent.Count);
        if (_currentAmount>0)
        {
        int ActiveAmount = Mathf.FloorToInt(_MaxAmount / _currentAmount);
        }

        for (int i = 0; i < ResourceParent.Count; i++)
        {
            if (i <= index)
            {
                ResourceParent[i].gameObject.SetActive(true);
            }
            else
            {
                ResourceParent[i].gameObject.SetActive(false);
            }
        }
        RescaleTest(_currentAmount, _MaxAmount);
    }

  
    public void RescaleTest(int _currentAmount, int _MaxAmount)
    {
        int tempAmount = _currentAmount;
        int amountPerObject = Mathf.RoundToInt(_MaxAmount / ResourceParent.Count);

        int amountToPlace = 0;
        float wantedSize = 0;

        for (int i = 0; i < ResourceParent.Count; i++)
        {
            amountToPlace = Mathf.Clamp(tempAmount, 0, amountPerObject);
            wantedSize = Map(amountToPlace, 0, amountPerObject, minSize, maxSize);
            ResourceParent[i].localScale = Vector3.one * wantedSize;
            tempAmount -= amountToPlace;
        }
    }

    public float Map(float value, float inMin, float inMax, float OutMin, float outMax)
    {
        return (value - inMin) * (outMax - OutMin) / (inMax - inMin) + OutMin;
    }
    #endregion
}
