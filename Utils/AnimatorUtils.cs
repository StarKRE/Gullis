using UnityEngine;

namespace Gameknit
{
    public static class AnimatorUtils
    {
        public static void PlayByDefault(this Animator animator, string animationName)
        {
            var hash = Animator.StringToHash(animationName);
            animator.PlayByDefault(hash);
        }

        public static void PlayByDefault(this Animator animator, int hash)
        {
            const int layer = -1;
            const int time = 0;
            animator.Play(hash, layer, time);
        }
    }
}