using System;
using UnityEngine;
using Zenject;

public class StrufeControl : MonoBehaviour
{
    public event Action OnAllStrufesMoved;
    private IStrufeFactory _strufeFactory;
    [Inject]
    public void Construct(IStrufeFactory strufeFactory, Player player)
    {
        _strufeFactory = strufeFactory;
        player.OnMoving += MoveStrufes;
    }

    public void MoveStrufes(float speed)
    {
        for (int index = 0; index < _strufeFactory.Strufes.Count; index++)
        {
            _strufeFactory.Strufes[index].Move(speed);
        }
        OnAllStrufesMoved?.Invoke();
    }
}
