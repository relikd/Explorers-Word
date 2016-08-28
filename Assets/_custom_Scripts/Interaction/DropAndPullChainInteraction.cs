using UnityEngine;
using System.Collections;
namespace Interaction
{
    /*
     * Ein Interaktionsskript, mit dem der Kronleuchter im zweiten Raum zu Fall gebreacht bzw. nach oben gezogen werden kann.
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
                LogWriter.WriteLog("Kronleuchter fallen gelassen",gameObject);
                ChainControlScript.drop();
            }
            else
            {
                LogWriter.WriteLog("Kronleuchter nach oben gezogen", gameObject);
                ChainControlScript.pull();
            }
            EnableGUI(true);

        }
        /*
         * Setzt die angezeigte Nachricht je nach aktuellem Zustand.
         */
        public override string interactMessage()
        {
            return !ChainControlScript.isDropping()? "Press to loosen chain": "Press to pull the chain";
        }

    }
}
