using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Sample : MonoBehaviour
{
    List<string> m_listVoices = new List<string>();

    private void Start()
    {
        Task task = Task.Run(() =>
        {
            return OpenJTalk.SpeakStoppable("‚±‚ñ‚É‚¿‚Í");
        });
    }
}
