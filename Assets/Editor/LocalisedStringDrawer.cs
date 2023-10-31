using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace LingoLoom
{
    [CustomPropertyDrawer(typeof(LocalisedString))]
    public class LocalisedStringDrawer : PropertyDrawer
    {
        private bool _dropDown;
        private float _height;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_dropDown)
            {
                return _height + 25;
            }

            return 20;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.width -= 45;
            position.height = 18;

            Rect valueRect = new Rect(position);
            valueRect.x += 15;
            valueRect.width -= 15;

            Rect foldButtonRect = new Rect(position);
            foldButtonRect.width = 15;

            _dropDown = EditorGUI.Foldout(foldButtonRect, _dropDown, "");

            position.x += 15;
            position.width -= 15;
            SerializedProperty key = property.FindPropertyRelative("key");
            key.stringValue = EditorGUI.TextField(position, key.stringValue);

            position.x += position.width + 2;
            position.width = 20;
            position.height = 20;
            Texture searchIcon = (Texture)Resources.Load("Search");
            GUIContent searchContent = new GUIContent(searchIcon);

            if (GUI.Button(position, searchContent))
            {
                LingoLoomSearchWindow.Open();
            }

            position.x += position.width + 2;
            Texture storeIcon = (Texture)Resources.Load("Store");
            GUIContent storeContent = new GUIContent(storeIcon);
            if (GUI.Button(position, storeContent))
            {
                LingoLoomEditorWindow.Open(key.stringValue);
            }

            if (_dropDown)
            {
                var value = LocalisationSystem.GetLocalisedValue(key.stringValue);
                GUIStyle style = GUI.skin.box;
                _height = style.CalcHeight(new GUIContent(value), valueRect.width);

                valueRect.height = _height;
                valueRect.y += 21;
                EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
            }

            EditorGUI.EndProperty();
        }



    }
}

