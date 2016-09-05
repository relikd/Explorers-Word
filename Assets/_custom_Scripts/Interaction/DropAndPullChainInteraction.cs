using UnityEngine;
using System.Collections;
namespace Interaction
{
    /*
     * Ein Interaktionsskript, mit dem der Kronleuchter im zweiten Raum zu Fall gebracht bzw. nach oben gezogen werden kann.
     */
    public class DropAndPullChainInteraction : Interactable
    {
        [SerializeField]
        DropAndMoveChains ChainControlScript;
        /*
         * Laesst den Kronleuchter Fallen bzw. zieht ihn nach oben. Schaltet die angezeigte Nachricht um.
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
        /*
         * Setzt die angezeigte Nachricht je nach aktuellem Zustand.
         */
        public override string interactMessage()
        {
            return !ChainControlScript.isDropping()? "Kronleuchter fallen lassen": "Kronleuchter hochziehen";
        }

    }
}
