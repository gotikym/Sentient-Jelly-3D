using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Block _block;

    public event Action<Block> BlockIsChoiced;

    private void Start()
    {
        
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
}
