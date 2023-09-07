using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StrufeFactory : IStrufeFactory
{
    public List<Strufe> Strufes { get; private set; }
    public float StrufeWidth { get; private set; }

    private GameObject _strufe;
    private GameObject _strufeContainer;
    private DiContainer _container;
    private Vector3 _strufeAddPosition;
    public StrufeFactory(GameObject strufeContainer, GameObject strufe, DiContainer container, int pullSize, Vector3 initialStrufePosition)
    {
        _strufeContainer = strufeContainer;
        _strufe = strufe;
        StrufeWidth = _strufe.transform.localScale.x;
        _container = container;
        _strufeAddPosition = new Vector3(initialStrufePosition.x, initialStrufePosition.y + pullSize - 1, initialStrufePosition.z + pullSize - 1);
        Strufes = CreateStrufes(pullSize, initialStrufePosition);
    }

    public void PlaceStrufe(GameObject strufe)
    {
        strufe.transform.position = _strufeAddPosition;
        strufe.SetActive(true);
    }

    private List<Strufe> CreateStrufes(int pullSize, Vector3 initialStrufePosition)
    {
        var strufes = new List<Strufe>(pullSize);
        for (int index = 0; index < pullSize; index++)
        {
            var strufe = CreateStrufe(new Vector3(initialStrufePosition.x, initialStrufePosition.y + index, initialStrufePosition.z + index));
            strufes.Add(strufe);
        }
        return strufes;
    }
    private Strufe CreateStrufe(Vector3 at)
    {
        return _container.InstantiatePrefabForComponent<Strufe>(_strufe, at, Quaternion.identity, _strufeContainer.transform);
    }
}