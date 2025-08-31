using UnityEngine;

public class GarbageCan : InteractionArea
{
    [SerializeField] private Interpolator _interpolatorScr;
    void OnMouseEnter()
    {
        _interpolatorScr.SetGrow(true);
    }

    void OnMouseExit()
    {
        _interpolatorScr.SetGrow(false);
    }

    protected override void OnPlayerRelease()
    {
        if (_coffeeCupOut != null)
        {
            _coffeeCupOut.ResetCup();
        }
    }
}
