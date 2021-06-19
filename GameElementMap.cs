using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a dictionary of elements.</para>
    /// </summary>
    public interface IGameElementMap<in K, in V> : IGameElementGroup where V : IGameElement
    {
        /// <summary>
        ///     <para>Adds an element into the dictionary.</para>
        /// </summary>
        bool RegisterElement(K key, V element);

        /// <summary>
        ///     <para>Removes an element from the dictionary.</para>
        /// </summary>
        bool UnregisterElement(K key);

        /// <summary>
        ///     <para>Returns an element.</para>
        /// </summary>
        T GetElement<T>(K key);

        /// <summary>
        ///     <para>Tries to get an element from the dictionary.</para>
        /// </summary>
        bool TryGetElement<T>(K key, out T element);
    }

    public abstract class GameElementMap<K, V> : GameElementGroup, IGameElementMap<K, V> where V : IGameElement
    {
        private readonly Dictionary<K, V> registeredElementMap;

        protected GameElementMap()
        {
            this.registeredElementMap = new Dictionary<K, V>();
        }
        
        public bool RegisterElement(K key, V element)
        {
            if (this.registeredElementMap.ContainsKey(key))
            {
                return false;
            }

            this.registeredElementMap.Add(key, element);
            element.OnRegistered(this, this.GameSystem);
            GameElementUtils.SyncState(element, this.State, this);
            return true;
        }

        public bool UnregisterElement(K key)
        {
            if (!this.registeredElementMap.ContainsKey(key))
            {
                return false;
            }

            var element = this.registeredElementMap[key];
            element.OnUnregistered();
            this.registeredElementMap.Remove(key);
            return true;
        }

        public T GetElement<T>(K key)
        {
            object registeredElement = this.registeredElementMap[key];
            return (T) registeredElement;
        }

        public bool TryGetElement<T>(K key, out T element)
        {
            V result;
            if (this.registeredElementMap.TryGetValue(key, out result))
            {
                element = (T) (object) result;
                return true;
            }

            element = default(T);
            return false;
        }

        public override IEnumerable<IGameElement> GetElements()
        {
            return this.registeredElementMap.Values.OfType<IGameElement>();
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
