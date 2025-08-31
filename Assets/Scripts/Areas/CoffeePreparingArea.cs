using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Build;
using UnityEngine;

public class CoffeePreparingArea : InteractionArea
{
    private bool[] _spotsUsed = new bool[3];
    [SerializeField] private Transform[] _spots = new Transform[3];
    private CoffeeCup[] _coffeeCups = new CoffeeCup[3];
    protected override void OnPlayerRelease()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = new Color(0, 0, 0, 0);

        if (_isAreaInUse) return;

        if (_coffeeCupOut != null)
        {
            for (int i = 0; i < _spotsUsed.Length; i++)
            {
                if (_spotsUsed[i] == false)
                {
                    _coffeeCupOut.AssignPosition(_spots[i].position, this);
                    _coffeeCups[i] = _coffeeCupOut;
                    _spotsUsed[i] = true;
                    break;
                }
            }

            if (_spotsUsed[0] == true && _spotsUsed[1] == true && _spotsUsed[2] == true)
                _isAreaInUse = true;

            _coffeeCupOut = null;
        }
    }

    public override void SetAreaInUse(bool value, CoffeeCup coffeeCup)
    {
        base.SetAreaInUse(value, coffeeCup);
        for (int i = 0; i < _coffeeCups.Length; i++)
        {
            if (coffeeCup == _coffeeCups[i])
            {
                _coffeeCups[i] = null;
                _spotsUsed[i] = false;
                break;
            }
        }
    }
}
