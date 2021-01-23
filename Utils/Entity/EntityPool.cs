using System.Collections.Generic;
using Gameknit;

namespace Gameknit
{
    public abstract class EntityPool<T> : EntityManager<T>
    {
        protected List<T> AvailableEntities { get; }

        #region Lifecycle

        protected EntityPool()
        {
            this.AvailableEntities = new List<T>();
        }

        protected sealed override void OnRegisterEntity(T entity)
        {
            this.AvailableEntities.Add(entity);
            this.OnRegisterEntity(this, entity);
        }

        protected virtual void OnRegisterEntity(EntityPool<T> _, T entity)
        {
        }

        protected sealed override void OnUnregisterEntity(T entity)
        {
            this.AvailableEntities.Remove(entity);
            this.OnUnregisterEntity(this, entity);
        }

        protected virtual void OnUnregisterEntity(EntityPool<T> _, T entity)
        {
        }

        #endregion

        public void PushEntity(T entity)
        {
            this.AvailableEntities.Add(entity);
        }

        public T PopEntity()
        {
            var next = this.PeekEntity();
            this.AvailableEntities.Remove(next);
            return next;
        }

        public T PeekEntity()
        {
            return this.AvailableEntities[0];
        }

        public int AvailableEntityCount()
        {
            return this.AvailableEntities.Count;
        }
    }
}