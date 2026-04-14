using UnityEngine;
using TMPro;

namespace StatusGroup
{
    public class StatusSetterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textDescription;

        public void Init(string name, string description)
        {
            _textName.text = name;
            _textDescription.text = description;
        }
    }
}