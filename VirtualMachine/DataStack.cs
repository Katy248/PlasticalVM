using System.Collections;

namespace VirtualMachine;

public class DataStack<T> : IEnumerable<T>, IEnumerable where T : class
{
    public DataStack(T standartObject, int capacity = 10)
    {
        this.StandartObject = standartObject;
        Stack = new List<T>(capacity);
    }
    private List<T>? _stack = null;
    private List<T> Stack
    {
        get
        {
            if (_stack == null) return new List<T>() { StandartObject };
            if (_stack.Count == 0) _stack.Add(StandartObject);
            return _stack;
        }
        set
        {
            if (_stack == null) _stack = value;
        }
    }
    public readonly T StandartObject;
    public void Push(T obj, int index = 0)
    {
        Stack.Insert(index, obj);
    }
    public T? Pop(int index = 0)
    {
        if (index < 0 || index >= Stack.Count) return null;
        T obj = Stack[index];
        Stack.RemoveAt(index);
        return obj;
    }
    public T? Peek(int index = 0)
    {
        if (index < 0 || index >= Stack.Count) return null;
        return Stack[index];
    }
    public bool TryPeek(out object? peekedValue)
    {
        peekedValue = null;
        if (Stack.Count == 0) return false;

        peekedValue = Stack[0];
        return true;
    }
    public void PushToTop(int index)
    {
        if (index < 0 || index >= Stack.Count) return;
        T obj = Stack[index];
        Stack.RemoveAt(index);
        Stack.Insert(0, obj);
    }
    public void PeekToTop(int index)
    {
        if (index < 0 || index >= Stack.Count) return;
        T obj = Stack[index];
        Stack.Insert(0, obj);
    }

    IEnumerator IEnumerable.GetEnumerator() => Stack.GetEnumerator();
    public IEnumerator<T> GetEnumerator() => Stack.GetEnumerator();
}