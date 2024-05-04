using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace SharedKernel.Ddd.Notification;

/// <summary>
/// 监测列表
/// </summary>
[DebuggerStepThrough]
public class NotificationList<T, TAggregateRoot> : ICollection<T>
{
    /// <summary>
    /// 聚合根
    /// </summary>
    private readonly TAggregateRoot _aggregateRoot;

    /// <summary>
    /// 内部实现
    /// </summary>
    private readonly List<T> _innerImpl = [];

    public int Count => _innerImpl.Count;
    public bool IsReadOnly => false;

    public NotificationList(TAggregateRoot aggregateRoot)
    {
        _aggregateRoot = aggregateRoot;
    }

    public IEnumerator<T> GetEnumerator() => _innerImpl.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var notifier = new DefaultEventNotifier<TAggregateRoot>(_aggregateRoot);
        
        var fieldInfo = item.GetType().GetField("_notifier", BindingFlags.Instance | BindingFlags.NonPublic)!;
        fieldInfo.SetValue(item, notifier);
        
        _innerImpl.Add(item);
    }

    public void Clear() => _innerImpl.Clear();

    public bool Contains(T item) => _innerImpl.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _innerImpl.CopyTo(array, arrayIndex);

    public bool Remove(T item) => _innerImpl.Remove(item);
}