using UnityEngine;

namespace Assets.Scripts.Gameplay.People
{
    public class PersonColorer : MonoBehaviour
    {
        [SerializeField] Renderer face;
        [SerializeField] Renderer shirt;
        [SerializeField] Material[] faceMaterials;

        [SerializeField] Material idleShirt;
        [SerializeField] Material farmerShirt;
        [SerializeField] Material lumberjackShirt;
        [SerializeField] Material quarryShirt;
        [SerializeField] Material sacrificeShirt;

        // Use this for initialization
        void Start()
        {
            face.material = faceMaterials[Random.Range(0, faceMaterials.Length)];
        }

        public void SetJobColor(Job job)
        {
            Material m;
            switch(job)
            {
                case Job.Farmer:
                    m = farmerShirt;
                    break;
                case Job.Idle:
                    m = idleShirt;
                    break;
                case Job.Lumberjack:
                    m = lumberjackShirt;
                    break;
                case Job.Quarryworker:
                    m = quarryShirt;
                    break;
                case Job.Sacrifice:
                    m = sacrificeShirt;
                    break;
                default:
                    Debug.LogError("Unhandled color case!");
                    m = null;
                    break;                    
            }
            shirt.material = m;
        }
    }
}