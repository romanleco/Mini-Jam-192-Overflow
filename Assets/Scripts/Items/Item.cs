using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _itemID;
    [SerializeField] private bool _selected;
    [SerializeField] private Interpolator _interpolatorScr;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _interactablesLM;
    private Collider2D _thisCollider;

    void Start()
    {
        _thisCollider = GetComponent<Collider2D>();
        Initialize();
    }

    protected virtual void Initialize() { }

    public virtual void Select(Transform target)
    {
        _interpolatorScr.SetGrow(true);
        _interpolatorScr.SetFollowTarget(target);
        _spriteRenderer.sortingLayerName = "Foreground";
    }

    public virtual void Deselect()
    {
        _interpolatorScr.SetGrow(false);
        _interpolatorScr.SetFollowTarget(null);
        _spriteRenderer.sortingLayerName = "Default";
        InteractWithOtherItem();
    }

    protected virtual void InteractWithOtherItem()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), _interactablesLM);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != _thisCollider)
            {
                Item otherItemScr = colliders[i].GetComponent<Item>();
                if (otherItemScr != null)
                {
                    if (otherItemScr.GetItemID() == 0)
                    {
                        CoffeeCup coffeeCupScr = otherItemScr.transform.GetComponent<CoffeeCup>();
                        if (coffeeCupScr != null)
                        {
                            switch (_itemID)
                            {
                                case 1: // Whipped Cream
                                    coffeeCupScr.AddWhippedCream();
                                    break;

                                case 2: // Ice
                                    coffeeCupScr.AddIce();
                                    break;

                                case 3: // Normal Lid
                                    coffeeCupScr.PutLid();
                                    break;

                                case 4: // Dome Lid
                                    coffeeCupScr.PutLid(1);
                                    break;

                                default:
                                    Debug.LogError("Invalid or Unrecognized item ID");
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public int GetItemID() => _itemID;
}
