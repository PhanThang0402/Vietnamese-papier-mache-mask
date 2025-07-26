using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Welcome;
    public GameObject Intro_1;
    public GameObject Intro_2;
    public GameObject Intro_3;
    public GameObject Intro_4;
    public GameObject Intro_5;
    public GameObject ChooseMask;

    public GameObject Thino_intro;
    public GameObject Thino_detail;
    public GameObject Chipheo_intro;
    public GameObject Chipheo_detail;
    public GameObject Wukong_intro;
    public GameObject Wukong_detail;

    public AudioSource audioSource;

    // 5 AudioClip cho tung intro
    public AudioClip intro1Clip;
    public AudioClip intro2Clip;
    public AudioClip intro3Clip;
    public AudioClip intro4Clip;
    public AudioClip intro5Clip;

    private Coroutine currentVoiceCoroutine;

    public GameObject soundToggleButton;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public UnityEngine.UI.Image soundToggleImage;
    private AudioClip pendingClip = null;
    private System.Action pendingCallback = null;
    private bool voiceStarted = false;


    private bool isPaused = false;

    void Start()
    {
        string returnTo = PlayerPrefs.GetString("ReturnTo", "");

        if (returnTo == "ChooseMask")
        {
            PlayerPrefs.DeleteKey("ReturnTo");
            GoToChooseMask();
        }
        else
        {
            ShowWelcome();
        }
    }

    public void ToggleSound()
    {
        if (!audioSource.isPlaying && isPaused)
        {
            isPaused = false;
            UpdateSoundIcon();

            if (!voiceStarted && pendingClip != null)
            {
                // Neu ch tung phat doan nay, phat tu dau
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
        //  Neu clip chua duoc phat vi dang pause, doi den khi phat
        while (!voiceStarted)
        {
            yield return null;
        }

        // Doi phat xong hoac bi pause
        while (audioSource.isPlaying || isPaused)
        {
            yield return null;
        }

        //  reset cac bien, next intro
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

    public void ShowWelcome()
    {
        StopVoice();
        DisableAll();
        Welcome.SetActive(true);
        ShowSoundButton(false);
    }

    public void GoToIntro1()
    {
        StopVoice();
        DisableAll();
        Intro_1.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro1Clip, GoToIntro2);
    }

    public void GoToIntro2()
    {
        StopVoice();
        DisableAll();
        Intro_2.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro2Clip, GoToIntro3);
    }

    public void GoToIntro3()
    {
        StopVoice();
        DisableAll();
        Intro_3.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro3Clip, GoToIntro4);
    }

    public void GoToIntro4()
    {
        StopVoice();
        DisableAll();
        Intro_4.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro4Clip, GoToIntro5);
    }

    public void GoToIntro5()
    {
        StopVoice();
        DisableAll();
        Intro_5.SetActive(true);
        ShowSoundButton(true);
        PlayVoice(intro5Clip, GoToChooseMask);
    }

    public void GoToChooseMask()
    {
        StopVoice();
        DisableAll();
        ChooseMask.SetActive(true);
        ShowSoundButton(false);
    }

    // Thino
    public void GoToThinoIntro()
    {
        DisableAll();
        Thino_intro.SetActive(true);
        ShowSoundButton(false);
    }

    public void GoToThinoDetail()
    {
        DisableAll();
        Thino_detail.SetActive(true);
        ShowSoundButton(false);
    }

    public void BackToThinoIntro()
    {
        DisableAll();
        Thino_intro.SetActive(true);
        ShowSoundButton(false);
    }
    public void GoToThi_NoCreateMaskScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask"); // Dat flag
        PlayerPrefs.Save(); // save
        SceneManager.LoadScene("CreateThi_No_Mask"); 
    }
    public void GoToTutorialScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask"); // Dat flag
        PlayerPrefs.Save(); 
        SceneManager.LoadScene("Tutorial"); 
    }


    // Chipheo
    public void GoToChipheoIntro()
    {
        DisableAll();
        Chipheo_intro.SetActive(true);
        ShowSoundButton(false);
    }

    public void GoToChipheoDetail()
    {
        DisableAll();
        Chipheo_detail.SetActive(true);
        ShowSoundButton(false);
    }

    public void BackToChipheoIntro()
    {
        DisableAll();
        Chipheo_intro.SetActive(true);
        ShowSoundButton(false);
    }
    public void GoToChi_PheoCreateMaskScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask"); 
        PlayerPrefs.Save(); 
        SceneManager.LoadScene("CreateChi_Pheo_Mask"); 
    }

    // wukong
    public void GoToWukongIntro()
    {
        DisableAll();
        Wukong_intro.SetActive(true);
        ShowSoundButton(false);
    }

    public void GoToWukongDetail()
    {
        DisableAll();
        Wukong_detail.SetActive(true);
        ShowSoundButton(false);
    }

    public void BackToWukongIntro()
    {
        DisableAll();
        Wukong_intro.SetActive(true);
        ShowSoundButton(false);
    }
    public void GoToWukongCreateMaskScene()
    {
        PlayerPrefs.SetString("ReturnTo", "ChooseMask");
        PlayerPrefs.Save(); 
        SceneManager.LoadScene("CreateMask"); 
    }


    private void DisableAll()
    {
        Welcome.SetActive(false);
        Intro_1.SetActive(false);
        Intro_2.SetActive(false);
        Intro_3.SetActive(false);
        Intro_4.SetActive(false);
        Intro_5.SetActive(false);
        ChooseMask.SetActive(false);

        Thino_intro.SetActive(false);
        Thino_detail.SetActive(false);
        Chipheo_intro.SetActive(false);
        Chipheo_detail.SetActive(false);
        Wukong_intro.SetActive(false);
        Wukong_detail.SetActive(false);
    }
}

