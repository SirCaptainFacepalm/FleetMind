using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;


public class TESTER_Shooter : MonoBehaviour
{
    [FormerlySerializedAs("Source")] [SerializeField]
    Transform source;

    [FormerlySerializedAs("Target")] [SerializeField]
    Transform target;

    [FormerlySerializedAs("Tag")] [SerializeField]
    string customTag = "Gatling";

    bool isCoroutineRunning = false;
    Quaternion tmpAngle;
    float firerate = 100f;
    private float attackCounter;

    void Start()
    {
        //StartCoroutine(Shoot());
    }

    void Update()
    {
        UpdateShoot();
    }

    void UpdateShoot()
    {
        if (true)
        {
            if (true)
            {
                if (!isCoroutineRunning)
                {
                    isCoroutineRunning = true;
                    ProjectileBase thisObject = ObjectPooler.instace.SpawnProjectile(customTag, 0, source, target, tmpAngle);
                    StartCoroutine(ShootDelay());
                }


            }
        }
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < 10; i++)
        {

            ProjectileBase thisObject = ObjectPooler.instace.SpawnProjectile(customTag, 0, source, target, tmpAngle);


            yield return new WaitForSeconds(thisObject.FireRate);
        }
    }

    IEnumerator ShootDelay()
    {

        yield return new WaitForSeconds(0.2f);

        isCoroutineRunning = false;
    }
}
