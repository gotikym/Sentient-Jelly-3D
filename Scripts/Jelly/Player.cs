using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string PointsKey = "Points";

    private int _points;
    private Block _block;

    public int Points => _points;

    public event Action<Block> BlockIsChoiced;
    public event Action<int> PointsChanged;

    private void Start()
    {
        _points = PlayerPrefs.GetInt(PointsKey);
    }

    private void Update()
    {
        ChoiceBlock();
    }

    private void ChoiceBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.transform.GetComponent<Block>())
            {
                _block = hit.transform.GetComponent<Block>();
                BlockIsChoiced?.Invoke(_block);
            }
        }
    }

    public void AddPoints(int points)
    {
        if (points > 0)
            _points += points;

        ChangePoints();
    }

    public void BuyGoods()
    {
        //_points -= goods.Price;
        PointsChanged?.Invoke(_points);
        //BuyedGoods?.Invoke(goods);
        ChangePoints();
    }

    private void ChangePoints()
    {
        PointsChanged?.Invoke(_points);
        PlayerPrefs.SetInt(PointsKey, _points);
    }
}
