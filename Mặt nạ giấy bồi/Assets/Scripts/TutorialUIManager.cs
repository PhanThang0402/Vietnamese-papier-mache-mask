using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TutorialUIManager : MonoBehaviour
{
    public GameObject Intro_1;
    public GameObject Intro_2;
    public GameObject Intro_3;
    public GameObject Intro_4;

    public AudioSource audioSource;

    // 5 AudioClip cho tung intro
    public AudioClip intro1Clip;
    public AudioClip intro2Clip;
    public AudioClip intro3Clip;
    public AudioClip intro4Clip;

    private Coroutine currentVoiceCoroutine;

    public GameObject soundToggleButton;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public UnityEngine.UI.Image soundToggleImage;
    private AudioClip pendingClip = null;
    private System.Action pendingCallback = null;
    private bool voiceStarted = false;


    private bool isPaused = false;
    public void ToggleSound()
    {
        if (!audioSource.isPlaying && isPaused)
        {
            isPaused = false;
            UpdateSoundIcon();

            if (!voiceStarted && pendingClip != null)
            {
                //  Neu ch tung phat doan nay, phat tu dau
                audioSource.clip = pendingClip;
                audioSource.Play();
                voiceStarted = true;
            }
            else
            {
                audioSource.UnPause();
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isPaused = true;
            UpdateSoundIcon();
        }
    }

    private void UpdateSoundIcon()
    {
        if (soundToggleImage == null) return;

        soundToggleImage.sprite = isPaused ? soundOffIcon : soundOnIcon;
    }

    private void ShowSoundButton(bool show)
    {
        if (soundToggleButton != null)
            soundToggleButton.SetActive(show);
    }

    private void PlayVoice(AudioClip clip, System.Action onFinished)
    {
        if (clip == null)
        {
            Debug.LogWarning("Voice clip is missing!");
            return;
        }

        StopVoice(); // Ngung doan truoc neu con

        pendingClip = clip;
        pendingCallback = onFinished;
        voiceStarted = false;

        audioSource.clip = clip;

        if (!isPaused)
        {
            audioSource.Play();
            voiceStarted = true;
        }

        UpdateSoundIcon();

        currentVoiceCoroutine = StartCoroutine(WaitForVoiceOver());
    }


    private IEnumerator WaitForVoiceOver()
    {
        // Ngung doan truoc neu con
        while (!voiceStarted)
        {
            yield return null;
        }

        // Doi phat xong hoac bi pause
        while (audioSource.isPlaying || isPaused)
        {
            yield return null;
        }

        // reset cac bien, next intro
        voiceStarted = false;
        currentVoiceCoroutine = null;

        pendingCallback?.Invoke();
    }



    private void StopVoice()
    {
        if (currentVoiceCoroutine != null)
        {
            StopCoroutine(currentVoiceCoroutine);
            currentVoiceCoroutine = null;
        }

        audioSource.Stop();
        voiceStarted = false;
        pendingClip = null;
        pendingCallback = null;
    }



    public void GoToMenuScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save();
        SceneManager.LoadScene("All"); // 
    }
    void Start()
    {
        GoToIntro1();
    }

    public void GoToIntro1()
    {
        DisableAll();
        Intro_1.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro1Clip, GoToIntro2);
    }

    public void GoToIntro2()
    {
        DisableAll();
        Intro_2.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro2Clip, GoToIntro3);
    }

    public void GoToIntro3()
    {
        DisableAll();
        Intro_3.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro3Clip, GoToIntro4);
    }
    public void GoToIntro4()
    {
        DisableAll();
        Intro_4.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro4Clip, GoToMenuScene);
    }
    private void DisableAll()
    {
        // 
        Intro_1.SetActive(false);
        Intro_2.SetActive(false);
        Intro_3.SetActive(false);
        Intro_4.SetActive(false);
    }
}

