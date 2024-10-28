using UnityEngine;
using Unity.AI.Navigation;

public class Player : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables
    RaycastHit _rayinfo;
    [SerializeField]
    float CameraSpeed;
    float speedX, speedY;
    [SerializeField]
    Rigidbody rb;
    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    public LayerMask GameGrid;
    public LayerMask UnitSelection;


    [SerializeField]
    public NavMeshSurface Navmesh;
    [SerializeField]
    GameObject ObjectToSpawn;
    [SerializeField]
    Camera MyCamera;
    [SerializeField]
    AiCommander AIRef;

    #endregion

    //---------------------------------------------------------------------------------------------Functions----------------------------------------------------------------------\
    #region Functions

    void Update()
    {
        PlayerControl();
    }

    void spawnAThing()
    {
       var NewAsteroid =  Instantiate(ObjectToSpawn, _rayinfo.point, Quaternion.identity);
        AIRef.ResourceNodes.Add(NewAsteroid.GetComponent<ResourceNode>());
    }

   void PlayerControl()
    {
        WASD();
       // MyCamera.transform.position = 
        if (Input.GetMouseButtonDown(0))
        {
            Ray _rayPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_rayPoint, out _rayinfo, 1000000000, GameGrid))
            {
                //debug testing
                spawnAThing();
                SendInfo();


            }
        }
    }

    void WASD()
    {
        speedX = Input.GetAxisRaw("Horizontal") * CameraSpeed;
        speedY = Input.GetAxisRaw("Vertical") * CameraSpeed;
        rb.linearVelocity = new Vector3(speedX, speedY, 0);
    }
   

    void SendInfo()
    {
        GameManager.Instance.RayPoint = _rayinfo.point;
    }
    #endregion
}
