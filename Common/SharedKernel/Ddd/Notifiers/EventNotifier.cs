﻿namespace SharedKernel.Ddd.Events;

public abstract class EventNotifier
{
    /// <summary>
    /// 提交事件
    /// </summary>
    public abstract void Raise<T>(T domainEvent) where T : DomainEvent;
}