using UnityEngine;
using System.Collections;

namespace ContinuousTransformation
{
	/** Moves text canvas up */
	public class CreditsTranslation : MonoBehaviour {

		[SerializeField][Range(0.2f,1.0f)] public float creditRunSpeed;
		private bool runTranslation = false;

		/** Only move text canvas if bool is set */
		void Update () {
			if (runTranslation) {
				gameObject.transform.Translate(new Vector3(0,creditRunSpeed,0));
			}
		}
		/** Set bool to start translation */
		public void StartCredits(){
			runTranslation = true;
		}
	}
}