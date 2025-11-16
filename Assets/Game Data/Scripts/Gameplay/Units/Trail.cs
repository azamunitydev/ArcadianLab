using TMPro;
using UnityEngine;

namespace ArcadianLab.DemoGame.Gameplay.Unit
{
    public class Trail : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI trailText;
        [SerializeField] private Rigidbody rb;

        public TextMeshProUGUI TrailText => trailText;
        public Rigidbody Rigid => rb;
    }
}
