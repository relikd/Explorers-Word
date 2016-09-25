using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
namespace Interaction{

/**
 * A script that extends {@link ReadInteraction}. It is used to implement an input for alphanumeric codes. It will toggle the specified canvas on interaction.
 * When the user presses "Enter" the script will verify wether the code in the inputfield is correct.
 * If the code is correct the script will toggle the given interactions and gameobjects just like  {@link ToggleInteraction}.
 */

    public class NumberlockInteraction : ReadInteraction
    {
        [SerializeField]
        String ResponseForRightCode;
        [SerializeField]
        String ResponseForWrongCode;
        [SerializeField]
        int Numbercode;
        [SerializeField]
        private InputField NumberInputField;
        [Serializable]
        enum OnRightCode{
            DoNothing,
            DeactivateThisScript,
            DeleteGameObject,
            CollectGameObject
        }
        [SerializeField]
        private OnRightCode onInteraction;
        [SerializeField]
        private Interactable[] toggleScriptsEnabledState;
        [SerializeField]
        private GameObject[] toggleGameObjectActiveState;


        /**
         * Also toggle the inputfield on the canvas when the canvas is toggled.
         */
        override protected void setCanvasVisible(bool flag) {
            base.setCanvasVisible(flag);
            if (flag)
                NumberInputField.ActivateInputField();
            else
                NumberInputField.DeactivateInputField();
        }
        /**
         * Test the code when the user presses "Spell Word" button (Default is Enter). 
         */
        void LateUpdate()
        {
            if (reading && Input.GetButtonUp("Spell Word"))
            {
                if(NumberInputField.text == Numbercode.ToString()) {
                    setCanvasVisible(false);
                    centeredMessage(ResponseForRightCode);                   
                    toggle();
                }
                else {
                    centeredMessage(ResponseForWrongCode);
                    NumberInputField.ActivateInputField();
                }
            }
        }
        /**
         * Toggles all interactions and gameobjects that were specified. Also deletes the gameobject or deactivates the script based on setting given in the Unity inspector.
         */
        private void toggle()
        {
            foreach (Interactable script in toggleScriptsEnabledState)
                if (script)
                    script.interactionEnabled = !script.interactionEnabled;
            foreach (GameObject go in toggleGameObjectActiveState)
                if (go)
                    go.SetActive(!go.activeSelf);
            // what happens after the toggle
            switch (onInteraction)
            {
                case OnRightCode.CollectGameObject:
                case OnRightCode.DeleteGameObject:
                    Destroy(gameObject);
                    break;
                case OnRightCode.DeactivateThisScript:
                    this.interactionEnabled = false;
                    break;
            }
        }
    }
}
