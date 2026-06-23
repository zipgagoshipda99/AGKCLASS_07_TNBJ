using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{

    private List<IObserver> _observers = new List<IObserver>();
    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }
    protected void NotifyObservers(GameResult gameResult)
    {// item 은 _observers에서 꺼낸 상자를 item이라고 명시한거.
        // _observers.ForEach((observers)=>{//_observers 안에 들어있는 상자들 처음부터 끝까지 한개씩 꺼내서 다음작업을 하라는 뜻
        // observers.OnNotify(); 
        // });
        foreach(IObserver observers in _observers)
        {
            observers.OnNotify(gameResult);
        }
    }
}
