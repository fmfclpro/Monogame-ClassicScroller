using System;
using System.Collections.Generic;
using ClassicScroller.Core.Interfaces;
using ClassicScroller.Core.Math;

/*
 MIT License
Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

namespace ClassicScroller.Core.Composite;

public sealed class CompositeObject
{
    private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

    private List<IUpdatable> _updates;
    public Transform Transform { get; }

    public CompositeObject()
    {
        Transform = new Transform();
        _updates = new List<IUpdatable>();
    }

    /// <summary>
    /// Adds a component. If the value is null, it creates a new one and returns it.
    /// </summary>
    public T AddComponent<T>(T component = null) where T : Component, new()
    {
        if (HasComponent<T>()) return default;

        T newComponent = component;
        if (newComponent == null) newComponent = AddComponent<T>();
        return newComponent;
    }

    /// <summary>
    /// Adds a component.
    /// </summary>
    public T AddComponent<T>() where T : Component, new()
    {
        if (TryGetComponent(out T comp)) return comp;

        T newComponent = new T();

        _components[typeof(T)] = newComponent;

        newComponent.SetAttached(this);

        if (newComponent is IUpdatable update)
        {
            _updates.Add(update);
        }

        return newComponent;
    }

    public bool HasComponent<T>()
    {
        return _components.ContainsKey(typeof(T));
    }

    public bool TryGetComponent<T>(out T component) where T : Component
    {
        bool exists = _components.TryGetValue(typeof(T), out Component c);
        component = c as T;
        return exists;
    }

    public T GetComponent<T>() where T : Component
    {
        return !TryGetComponent(out T comp) ? null : comp;
    }

    public void Update(World world, float deltaTime)
    {
        foreach (var toUpdate in _updates)
        {
            toUpdate.Update(world, deltaTime);
        }
    }

    public void PreStart(World world)
    {
        foreach (var toUpdate in _updates)
        {
            toUpdate.PreStart(world);
        }
    }

    public void Start(World world)
    {
        foreach (var toUpdate in _updates)
        {
            toUpdate.Start(world);
        }
    }
}