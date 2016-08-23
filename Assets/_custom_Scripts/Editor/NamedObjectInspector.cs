using UnityEngine;
using UnityEditor;
/*
[CustomPropertyDrawer(typeof(NamedObject))]
public class NamedObjectInspector : PropertyDrawer
{
	const float CELL_HEIGHT = 16.0f;
	const float CELL_PAD_WIDTH = 4.0f;
	const float CELL_PAD_HEIGHT = 4.0f;

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		int rows = Mathf.CeilToInt(property.FindPropertyRelative ("names").arraySize / 3.0f);
		if (!property.isExpanded) rows = 0;
		return (CELL_HEIGHT + CELL_PAD_HEIGHT) * (rows+1);// +1 for the object field
	}

	void checkClassConsistency() {
		// make sure properties with this name exist, otherwise change name in OnGUI()
		NamedObject o = new NamedObject (); o.names = null; o.thing = null;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty theNames = property.FindPropertyRelative ("names");
		SerializedProperty theObject = property.FindPropertyRelative ("thing");

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
			Rect rectObject = new Rect (position.x, position.y, position.width, CELL_HEIGHT);
			EditorGUI.PropertyField (rectObject, theObject, GUIContent.none);

			// DISPLAY names field
			for (int i = 0; i <= nameCount; i++) {
				int col = i % 3;
				int row = Mathf.FloorToInt (i / 3.0f) + 1; // +1 because of the object field
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

	private bool isEmptyIndex(SerializedProperty array, int index) {
		return (array.GetArrayElementAtIndex (index).stringValue.Length == 0);
	}
}
*/