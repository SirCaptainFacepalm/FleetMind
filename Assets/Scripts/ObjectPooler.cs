using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObjectPooler : MonoBehaviour
{

    private int faction = 5;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public ProjectileBase projectile;
        public int poolSize;
    }

    // Singleton

    #region Singleton

    public static ObjectPooler instace;

    private void Awake()
    {
        instace = this;
        initializer();
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<ProjectileBase>> poolDictionary;

    void initializer()
    {
        poolDictionary = new Dictionary<string, Queue<ProjectileBase>>();

        foreach (Pool _pool in pools)
        {
            Queue<ProjectileBase> projectilePool = new Queue<ProjectileBase>();
            _pool.projectile.gameObject.SetActive(false);
            for (int i = 0; i < _pool.poolSize; i++)
            {
                ProjectileBase projectile = Instantiate(_pool.projectile);
                projectile.gameObject.SetActive(false);
                projectilePool.Enqueue(projectile);

            }

            poolDictionary.Add(_pool.tag, projectilePool);

        }

    }

    void EmptyPool(string tag)
    {
        Queue<ProjectileBase> projectilePool = poolDictionary[tag];
        Pool poolData = pools.Find(pool => pool.tag == tag);
        for (int i = 0; i < 50; i++)
        {
            ProjectileBase projectile = Instantiate(poolData.projectile);
            projectile.gameObject.SetActive(false);
            projectilePool.Enqueue(projectile);
            Debug.Log("Added " +"50 " + "To " + tag + " Pool");
        }
    }

    public ProjectileBase SpawnProjectile(string _tag, int _faction, Transform _origin, Transform _target,
        Quaternion _rotation)
    {
        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogError("Bad Tag input");
            return null;
        }

        ProjectileBase spawnedProjectile = poolDictionary[_tag].Dequeue();

        spawnedProjectile.origin = _origin;
        spawnedProjectile.target = _target;
        spawnedProjectile.Faction = faction;
        spawnedProjectile.transform.position = _origin.position;
        spawnedProjectile.transform.rotation = _rotation;
        spawnedProjectile.Activate();
        spawnedProjectile.gameObject.SetActive(true);

        StartCoroutine(Requeue(_tag, spawnedProjectile));


        if (poolDictionary[_tag].Count <= 50)
        {
            EmptyPool(_tag);
        }
        return spawnedProjectile;
    }

    //return to Queue
    IEnumerator Requeue(string _tag, ProjectileBase ObjectToQue)
    {
        if (ObjectToQue.ProjectileHit)
        {
            ObjectToQue.gameObject.SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(ObjectToQue._lifespan);
        poolDictionary[_tag].Enqueue(ObjectToQue);
        ObjectToQue.gameObject.SetActive(false);
    }
}
