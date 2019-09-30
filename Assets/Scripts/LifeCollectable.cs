using UnityEngine;

namespace Collectables
{
    public class LifeCollectable : ICollectable
    {
        public int gain = 1;

        private bool hasBeenPickedUp = false;

        public override void Gain(GameObject player)
        {
            if (!hasBeenPickedUp)
            {
                base.Gain(player);
                PlayerDamage damageScript = player.GetComponent<PlayerDamage>();
                damageScript.livesRemaining += gain;

                damageScript.livesText.SetText("x " + damageScript.livesRemaining.ToString());

                PlayerPrefs.SetInt("PlayerCurrentLives", damageScript.livesRemaining);
            }
        }
    }
}