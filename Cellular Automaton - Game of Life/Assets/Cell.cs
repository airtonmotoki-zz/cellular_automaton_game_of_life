using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	[SerializeField]
	private Color _liveColor;
	[SerializeField]
	private Color _deadColor;

	public bool Value;

	public bool NewValue;

	private Image _image;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	private void Update()
	{
		if (Value)
		{
			_image.color = _liveColor;
		}
		else
		{
			_image.color = _deadColor;
		}
	}

	public void Alive()
	{
		Value = true;
		NewValue = true;
	}

	public void Dead()
	{
		Value = false;
		NewValue = false;
	}
}
