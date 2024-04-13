using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is util script by SimoGecko from Unity forum (https://forum.unity.com/threads/tip-invoke-any-function-with-delay-also-with-parameters.978273/)
/// Thanks a lot!
/// </summary>

namespace utils
{
    public static class CustomInvoke
    {
        public static void Invoke(this MonoBehaviour mb, Action f, float delay)
        {
            mb.StartCoroutine(InvokeRoutine(f, delay));
        }

        private static IEnumerator InvokeRoutine(System.Action f, float delay)
        {
            yield return new WaitForSeconds(delay);
            f();
        }
    }

}

