using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetName : MonoBehaviour
{
	[SerializeField]
	private GameObject _gameObject;

	private Text _text;
	private TextMeshProUGUI _textMeshPro;
	
	void Start()
	{
		_text = GetComponent<Text>();
		_textMeshPro = GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		if (_text)
		{
			_text.text = _gameObject.name;
		}
		
		if(_textMeshPro)
		{
			_textMeshPro.text = _gameObject.name;
		}

	}
}
