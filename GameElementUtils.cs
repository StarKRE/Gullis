using System;
using System.Collections.Generic;

namespace GameElements
{
    public static class GameElementUtils
    {
        public static void SyncState(IGameElement element, GameState targetState, object sender)
        {
            if (targetState >= GameState.FINISH)
            {
                return;
            }
            
            if (targetState < GameState.PREPARE)
            {
                return;
            }

            element.OnPrepareGame(sender);

            if (targetState >= GameState.READY)
            {
                element.OnReadyGame(sender);
            }

            if (targetState >= GameState.PLAY)
            {
                element.OnStartGame(sender);
            }

            if (targetState == GameState.PAUSE)
            {
                element.OnPauseGame(sender);
            }
        }
        
        public static T Find<R, T>(Dictionary<Type, T> map)
        {
            return Find(map, typeof(R));
        }

        public static T Find<T>(Dictionary<Type, T> map, Type requiredType)
        {
            if (map.ContainsKey(requiredType))
            {
                return map[requiredType];
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return map[key];
                }
            }

            throw new Exception("Value is not found!");
        }

        public static bool TryFind<T>(Dictionary<Type, T> map, Type requiredType, out T item)
        {
            if (map.ContainsKey(requiredType))
            {
                item = map[requiredType];
                return true;
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    item = map[key];
                    return true;
                }
            }

            item = default(T);
            return false;
        }
    }
}