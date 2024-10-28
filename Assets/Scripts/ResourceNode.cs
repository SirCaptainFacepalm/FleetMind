using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------Variables----------------------------------------------------------------------\
    #region Variables

    float x;
    float y;
    float z;
    float Speed =18f;

    public float ResourceAmount;
    public bool Rotate = true;
    Vector3 Angle;

    #endregion

    //---------------------------------------------------------------------------------------------References----------------------------------------------------------------------\
    #region References

    #endregion 

    //---------------------------------------------------------------------------------------------Functions------------------------------------------------------------------------\
    #region Functions

    void Start()
    {
        Rotation();
        Angle = new Vector3(x, y, z);
    }

    void Update()
    {
        AsteroidRotation();
    }
    private void AsteroidRotation()
    {
        //float roatation = Time.deltaTime * Speed;

        if (Rotate)
        {
        transform.Rotate(Angle * Speed * Time.deltaTime);
        }
    }
    public void Rotation()
    {
        x = Random.Range(-1f,1f);
        y = Random.Range(-1f, 1f);
        z = Random.Range(-1f, 1f);

        transform.localScale = transform.localScale * Random.Range(1,2);
    }
    #endregion
}
