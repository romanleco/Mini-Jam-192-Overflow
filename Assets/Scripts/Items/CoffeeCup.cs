using UnityEngine;

public class CoffeeCup : Item
{
    [SerializeField] private SpriteRenderer _letterSR;
    [SerializeField] private SpriteRenderer _coffeeSR;
    [SerializeField] private SpriteRenderer _iceSR;
    [SerializeField] private SpriteRenderer _lidSR;
    [SerializeField] private Color[] _coffeeColors = new Color[4];
    [SerializeField] private Sprite[] _letters = new Sprite[4];
    [SerializeField] private Sprite[] _coffeeSprites = new Sprite[2];
    [SerializeField] private Sprite[] _iceSprites = new Sprite[2];
    [SerializeField] private Sprite[] _lidSprites = new Sprite[2];
    [Header("Functionality")]
    private Vector2 _startPos;
    [SerializeField] private InteractionArea _assignedArea;
    [SerializeField] private Vector2 _assignedPosition;

    protected override void Initialize()
    {
        base.Initialize();
        _startPos = transform.position;
    }

    public void Fill(int coffeeIndex)
    {
        if (HasCoffee() == false)
        {
            _coffeeSR.sprite = _coffeeSprites[0];
            _coffeeSR.color = _coffeeColors[coffeeIndex];
            _letterSR.sprite = _letters[coffeeIndex];
        }
    }

    public void AddWhippedCream()
    {
        if (HasCoffee())
        {
            if (HasWhippedCream() == false)
            {
                _coffeeSR.sprite = _coffeeSprites[1];
                _coffeeSR.color = Color.white;

                if (HasIce())
                    _iceSR.sprite = _iceSprites[1];
            }
        }
    }

    public void AddIce()
    {
        if (HasCoffee())
        {
            if (HasIce() == false)
            {
                if (HasWhippedCream())
                    _iceSR.sprite = _iceSprites[1];
                else
                    _iceSR.sprite = _iceSprites[0];
            }
        }
    }

    public void PutLid(int lidType = 0)
    {
        if (HasLidOn()) return;

        if (lidType == 0)
        {
            if (HasWhippedCream() == false)
            {
                _lidSR.sprite = _lidSprites[lidType];
            }
        }
        else
            _lidSR.sprite = _lidSprites[lidType];
    }

    protected override void InteractWithOtherItem() { }

    public bool HasCoffee() => _letterSR.sprite != null;
    public bool HasIce() => _iceSR.sprite != null;
    public bool HasWhippedCream() => _coffeeSR.sprite == _coffeeSprites[1];
    public bool HasLidOn() => _lidSR.sprite != null;

    public void AssignPosition(Vector2 pos, InteractionArea area)
    {
        if (_assignedArea != null && _assignedArea != area) _assignedArea.SetAreaInUse(false, this);

        _assignedPosition = pos;
        _assignedArea = area;
        transform.position = pos;
    }

    public override void Deselect()
    {
        base.Deselect();
        if (_assignedArea != null)
        {
            transform.position = _assignedPosition;
        }
        else
        {
            transform.position = _startPos;
        }
    }

    public void ResetCup()
    {
        if(_assignedArea != null)
            _assignedArea.SetAreaInUse(false, this);
            
        _assignedArea = null;

        _coffeeSR.sprite = null;
        _iceSR.sprite = null;
        _lidSR.sprite = null;
        _letterSR.sprite = null;

        transform.position = _startPos;
    }
}
