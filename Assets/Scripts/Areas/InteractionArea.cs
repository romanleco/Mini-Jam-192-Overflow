using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    private CoffeeCup _coffeeCupIn;
    [SerializeField] protected CoffeeCup _coffeeCupOut;
    [SerializeField] protected Transform _snapPosition;
    [SerializeField] protected bool _isAreaInUse;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected float _highlightOpacity = 0.05f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cup"))
        {
            if (_coffeeCupOut != null)
            {
                CoffeeCup newCoffeeCup = other.GetComponent<CoffeeCup>();
                if (newCoffeeCup != null)
                {
                    if (newCoffeeCup != _coffeeCupOut)
                    {
                        _coffeeCupOut = newCoffeeCup;
                    }
                }
            }
            else
                _coffeeCupOut = other.GetComponent<CoffeeCup>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cup"))
        {
            if (other.GetComponent<CoffeeCup>() == _coffeeCupOut)
            {
                _coffeeCupOut = null;
            }
        }
    }

    void OnEnable()
    {
        Player.OnRelease += OnPlayerRelease;
        Player.OnCupGrab += OnPlayerCupGrab;
    }

    void OnDisable()
    {
        Player.OnRelease -= OnPlayerRelease;
        Player.OnCupGrab -= OnPlayerCupGrab;
    }

    protected virtual void OnPlayerRelease()
    {
        if(_spriteRenderer != null)
            _spriteRenderer.color = new Color(0, 0, 0, 0);

        if (_isAreaInUse) return;

        if (_coffeeCupOut != null)
        {
            _coffeeCupOut.AssignPosition(_snapPosition.position, this);
            _coffeeCupIn = _coffeeCupOut;
            _isAreaInUse = true;

            _coffeeCupOut = null;
        }
    }

    protected virtual void OnPlayerCupGrab()
    {
        if(_spriteRenderer != null)
            _spriteRenderer.color = new Color(1, 1, 1, _highlightOpacity);
    }

    public virtual void SetAreaInUse(bool value, CoffeeCup coffeeCup) => _isAreaInUse = value;
}
