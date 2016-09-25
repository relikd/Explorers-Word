using UnityEngine;
using System.Collections;

namespace Interaction
{
    /**
     * Interaction script for the chandelier in room 2, handles the up down movement
     */
    public class DropAndPullChainInteraction : Interactable
    {
        [SerializeField] DropAndMoveChains ChainControlScript;
        /**
         * Change the interaction text and perform drop or pull action
         * @see DropAndMoveChains
         */
        public override void OnInteractionKeyPressed()
        {
            EnableGUI(false);
            if (!ChainControlScript.isDropping()) {
				XplrDebug.LogWriter.Write("Kronleuchter fallen gelassen",gameObject);
                ChainControlScript.drop();
            }
            else
            {
				XplrDebug.LogWriter.Write("Kronleuchter nach oben gezogen", gameObject);
                ChainControlScript.pull();
            }
            EnableGUI(true);

        }
        /**
         * Display action text based on private bool
         */
        public override string interactMessage()
        {
            return !ChainControlScript.isDropping()? "Kronleuchter fallen lassen": "Kronleuchter hochziehen";
        }
    }
}
