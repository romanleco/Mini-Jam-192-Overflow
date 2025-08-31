using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Item _selectedItem;
    [SerializeField] private bool _grabbing;
    [SerializeField] private Transform _mousePosVisualizer;
    [SerializeField] private LayerMask _interactablesLM;
    private Vector3 _mousePos;
    private Vector3 _mouseVisPos;

    public static event Action OnCupGrab;
    public static event Action OnRelease;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Grab();

        if (Input.GetMouseButtonUp(0))
            Release();

        if (_grabbing)
        {
            _mousePos = Input.mousePosition;
            _mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
            _mouseVisPos = Camera.main.ScreenToWorldPoint(_mousePos);
            _mouseVisPos.z = 0;
            _mousePosVisualizer.position = _mouseVisPos;
        }
    }

    void Grab()
    {
        _grabbing = true;

        _mousePos = Input.mousePosition;
        _mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_mousePos), Vector2.zero, Mathf.Infinity, _interactablesLM);
        if (hit2D.collider != null)
        {
            Item itemScr = hit2D.collider.GetComponent<Item>();
            if (itemScr != null)
            {
                _selectedItem = itemScr;
                _selectedItem.Select(_mousePosVisualizer);

                if (itemScr.GetItemID() == 0)
                    OnCupGrab?.Invoke();
            }
        }
    }

    void Release()
    {
        _grabbing = false;

        OnRelease?.Invoke();

        if (_selectedItem != null)
        {
            _selectedItem.Deselect();
            _selectedItem = null;
        }
    }
}
