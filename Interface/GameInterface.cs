using UnityEngine;

namespace Gameknit
{
    /// <summary>
    ///     <para>Provides gameplay interface with player.
    ///     Each interface is responsible for single interaction.</para>
    /// </summary>
    public interface IGameInterface
    {
        /// <summary>
        ///     <para>Called when this interface is registered into system.</para>
        /// </summary>
        /// <param name="system">interface system.</param>
        void OnRegistered(IGameInterfaceSystem system);

        /// <summary>
        ///     <para>Calls when a game system prepares.
        ///     You can get game nodes from game context.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGamePrepared(object sender);

        /// <summary>
        ///     <para>Calls when a game system readies.
        ///     You can subscribe on game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameReady(object sender);

        /// <summary>
        ///     <para>Calls when game system launches.
        ///     You can start this interface.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameStarted(object sender);

        /// <summary>
        ///     <para>Calls when game system pauses.
        ///     You can pause this interface.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGamePaused(object sender);

        /// <summary>
        ///     <para>Calls when game system resumes.
        ///     You can resume this interface.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameResumed(object sender);

        /// <summary>
        ///     <para>Calls when game system finishes.</para>
        ///     You can stop this interface and unsubscribe from other game nodes.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnGameFinished(object sender);

        /// <summary>
        ///     <para>Called when this interface is unregistered from system.</para>
        /// </summary>
        void OnUnregistered();
    }

    /// <inheritdoc cref="IGameInterface"/>
    public abstract class GameInterface : MonoBehaviour, IGameInterface
    {
        /// <summary>
        ///     <para>A parent interface system reference.</para>
        /// </summary>
        protected IGameInterfaceSystem InterfaceSystem { get; private set; }

        #region Lifecycle

        /// <inheritdoc cref="IGameInterface.OnRegistered"/>
        void IGameInterface.OnRegistered(IGameInterfaceSystem system)
        {
            this.InterfaceSystem = system;
            this.OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGamePrepared"/>
        void IGameInterface.OnGamePrepared(object sender)
        {
            this.OnGamePrepared(sender);
        }

        protected virtual void OnGamePrepared(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameReady"/>
        void IGameInterface.OnGameReady(object sender)
        {
            this.OnGameReady(sender);
        }

        protected virtual void OnGameReady(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameStarted"/>
        void IGameInterface.OnGameStarted(object sender)
        {
            this.OnGameStarted(sender);
        }

        protected virtual void OnGameStarted(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGamePaused"/>
        void IGameInterface.OnGamePaused(object sender)
        {
            this.OnGamePaused(sender);
        }

        protected virtual void OnGamePaused(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameResumed"/>
        void IGameInterface.OnGameResumed(object sender)
        {
            this.OnGameResumed(sender);
        }

        protected virtual void OnGameResumed(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameFinished"/>
        void IGameInterface.OnGameFinished(object sender)
        {
            this.OnGameFinished(sender);
        }

        protected virtual void OnGameFinished(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnUnregistered"/>
        void IGameInterface.OnUnregistered()
        {
            this.OnUnregistered();
        }

        protected virtual void OnUnregistered()
        {
        }

        #endregion

        /// <summary>
        ///     <para>Gets game context reference through interface system.</para>
        /// </summary>
        protected T GetGameContext<T>() where T : IGameContext
        {
            return this.InterfaceSystem.ProvideGameContext<T>();
        }

        /// <summary>
        ///     <para>Gets parent interface system.</para>
        /// </summary>
        protected T GetInterfaceSystem<T>() where T : IGameInterfaceSystem
        {
            return (T) this.InterfaceSystem;
        }
    }
}