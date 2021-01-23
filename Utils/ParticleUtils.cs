using System.Collections;
using UnityEngine;

namespace Gameknit
{
    public static class ParticleUtils
    {
        public static IEnumerator PlayOneShot(this ParticleSystem particleSystem)
        {
            particleSystem.Play();
            yield return new WaitForSeconds(particleSystem.main.duration);
            particleSystem.Stop();
        }
    }
}