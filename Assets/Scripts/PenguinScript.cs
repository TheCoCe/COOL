using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_dialogStartClip;
    [SerializeField]
    private AudioClip m_dialogCloseClip;

    [SerializeField]
    private AudioSource m_soundSource;
    private bool m_dialogOpened = false;

    [SerializeField]
    private Canvas m_dialogCanvas;

    public void Start()
    {
        m_dialogCanvas.enabled = false;
    }

    private void OpenDialog()
    {
        if(m_soundSource)
        {
            m_soundSource.clip = m_dialogStartClip;
            m_soundSource.Play();
        }
        m_dialogOpened = true;
        m_dialogCanvas.enabled = true;
    }

    public void CloseDialog()
    {
        if(m_soundSource)
        {
            m_soundSource.clip = m_dialogCloseClip;
            m_soundSource.Play();
        }
        m_dialogOpened = false;
        m_dialogCanvas.enabled = false;

        Debug.Log("Conversation over.");
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on penguin!");
        if (!m_dialogOpened)
        {
            OpenDialog();
            Debug.Log("Entered conversation with penguin.");
        }
    }
}
