using System;

namespace Gameknit
{
    /// <summary>
    ///     <para>A gameplay system with lifecycle.</para>
    /// </summary>
    public interface IGameContext
    {
        #region Event

        /// <summary>
        ///     <para>Invoke this event when game is prepared. <see cref="PrepareGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGamePreparedEvent;
        
        /// <summary>
        ///     <para>Invoke this event when game is prepared. <see cref="ReadyGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameReadyEvent;

        /// <summary>
        ///     <para>Invoke this event when game is prepared. <see cref="StartGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameStartedEvent;

        /// <summary>
        ///     <para>Invoke this event when game is prepared. <see cref="PauseGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGamePausedEvent;

        /// <summary>
        ///     <para>Invoke this event when game is prepared. <see cref="ResumeGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameResumedEvent;

        /// <summary>
        ///     <para>Invoke this event when game is prepared. <see cref="FinishGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameFinishedEvent;

        #endregion

        /// <summary>
        ///     <para>A status of game system lifecycle.
        ///     Change status when calling lifecycle methods.</para>
        /// </summary>
        GameStatus Status { get; }

        #region Lifecycle

        /// <summary>
        ///     <para>Configures game components in this game system.
        ///     Use this method to bind game components to each other.</para>
        /// </summary>
        /// <param name="sender"></param>
        void PrepareGame(object sender);

        /// <summary>
        ///     <para>Listens game components.</para>
        ///     Use this method to subscribe game components to each other.</para>
        /// </summary>
        /// <param name="sender"></param>
        void ReadyGame(object sender);

        /// <summary>
        ///     <para>Lauches this game system.
        ///     Use this method to activate game components.</para>
        /// </summary>
        /// <param name="sender"></param>
        void StartGame(object sender);

        /// <summary>
        ///     <para>Pauses this game system.
        ///     Use this method to pause game components.</para>
        /// </summary>
        /// <param name="sender"></param>
        void PauseGame(object sender);
        
        /// <summary>
        ///     <para>Resumes this game system.
        ///     Use this method to resume game components.</para>
        /// </summary>
        /// <param name="sender"></param>
        void ResumeGame(object sender);

        /// <summary>
        ///     <para>Finishes this game system.</para>
        ///     Use this method to stop game components.</para>
        /// </summary>
        /// <param name="sender"></param>
        void FinishGame(object sender);

        /// <summary>
        ///     <para>Destroys this game system.</para>
        ///     Use this method to destroy game components.</para>
        /// </summary>
        /// <param name="sender"></param>
        void DestroyGame(object sender);

        #endregion
    }
}