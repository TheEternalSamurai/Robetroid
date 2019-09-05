using UnityEngine;

namespace Collectables
{
    public interface ICollectable
    {
        void Gain(GameObject player);
    }
}