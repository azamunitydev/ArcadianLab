using UnityEngine;
using UnityEngine.Events;

namespace ArcadianLab.DemoGame.ObjectPooling.Unit
{
    public class PoolItem : MonoBehaviour
    {
        [SerializeField] private string poolId;
        [SerializeField] private UnityEvent onFetchFromPool;
        [SerializeField] private UnityEvent onReturnToPool;

        public string PoolId => poolId;
        public UnityEvent OnFetchFromPool => onFetchFromPool;
        public UnityEvent OnReturnToPool => OnReturnToPool;
    }
}
