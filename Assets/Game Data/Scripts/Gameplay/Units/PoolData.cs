using ArcadianLab.DemoGame.ObjectPooling.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pool", menuName = "Object Pool/Pool")]
public class PoolData : ScriptableObject
{
    [SerializeField] private ItemData[] items;

    public ItemData[] Items => items;
}
