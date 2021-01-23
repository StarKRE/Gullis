using System;
using System.Collections.Generic;
using System.Linq;
using GameNode;

namespace Gameknit
{
    /// <summary>
    ///     <para>Contains unique game nodes <see cref="IGameNode"/>.</para>
    /// </summary>
    public interface IGameNodeLayer
    {
        /// <summary>
        ///     <para>Gets a registered unique node by type.</para>
        /// </summary>
        T GetNode<T>() where T : IGameNode;

        /// <summary>
        ///     <para>Try Gets a registered unique node by type.</para>
        /// </summary>
        bool TryGetNode<T>(out T node) where T : IGameNode;

        /// <summary>
        ///     <para>Gets registered unique nodes by base type.</para>
        /// </summary>
        /// <typeparam name="T">Base node type.</typeparam>
        /// <returns>Group of nodes.</returns>
        IEnumerable<T> GetNodes<T>() where T : IGameNode;
    }
    
    /// <inheritdoc cref="IGameNodeLayer"/>
    public abstract class GameNodeLayer : GameNodeContainer, IGameNodeLayer
    {
        /// <summary>
        ///     <para>Map of unique registered game components.
        ///     Key: a node type, Value: a node instance.</para>
        /// </summary>
        private readonly Dictionary<Type, IGameNode> registeredNodeMap;
        
        protected GameNodeLayer()
        {
            this.registeredNodeMap = new Dictionary<Type, IGameNode>();
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNode{T}"/>
        public T GetNode<T>() where T : IGameNode
        {
            return DictionaryHelper.Find<T, IGameNode>(this.registeredNodeMap);
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNodes{T}"/>
        public IEnumerable<T> GetNodes<T>() where T : IGameNode
        {
            return this.registeredNodeMap.Values.OfType<T>();
        }
        
        /// <inheritdoc cref="IGameNodeLayer.TryGetNode{T}"/>
        public bool TryGetNode<T>(out T node) where T : IGameNode
        {
            var requiredType = typeof(T);
            if (DictionaryHelper.TryFind(this.registeredNodeMap, requiredType, out var result))
            {
                node = (T) result;
                return true;
            }

            node = default;
            return false;
        }

        public override void RegisterNode(IGameNode gameNode)
        {
            base.RegisterNode(gameNode);
            var type = gameNode.GetType();
            this.registeredNodeMap.Add(type, gameNode);
        }

        public override void UnregisterNode(IGameNode gameNode)
        {
            var type = gameNode.GetType();
            this.registeredNodeMap.Remove(type);
            base.UnregisterNode(gameNode);
        }
    }
}