using UnityEngine;

namespace Collectables
{
    public class Battery : ICollectable
    {
        public int healthGain = 1;

        private bool hasBeenPickedUp = false;

        public override void Gain(GameObject player)
        {
            if (!hasBeenPickedUp)
            {
                base.Gain(player);
                hasBeenPickedUp = true;

                PlayerDamage damageScript = player.GetComponent<PlayerDamage>();

                if (damageScript.healthRemaining < damageScript.maxHealth)
                    damageScript.healthRemaining += healthGain;

                damageScript.healthBar.fillAmount = (float)damageScript.healthRemaining / (float)damageScript.maxHealth;
            }
        }
    }

}