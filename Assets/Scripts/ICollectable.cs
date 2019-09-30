using UnityEngine;

namespace Collectables
{
    public class ICollectable : MonoBehaviour
    {
        public virtual void Gain(GameObject player)
        {
            FindObjectOfType<AudioManager>().Play("Collect");
        }
    }
}