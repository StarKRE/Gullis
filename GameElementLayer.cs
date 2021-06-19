using System;
using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a dictionary of generic elements.</para>
    /// </summary>
    public interface IGameElementLayer : IGameElementGroup
    {
        /// <summary>
        ///     <para>Adds an element into the layer.</para>
        /// </summary>
        bool RegisterElement(IGameElement element);

        /// <summary>
        ///     <para>Removes an element from the layer.</para>
        /// </summary>
        bool UnregisterElement(IGameElement element);

        /// <summary>
        ///     <para>Returns an element of "T".</para>
        /// </summary>
        T GetElement<T>();

        /// <summary>
        ///     <para>Tries to get an element of "T".</para>
        /// </summary>
        bool TryGetElement<T>(out T element);
    }

    public class GameElementLayer : GameElementGroup, IGameElementLayer
    {
        private readonly Dictionary<Type, IGameElement> registeredElementMap;

        protected GameElementLayer()
        {
            this.registeredElementMap = new Dictionary<Type, IGameElement>();
        }

        public bool RegisterElement(IGameElement element)
        {
            var type = element.GetType();
            if (this.registeredElementMap.ContainsKey(type))
            {
                return false;
            }

            this.registeredElementMap.Add(type, element);
            element.OnRegistered(this, this.GameSystem);
            GameElementUtils.SyncState(element, this.State, this);
            return true;
        }
        
        public bool UnregisterElement(IGameElement element)
        {
            var type = element.GetType();
            if (!this.registeredElementMap.Remove(type))
            {
                return false;
            }

            element.OnUnregistered();
            return true;
        }

        public T GetElement<T>()
        {
            return (T) GameElementUtils.Find<T, IGameElement>(this.registeredElementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            IGameElement result;
            if (GameElementUtils.TryFind(this.registeredElementMap, typeof(T), out result))
            {
                element = (T) result;
                return true;
            }

            element = default(T);
            return false;
        }

        public override IEnumerable<IGameElement> GetElements()
        {
            return this.registeredElementMap.Values;
        }

        public override int GetElementCount()
        {
            return this.registeredElementMap.Count;
        }

        public override IEnumerator<IGameElement> GetEnumerator()
        {
            foreach (var element in this.registeredElementMap.Values)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
