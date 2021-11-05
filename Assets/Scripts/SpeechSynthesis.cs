using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public class SpeechSynthesis : MonoBehaviour
{
    [DllImport("WindowsVoice")]
    public static extern void initSpeech();
    [DllImport("WindowsVoice")]
    public static extern void destroySpeech();
    [DllImport("WindowsVoice")]
    public static extern void addToSpeechQueue(string s);
    [DllImport("WindowsVoice")]
    public static extern void clearSpeechQueue();
    [DllImport("WindowsVoice")]
    public static extern void statusMessage(StringBuilder str, int length);
    public static SpeechSynthesis m_SpeechSynthesis = null;
    
    void OnEnable()
    {
        if (m_SpeechSynthesis == null)
        {
            m_SpeechSynthesis = this;
            initSpeech();
        }
    }

    public static void Speak(string msg, float delay = 0.0f)
    {

        if (delay == 0f)
            addToSpeechQueue(msg);
        else
            m_SpeechSynthesis.ExecuteLater(delay, () => Speak(msg));
    }

    void OnDestroy()
    {
        if (m_SpeechSynthesis == this)
        {
            Debug.Log("Destroying speech");
            destroySpeech();
            Debug.Log("Speech destroyed");
            m_SpeechSynthesis = null;
        }
    }

    public static string GetStatusMessage()
    {
        StringBuilder sb = new StringBuilder(40);
        statusMessage(sb, 40);
        return sb.ToString();
    }
}

public static class Utility
{
    public static Coroutine ExecuteLater(this MonoBehaviour behaviour, float delay, System.Action fn)
    {
        return behaviour.StartCoroutine(_realExecute(delay, fn));
    }
    static IEnumerator _realExecute(float delay, System.Action fn)
    {
        yield return new WaitForSeconds(delay);
        fn();
    }
}
