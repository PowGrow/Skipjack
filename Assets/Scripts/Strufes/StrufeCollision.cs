using UnityEngine;
using Zenject;

public class StrufeCollision : MonoBehaviour
{
    private IStrufeFactory _strufeFactory;
    [Inject]
    public void Construct(IStrufeFactory strufeFactory)
    {
        _strufeFactory = strufeFactory;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Strufe strufe))
        {
            other.gameObject.SetActive(false);
            _strufeFactory.PlaceStrufe(other.gameObject);
        }

        if(other.TryGetComponent(out Enemy enemy))
        {
            other.gameObject.SetActive(false);
        }
    }
}
