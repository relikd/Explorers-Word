using System;
using Interaction;

/**
* Remvoes Screws.
*/
public class RemoveScrews : Interactable
{
	/**
	 * shows an Interaction Message.
	 */
	public override string interactMessage () {			
		return "Remove Screws";
	}
		
	/**
	 * Sets its own Interaction Possibility to false.
	 */
	public override void OnInteractionKeyPressed () {
		this.interactionEnabled = false;
	}
}


