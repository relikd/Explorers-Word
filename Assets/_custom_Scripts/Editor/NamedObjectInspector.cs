using UnityEngine;
using UnityEditor;

namespace XplrUnityExtension
{
	/**
	 * Use custom drawing for any NamedObject property in Inspector window
	 * @see NamedObject
	 */
	[CustomPropertyDrawer(typeof(NamedObject))]
	public class NamedObjectInspector : PropertyDrawer
	{
		const float CELL_HEIGHT = 16.0f;
		const float CELL_PAD_WIDTH = 4.0f;
		const float CELL_PAD_HEIGHT = 4.0f;
		const float ICON_SIZE = 32.0f;

		/**
		 * Calculate assumed element height
		 * @return single line if collapsed. Otherwise depends on the word count
		 */
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			int rows = Mathf.CeilToInt(property.FindPropertyRelative ("names").arraySize / 3.0f);
			if (!property.isExpanded) rows = -1; // we just want one single line
			return (CELL_HEIGHT + CELL_PAD_HEIGHT) * (rows+2);// +1 for the object field, +1 for the icon
		}
		/**
		 * No real functionality, just make sure the name is accessible in {@link #OnGUI(Rect,SerializedProperty,GUIContent)}
		 */
		void checkClassConsistency() {
			// make sure properties with this name exist, otherwise change name in OnGUI()
			NamedObject o = new NamedObject (); o.names = null; o.thing = null; o.icon = null;
		}
		/**
		 * Render UI Elements based on actual data. Dynamic text field size and three strings side by side
		 */
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty theNames = property.FindPropertyRelative ("names");
			SerializedProperty theObject = property.FindPropertyRelative ("thing");
			SerializedProperty theIcon = property.FindPropertyRelative ("icon");

			float widthDiff = position.width - EditorGUI.IndentedRect (position).width;
			float txtWidth = (position.width + 2*(widthDiff - CELL_PAD_WIDTH)) / 3.0f;
			float cWidth = (CELL_PAD_WIDTH + txtWidth - widthDiff);
			float rWidth = (CELL_PAD_HEIGHT + CELL_HEIGHT);
			float addedNameFieldWidth = 30 + widthDiff;

			int nameCount = theNames.arraySize-1; // suppress last empty field
			string theObjectName = "(not assigned)";
			if (theObject.objectReferenceValue != null) {
				theObjectName = theObject.objectReferenceValue.name + " ["+(nameCount)+"]";
				// has to be a scene object, not a stored asset
				if (EditorUtility.IsPersistent (theObject.objectReferenceValue))
					theObject.objectReferenceValue = null;
			}


			// DISPLAY property
			EditorGUI.BeginProperty (position, label, property);

			Rect rectExpand = new Rect (position.x, position.y, (property.isExpanded ? 6+widthDiff : position.width), CELL_HEIGHT);
			property.isExpanded = EditorGUI.Foldout (rectExpand, property.isExpanded, (property.isExpanded ? "" : theObjectName));

			if (property.isExpanded) {
				// DISPLAY object field
				Rect rectObject = new Rect (position.x, position.y, position.width - ICON_SIZE - CELL_PAD_WIDTH, CELL_HEIGHT);
				EditorGUI.PropertyField (rectObject, theObject, GUIContent.none);

				// DISPLAY icon field
				Rect rectIconField = rectObject;
				rectIconField.y += CELL_HEIGHT + CELL_PAD_HEIGHT;
				Rect rectIcon = new Rect (position.xMax - ICON_SIZE, position.yMin, ICON_SIZE, ICON_SIZE);
				EditorGUI.PropertyField( rectIconField, theIcon, GUIContent.none, false );
				DrawTexture(theIcon, rectIcon);

				// DISPLAY names field
				for (int i = 0; i <= nameCount; i++) {
					int col = i % 3;
					int row = Mathf.FloorToInt (i / 3.0f) + 2; // +1 object field, +1 icon field
					Rect pos = new Rect (position.x + col*cWidth, position.y + row*rWidth, txtWidth, CELL_HEIGHT);

					if (i == nameCount)
						pos.width = addedNameFieldWidth;
					EditorGUI.PropertyField (pos, theNames.GetArrayElementAtIndex (nameCount-i), GUIContent.none);
				}

				// modify to have last cell always blank 
				if (theNames.arraySize == 0 || !isEmptyIndex (theNames, 0)) {
					theNames.InsertArrayElementAtIndex (0);
					theNames.GetArrayElementAtIndex (0).stringValue = ""; // otherwise it will copy old value
				}

				// nameCount will change during deletion
				while (theNames.arraySize > 1 && isEmptyIndex (theNames, 1))
					theNames.DeleteArrayElementAtIndex (1);
			}

			EditorGUI.EndProperty ();
		}
		/**
		 * Helper function to draw one single icon
		 */
		private void DrawTexture(SerializedProperty iconProp, Rect rect) {
			if (iconProp.objectReferenceValue != null) {
				Texture2D tex = (Texture2D)iconProp.objectReferenceValue;
				//EditorGUI.DrawTextureTransparent(rect, tex, ScaleMode.ScaleToFit);
				EditorGUI.DropShadowLabel(rect, new GUIContent(tex));// or LabelField
			} else {
				// Draw a visibly unconfigured rectangle
				EditorGUI.DrawRect( rect, Color.magenta );
			}
		}
		/**
		 * Check if there is a word assigned at specified index
		 * @return true if no text or an empty assigned text
		 */
		private bool isEmptyIndex(SerializedProperty array, int index) {
			return (array.GetArrayElementAtIndex (index).stringValue.Length == 0);
		}
	}
}