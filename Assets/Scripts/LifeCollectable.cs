using UnityEngine;

namespace Collectables
{
    public class LifeCollectable : MonoBehaviour, ICollectable
    {
        public int gain = 1;

        private bool hasBeenPickedUp = false;

        public void Gain(GameObject player)
        {
            if (!hasBeenPickedUp)
            {
                PlayerDamage damageScript = player.GetComponent<PlayerDamage>();
                damageScript.livesRemaining += gain;

                damageScript.livesText.SetText("x " + damageScript.livesRemaining.ToString());

                PlayerPrefs.SetInt("PlayerCurrentLives", damageScript.livesRemaining);
            }
        }
    }
}