using System.Collections;

namespace VirtualMachine;

public class Stack<T> : IEnumerable<T>, IEnumerable where T : class
{
    public Stack(T standartObject, int capacity = 10)
    {
        this.StandartObject = standartObject;
        _stack = new List<T>(capacity);

    }
    private readonly List<T> _stack;
    private List<T> stack
    {
        get
        {
            if (_stack.Count == 0) _stack.Add(StandartObject);
            return _stack;
        }
    }
    public readonly T StandartObject;
    public void Push(T obj, int index = 0)
    {
        stack.Insert(index, obj);
    }
    public T? Pop(int index = 0)
    {
        if (index < 0 || index >= stack.Count) return null;
        T obj = stack[index];
        stack.RemoveAt(index);
        return obj;
    }
    public T? Peek(int index = 0)
    {
        if (index < 0 || index >= stack.Count) return null;
        return stack[index];
    }
    public bool TryPeek(out object? peekedValue)
    {
        try
        {
            peekedValue = Peek();
            return true;
        }
        catch (Exception)
        {
            peekedValue = default;
            return false;
        }
    }
    public void PushToTop(int index)
    {
        if (index < 0 || index >= stack.Count) return;
        T obj = stack[index];
        stack.RemoveAt(index);
        stack.Insert(0, obj);
    }
    public void Dup(int index)
    {
        if (index < 0 || index >= stack.Count) return;
        T obj = stack[index];
        stack.Insert(0, obj);
    }

    IEnumerator IEnumerable.GetEnumerator() => stack.GetEnumerator();
    public IEnumerator<T> GetEnumerator() => stack.GetEnumerator();
}