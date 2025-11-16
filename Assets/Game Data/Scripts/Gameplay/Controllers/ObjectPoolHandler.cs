using System.Collections.Generic;
using ArcadianLab.DemoGame.ObjectPooling.Core;
using ArcadianLab.DemoGame.ObjectPooling.Unit;
using UnityEngine;

namespace ArcadianLab.DemoGame.ObjectPooling.Controller
{
    public class ObjectPoolHandler : MonoBehaviour
    {
        public static ObjectPoolHandler Instance;

        [SerializeField] private PoolData poolData;

        private List<Pool> pools = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        private void Start()
        {
            CreatePools();
        }
        private void CreatePools()
        {
            if (pools == null) pools = new List<Pool>();
            for (int i = 0; i < poolData.Items.Length; i++)
            {
                Pool pool = new Pool();
                pool.PoolId = poolData.Items[i].PoolId;
                GameObject parent = new GameObject($"{poolData.Items[i].PoolId} Pool");
                parent.transform.localPosition = Vector3.zero;
                parent.transform.localRotation = Quaternion.identity;
                pool.Parent = parent.transform;
                pool.PoolItems = new Queue<PoolItem>();
                for (int j = 0; j < poolData.Items[i].Capacity; j++)
                {
                    PoolItem item = Instantiate(poolData.Items[i].Item);
                    item.gameObject.SetActive(false);
                    item.transform.parent = pool.Parent;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    pool.PoolItems.Enqueue(item);
                }
                pools.Add(pool);
            }
        }
        public PoolItem GetItem(string poolId)
        {
            for (int i = 0; i < pools.Count; i++)
                if (pools[i].PoolId == poolId)
                {
                    PoolItem item = pools[i].PoolItems.Dequeue();
                    if (item != null) return item;
                }
            return null;
        }
        public void ReturnItemToPool(PoolItem item)
        {
            for (int i = 0; i < pools.Count; i++)
                if (pools[i].PoolId == item.PoolId)
                {
                    item.transform.parent = pools[i].Parent;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    item.gameObject.SetActive(false);
                    pools[i].PoolItems.Enqueue(item);
                }
        }
        public Pool GetPoolById(string poolId)
        {
            Pool pool = null;
            for (int i = 0; i < pools.Count; i++)
                if (pools[i].PoolId == poolId)
                {
                    pool = pools[i];
                    break;
                }
            return pool;
        }
    }
}
