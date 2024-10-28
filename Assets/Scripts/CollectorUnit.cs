using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CollectorUnit : UnitController
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables
    public int ResourceCollected;
    int _maxResource = 500;
    int _currentStack;
    int ResourceLayer = 8;
    int resourceCheck;
    float GatherAmount = 1f;
    public float _stackSize = 1f;
    [SerializeField]
    float GatherDistance = 10;
    public bool IsDIrectControl = false;

    bool isCoroutineOn = false;
    bool isTargetDepleted = false;
    RaycastHit _rayinfo;

    ResourceNode TargetResource;
    public Light LaserLight1;
    public Light LaserLight2;


    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    [SerializeField]
    MeshRenderer[] ResourceMesh;
    [SerializeField]
    GameObject[] ResourceSizeContainers;
    [SerializeField]
    Transform DrillPosition;
    [SerializeField]
    public ResourceVisibility resourceVisibility;

    public GameObject LaserRef;

    #endregion

    //---------------------------------------------------------------------------------------------Functions----------------------------------------------------------------------\
    #region Functions

    void Start()
    {
        //StartCoroutine(MineRoutine());
    }
    void Update()
    {
        LightFlicker();
        DebugMove();
        // DisplayResource();
        // ResourceSize();
    }
    void MineResource()
    {

    }
    void HarvestResource() // *******applied in Collision Enter********
    {

    }



    public void SelectResource(List<ResourceNode> _ResourceCollection)
    {
        float closestDistance = Mathf.Infinity;

        if (!isCoroutineOn)
        {

            foreach (ResourceNode _resourceNode in _ResourceCollection)
            {
                float distance = Vector3.Distance(DrillPosition.position, _resourceNode.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    TargetResource = _resourceNode;
                }

                Ray _rayPoint = new Ray(DrillPosition.position, _resourceNode.transform.position);

                mineResource(TargetResource);
                // MoveToPoint(MyAgent, TargetResource.transform.position);
                if (Physics.Raycast(_rayPoint, out _rayinfo, ResourceLayer))
                {
                }
                //_resourceNode.transform.position
            }
        }
    }

    void DebugMove()
    {
        if (IsDIrectControl)
        {
            MoveToPoint(MyAgent, GameManager.Instance.RayPoint);
        }

    }

    void mineResource(ResourceNode _targetResource)
    {
        bool _inRange = true;
        // need to set offset
        float distance = Vector3.Distance(transform.position, TargetResource.transform.position);
        if (distance > GatherDistance)
        {
            MoveToPoint(MyAgent, TargetResource.transform.position);
        }
        else
        {
            MyAgent.speed = 0;
            if (!isCoroutineOn)
            {
                StartCoroutine(MineRoutine(_targetResource));
            }
        }
    }

    public void DepositResource(Transform _DepositPoint)
    {
        if (true)
        {
            MoveToPoint(MyAgent, _DepositPoint.position);
        }
    }

    void LightFlicker()
    {
        
        if (Random.Range(0f, 0.7f) > Random.Range(0.5f,1f))
        {

        LaserLight1.intensity = Random.Range(35, 250);
        LaserLight2.intensity = Random.Range(35, 250);
        }
    }


    IEnumerator MineRoutine(ResourceNode _targetResource)
    {
        isCoroutineOn = true;
        transform.LookAt(_targetResource.transform);
        LaserRef.gameObject.SetActive(true);
        if (_targetResource.ResourceAmount > 0)
        {
            for (int i = 0; i < _targetResource.ResourceAmount / GatherAmount; i++)
            {
                _targetResource.ResourceAmount -= GatherAmount;
                ResourceCollected += System.Convert.ToInt32(GatherAmount);
                _stackSize++;
                resourceVisibility.ActivateVisibility(ResourceCollected, _maxResource);
                yield return new WaitForSeconds(0.02f);
            }
        }
        if (_targetResource.ResourceAmount <= 0)
        {
            Destroy(_targetResource.gameObject);
        }
        isCoroutineOn = false;
        MyAgent.speed = 12;
        LaserRef.gameObject.SetActive(false);
    }


    IEnumerator Deposit()
    {
        if (true)
        {

            for (int i = 0; i < ResourceCollected / GatherAmount; i++)
            {



            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDrawGizmos()
    {
        if (TargetResource == null)
        {
            return;
        }
        Vector3 targetPosition = TargetResource.transform.position;
        Gizmos.DrawWireSphere(targetPosition, GatherDistance);
    }

    #endregion
}
