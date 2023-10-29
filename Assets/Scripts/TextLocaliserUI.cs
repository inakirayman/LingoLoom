using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LingoLoom
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocaliserUI : MonoBehaviour
    {
        TextMeshProUGUI _textField;
        [SerializeField] string _key;

        private void Start()
        {
            _textField = GetComponent<TextMeshProUGUI>();
            string value = LocalisationSystem.GetLocalisedValue(_key);
            _textField.text = value;
        }
    }
}

