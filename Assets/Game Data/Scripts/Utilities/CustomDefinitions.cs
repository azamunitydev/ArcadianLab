using System;
using System.Collections.Generic;
using ArcadianLab.DemoGame.ObjectPooling.Unit;
using UnityEngine;

namespace ArcadianLab.DemoGame.ObjectPooling.Core
{
    [Serializable]
	public class ItemData
	{
		public string PoolId;
		public int Capacity;
		public PoolItem Item;
	}
	public class Pool
	{
		public string PoolId;
		public Transform Parent;
		public Queue<PoolItem> PoolItems;
	}

}
namespace ArcadianLab.DemoGame.Gameplay.Core
{
    public enum InteractOperation
    {
        Add,
		Sub
    }
}