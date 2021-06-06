using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a group of elements.</para>
    /// </summary>
    public interface IGameElementGroup : IEnumerable<IGameElement>
    {
        /// <summary>
        ///     <para>Returns elements in this group.</para>
        /// </summary>
        IEnumerable<IGameElement> GetElements();

        /// <summary>
        ///     <para>Returns element count in this group.</para>
        /// </summary>
        int GetElementCount();
    }

    public abstract class GameElementGroup : GameElement, IGameElementGroup
    {
        public abstract IEnumerator<IGameElement> GetEnumerator();

        public abstract IEnumerable<IGameElement> GetElements();

        public abstract int GetElementCount();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected sealed override void OnPrepareGame(object sender)
        {
            base.OnPrepareGame(sender);
            foreach (var element in this)
            {
                element.OnPrepareGame(sender);
            }

            this.OnPrepareGame(sender, this);
        }

        protected virtual void OnPrepareGame(object sender, GameElementGroup _)
        {
        }

        protected sealed override void OnReadyGame(object sender)
        {
            base.OnReadyGame(sender);
            foreach (var element in this)
            {
                element.OnReadyGame(sender);
            }

            this.OnReadyGame(sender, this);
        }

        protected virtual void OnReadyGame(object sender, GameElementGroup _)
        {
        }

        protected sealed override void OnStartGame(object sender)
        {
            base.OnStartGame(sender);
            foreach (var element in this)
            {
                element.OnStartGame(sender);
            }

            this.OnStartGame(sender, this);
        }

        protected virtual void OnStartGame(object sender, GameElementGroup _)
        {
        }

        protected sealed override void OnPauseGame(object sender)
        {
            base.OnPauseGame(sender);
            foreach (var element in this)
            {
                element.OnPauseGame(sender);
            }

            this.OnPauseGame(sender, this);
        }

        protected virtual void OnPauseGame(object sender, GameElementGroup _)
        {
        }

        protected sealed override void OnResumeGame(object sender)
        {
            base.OnResumeGame(sender);
            foreach (var element in this)
            {
                element.OnResumeGame(sender);
            }

            this.OnResumeGame(sender, this);
        }

        protected virtual void OnResumeGame(object sender, GameElementGroup _)
        {
        }

        protected sealed override void OnFinishGame(object sender)
        {
            base.OnFinishGame(sender);
            foreach (var element in this)
            {
                element.OnFinishGame(sender);
            }

            this.OnFinishGame(sender, this);
        }

        protected virtual void OnFinishGame(object sender, GameElementGroup _)
        {
        }

        protected sealed override void OnDestroyGame(object sender)
        {
            this.OnDestroyGame(sender, this);

            foreach (var element in this)
            {
                element.OnDestroyGame(sender);
            }

            base.OnDestroyGame(sender);
        }

        protected virtual void OnDestroyGame(object sender, GameElementGroup _)
        {
        }
    }
}
