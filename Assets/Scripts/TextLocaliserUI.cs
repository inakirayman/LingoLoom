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
        public LocalisedString LocalisedString;

        private void Start()
        {
            _textField = GetComponent<TextMeshProUGUI>(); 
            _textField.text = LocalisedString.value;
        }
    }
}

