using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace LingoLoom
{
    public class LingoLoomEditorWindow : EditorWindow
    {
        public string key;
        public string value;

        public static void Open(string key)
        {
            LingoLoomEditorWindow window = new LingoLoomEditorWindow();
            window.titleContent = new GUIContent("LingoLoom Window");
            window.ShowUtility();
            window.key = key;
        }


        public void OnGUI()
        {
            key = EditorGUILayout.TextField("Key :",key);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", GUILayout.MaxWidth(50));
            EditorStyles.textArea.wordWrap = true;
            value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();


            if (GUILayout.Button("Add"))
            {
                if (LocalisationSystem.GetLocalisedValue(key) != string.Empty)
                {
                    LocalisationSystem.Replace(key, value);
                }
                else
                {
                    LocalisationSystem.Add(key, value);
                }
            }

            minSize = new Vector2(460, 250);
            maxSize = minSize;
        }
    }


    public class LingoLoomSearchWindow : EditorWindow
    {
        public string value;
        public Vector2 scroll;
        public Dictionary<string, string> dictionary;

        public static void Open()
        {
            LingoLoomSearchWindow window = new LingoLoomSearchWindow();

            window.titleContent = new GUIContent("LingoLoom Search");
            Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            Rect rect = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
            window.ShowAsDropDown(rect, new Vector2(500, 300));
        }

        private void OnEnable()
        {
            dictionary = LocalisationSystem.GetDictionaryForEditor();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
            value = EditorGUILayout.TextField(value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }

        private void GetSearchResults()
        {
            if(value == null)
            {
                return;
            }

            EditorGUILayout.BeginVertical();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (KeyValuePair<string, string> element in dictionary)
            {
                if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("box");
                    Texture closeIcon = (Texture)Resources.Load("Store");

                    GUIContent content = new GUIContent(closeIcon);

                    if(GUILayout.Button(content, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                    {
                        if (EditorUtility.DisplayDialog("Remove Key " + element.Key + "?", "This will remove the element from localisation, are you sure?", "Do it"))
                        {
                            LocalisationSystem.Remove(element.Key);
                            AssetDatabase.Refresh();
                            LocalisationSystem.Init();
                            dictionary = LocalisationSystem.GetDictionaryForEditor();

                        }
                    }

                    EditorGUILayout.TextField(element.Key);
                    EditorGUILayout.LabelField(element.Value);
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();


        }
    }








}

