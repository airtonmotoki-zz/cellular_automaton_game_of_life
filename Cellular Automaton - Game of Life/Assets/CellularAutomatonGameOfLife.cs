using System.Collections.Generic;
using UnityEngine;

public class CellularAutomatonGameOfLife : MonoBehaviour
{
	[SerializeField]
	private bool _running = true;

	[SerializeField]
	private float _evolveRate = 1f;
	private float _nextEvolveTime = 0;

	[SerializeField]
	private Vector2Int _size;

	[SerializeField]
	private Vector2Int _maskSize = new Vector2Int(3, 3);

	[SerializeField]
	private Vector2Int _maskOrigin = new Vector2Int(1, 1);

	[SerializeField]
	private GameObject _cellPrefab;

	[SerializeField]
	private List<CellRule> _rules;

	private float _generation = 0f;

	private Cell[][] _map;

	private MatrixGridLayout Layout;

	private void Start()
	{
		_nextEvolveTime = _evolveRate;
		_generation = 0;

		Layout = GetComponent<MatrixGridLayout>();
		Layout.ColumnCount = _size.x;
		Layout.RowCount = _size.y;

		GenerateRandomMap();
	}

	private void Update()
	{
		if (_running)
		{
			_nextEvolveTime -= Time.deltaTime;
		}
		if (_nextEvolveTime <= 0f)
		{
			_nextEvolveTime = _evolveRate;
			NextGeneration();
		}
	}

	public void GenerateClearMap()
	{
		_map = new Cell[_size.x][];

		for (var x = 0; x < _size.x; x++)
		{
			_map[x] = new Cell[_size.y];
			for (var y = 0; y < _size.y; y++)
			{
				var cell = Instantiate(_cellPrefab, this.transform).GetComponent<Cell>();
				cell.Dead();
				_map[x][y] = cell;
			}
		}
	}

	public void GenerateRandomMap()
	{
		_map = new Cell[_size.x][];

		for (var x = 0; x < _size.x; x++)
		{
			_map[x] = new Cell[_size.y];
			for (var y = 0; y < _size.y; y++)
			{
				var value = (Random.Range(0, 2) == 1);
				var cell = Instantiate(_cellPrefab, this.transform).GetComponent<Cell>();
				if (value)
				{
					cell.Alive();
				}
				else
				{
					cell.Dead();
				}
				_map[x][y] = cell;
			}
		}
	}

	public void NextGeneration()
	{
		_generation++;
		ProcessNextGeneration();
		CommitNextGeneration();
	}

	public override string ToString()
	{
		string strMap = "";
		for (var x = 0; x < _map.Length; x++)
		{
			var array = _map[x];
			for (var y = 0; y < array.Length; y++)
			{
				var value = _map[x][y].Value ? 1 : 0;
				strMap += value + "\t";
			}
			strMap += "\n";
		}
		return strMap;
	}

	private int CountMask(Vector2Int mapPosition)
	{
		var result = 0;

		for (var x = -_maskOrigin.x + mapPosition.x; x < _maskSize.x - _maskOrigin.x + mapPosition.x; x++)
		{
			if (x < 0 || x >= _map.Length)
			{
				continue;
			}
			var array = _map[x];
			for (var y = -_maskOrigin.y + mapPosition.y; y < _maskSize.y - _maskOrigin.y + mapPosition.y; y++)
			{
				if (y < 0 || y >= array.Length)
				{
					continue;
				}
				if (x == mapPosition.x && y == mapPosition.y)
				{
					continue;
				}
				result += _map[x][y].Value ? 1 : 0;
			}
		}
		return result;
	}

	private void ProcessNextGeneration()
	{
		for (var x = 0; x < _map.Length; x++)
		{
			var array = _map[x];
			for (var y = 0; y < array.Length; y++)
			{
				var count = CountMask(new Vector2Int(x, y));
				var result = _rules[count];
				if (result == CellRule.BORN)
				{
					_map[x][y].NewValue = true;
				}
				else if (result == CellRule.DIES)
				{
					_map[x][y].NewValue = false;
				}
				else if (result == CellRule.LIVE)
				{
					_map[x][y].NewValue = _map[x][y].Value;
				}

			}
		}
	}

	private void CommitNextGeneration()
	{
		for (var x = 0; x < _map.Length; x++)
		{
			var array = _map[x];
			for (var y = 0; y < array.Length; y++)
			{
				_map[x][y].Value = _map[x][y].NewValue;
			}
		}
	}
}
