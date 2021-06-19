using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a set of elements.</para>
    /// </summary>
    public interface IGameElementSet : IGameElementGroup
    {
        /// <summary>
        ///     <para>Adds an element into the set.</para>
        /// </summary>
        bool RegisterElement(IGameElement element);

        /// <summary>
        ///     <para>Removes an element into the set.</para>
        /// </summary>
        bool UnregisterElement(IGameElement element);

        /// <summary>
        ///     <para>Checks an element into the set.</para>
        /// </summary>
        bool ContainsElement(IGameElement element);
    }

    public class GameElementSet : GameElementGroup, IGameElementSet
    {
        private readonly HashSet<IGameElement> registeredElements;

        protected GameElementSet()
        {
            this.registeredElements = new HashSet<IGameElement>();
        }

        public virtual bool RegisterElement(IGameElement element)
        {
            if (!this.registeredElements.Add(element))
            {
                return false;
            }

            element.OnRegistered(this, this.GameSystem);
            GameElementUtils.SyncState(element, this.State, this);
            return true;
        }

        public virtual bool UnregisterElement(IGameElement element)
        {
            if (this.registeredElements.Remove(element))
            {
                element.OnUnregistered();
                return true;
            }

            return false;
        }
        
        public bool ContainsElement(IGameElement element)
        {
            return this.registeredElements.Contains(element);
        }

        public override IEnumerable<IGameElement> GetElements()
        {
            return new HashSet<IGameElement>(this.registeredElements);
        }

        public override int GetElementCount()
        {
            return this.registeredElements.Count;
        }

        public override IEnumerator<IGameElement> GetEnumerator()
        {
            foreach (var element in this.registeredElements)
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
