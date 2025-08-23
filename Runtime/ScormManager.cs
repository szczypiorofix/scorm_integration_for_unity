using UnityEngine;
using System.Runtime.InteropServices;

namespace ScormManager.ScormAPI
{
    public static class ScormPlayer
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void ScormInitialize();

        [DllImport("__Internal")]
        private static extern void ScormSetValue(string element, string value);

        [DllImport("__Internal")]
        private static extern void ScormCommit();

        [DllImport("__Internal")]
        private static extern void ScormFinish();
#endif

        public static void Initialize()
        {
            Debug.Log("ScormManager: Initializing SCORM...");
#if UNITY_WEBGL && !UNITY_EDITOR
            ScormInitialize();
#endif
        }

        public static void SetScore(int score, int min = 0, int max = 100)
        {
            string scoreString = score.ToString();
            Debug.Log($"ScormManager: Setting score: {scoreString}");
#if UNITY_WEBGL && !UNITY_EDITOR
            ScormSetValue("cmi.core.score.raw", scoreString);
            ScormSetValue("cmi.core.score.min", min.ToString());
            ScormSetValue("cmi.core.score.max", max.ToString());
#endif
        }

        public static void SetTime(string time)
        {
            if (time == null || time.Length == 0)
            {
                return;
            }
            Debug.Log($"ScormManager: Set session time:: {time}");
#if UNITY_WEBGL && !UNITY_EDITOR
            ScormSetValue("cmi.core.session_time", time);
#endif
        }

        public static void MarkAsComplete(bool passed)
        {
            Debug.Log($"ScormManager: Marking as complete, passed: {passed}");
            if (passed)
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                ScormSetValue("cmi.core.lesson_status", "passed");
#endif
            }
            else
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                ScormSetValue("cmi.core.lesson_status", "failed");
#endif
            }
#if UNITY_WEBGL && !UNITY_EDITOR
            ScormCommit();
            ScormFinish();
#endif
        }

        public static void Finish()
        {
            Debug.Log("ScormManager: Finishing SCORM session...");
#if UNITY_WEBGL && !UNITY_EDITOR
            ScormFinish();
#endif
        }
    }
}

