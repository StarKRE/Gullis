using UnityEngine;

namespace Gameknit
{
    /// <summary>
    ///     <para>A game component of game system <see cref="IGameContext"/>.</para>
    /// </summary>
    public interface IGameNode
    {
        /// <summary>
        ///     <para>Called when this game component is registered into system.</para>
        /// </summary>
        /// <param name="parent">Who registers this node.</param>
        /// <param name="context">Game system.</param>
        void OnRegistered(object parent, IGameNodeContext context);
        
        /// <summary>
        ///     <para>Calls when a game system prepares.
        ///     You can get other game nodes from game context.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnPrepareGame(object sender);

        /// <summary>
        ///     <para>Calls when a game system readies.
        ///     You can subscribe on other game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnReadyGame(object sender);

        /// <summary>
        ///     <para>Calls when game system launches.
        ///     You can start this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnStartGame(object sender);

        /// <summary>
        ///     <para>Calls when game system pauses.
        ///     You can pause this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnPauseGame(object sender);

        /// <summary>
        ///     <para>Calls when game system resumes</para>
        ///     You can resume this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnResumeGame(object sender);

        /// <summary>
        ///     <para>Calls when game system finishes</para>
        ///     You can stop this game node and unsubscribe from other game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnFinishGame(object sender);
        
        /// <summary>
        ///     <para>Calls when game system destroys.</para>
        ///     You can dispose resources of this game node.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnDestroyGame(object sender);

        /// <summary>
        ///     <para>Called when this game component is unregistered from system.</para>
        /// </summary>
        void OnUnregistered();
    }
    
    /// <inheritdoc cref="IGameNode"/>
    public abstract class GameNode : MonoBehaviour, IGameNode
    {
        /// <summary>
        ///     <para>A game system reference.</para>
        /// </summary>
        protected IGameNodeContext GameContext { get; private set; }

        /// <summary>
        ///     <para>A parent registrar reference. Usually registrar is a parent node.</para>
        /// </summary>
        protected object Parent { get; private set; }

        #region Lifecycle

        /// <inheritdoc cref="IGameNode.OnRegistered"/>
        void IGameNode.OnRegistered(object parent, IGameNodeContext context)
        {
            this.GameContext = context;
            this.Parent = parent;
            this.OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }

        /// <inheritdoc cref="IGameNode.OnPrepareGame"/>
        void IGameNode.OnPrepareGame(object sender)
        {
            this.OnPrepareGame(sender);
        }

        protected virtual void OnPrepareGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnReadyGame"/>
        void IGameNode.OnReadyGame(object sender)
        {
            this.OnReadyGame(sender);
        }

        protected virtual void OnReadyGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnStartGame"/>
        void IGameNode.OnStartGame(object sender)
        {
            this.OnStartGame(sender);
        }

        protected virtual void OnStartGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnPauseGame"/>
        void IGameNode.OnPauseGame(object sender)
        {
            this.OnPauseGame(sender);
        }

        protected virtual void OnPauseGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnResumeGame"/>
        void IGameNode.OnResumeGame(object sender)
        {
            this.OnResumeGame(sender);
        }

        protected virtual void OnResumeGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnFinishGame"/>
        void IGameNode.OnFinishGame(object sender)
        {
            this.OnFinishGame(sender);
        }

        protected virtual void OnFinishGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnDestroyGame"/>
        void IGameNode.OnDestroyGame(object sender)
        {
            this.OnDestroyGame(sender);
        }

        protected virtual void OnDestroyGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnDestroyGame"/>
        void IGameNode.OnUnregistered()
        {
            this.OnUnregistered();
        }

        protected virtual void OnUnregistered()
        {
        }

        #endregion

        /// <summary>
        ///     <para>Gets game context reference.</para>
        /// </summary>
        protected T GetGameContext<T>() where T : IGameNodeContext
        {
            return (T) this.GameContext;
        }

        protected T GetParent<T>()
        {
            return (T) this.Parent;
        }
    }
}