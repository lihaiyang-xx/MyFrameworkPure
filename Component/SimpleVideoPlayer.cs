
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class SimpleVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Toggle playToggle;
    [SerializeField] private Slider procesSlider;
    [SerializeField] private Transform loading;
    [SerializeField] private Text timeText;
    [SerializeField] private Text titleText;
    [SerializeField] private Button volumeBtn;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button fullScreenBtn;
    [SerializeField] private RectTransform closeRt;

    private bool isFullScreen;
    private bool isSeeking;
    private double targetTime;

    private Vector2 originSize;

    private RectTransform rectTransform;



    private float TotalVideoLength => videoPlayer.frameCount / videoPlayer.frameRate;

    private float ResidueVideoLength => (float)(TotalVideoLength - videoPlayer.time);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originSize = rectTransform.sizeDelta;
        
        isFullScreen = false;
        videoPlayer.skipOnDrop = false;

        playToggle.onValueChanged.AddListener(v =>
        {
            Color color = playToggle.targetGraphic.color;
            color.a = v ? 0 : 1;
            playToggle.targetGraphic.color = color;
            if (v)
            {
                videoPlayer.Play();
            }
            else
            {
                videoPlayer.Pause();
            }
        });

        EventTriggerListener.Get(procesSlider.gameObject).onBeginDrag = d =>
        {
            isSeeking = true;
        };
        EventTriggerListener.Get(procesSlider.gameObject).onEndDrag = d =>
        {
            isSeeking = true;
            targetTime = TotalVideoLength * procesSlider.value;
            videoPlayer.time = targetTime;
            loading?.SetActive(true);
            videoPlayer.Prepare();
            Debug.Log("ÍÏ×§µ½:" + videoPlayer.time + "   " + targetTime);
        };

        videoPlayer.seekCompleted += v =>
        {
            Delay.DoDelay(1, () =>
            {
                isSeeking = false;
                Debug.Log("seek complete:" + v.time);
            });
            loading.SetActive(false);
        };

        videoPlayer.prepareCompleted += v =>
        {
            procesSlider.interactable = true;
            loading.SetActive(false);
        };

        videoPlayer.loopPointReached += v =>
        {
            //videoPlayer.Stop();
            //videoPlayer.Play();
            //videoPlayer.time = 1;
            //videoPlayer.Pause();
        };

        volumeBtn.onClick.AddListener(()=>volumeSlider.SetActive(!volumeSlider.gameObject.activeSelf));
        volumeSlider.onValueChanged.AddListener(v =>
        {
            videoPlayer.SetDirectAudioVolume(0,v);
        });
        fullScreenBtn.onClick.AddListener(() =>
        {
            isFullScreen = !isFullScreen;
            Vector2 sizeDelta = isFullScreen
                ? new Vector2(AppGlobal.GetScreenWidth()+40, AppGlobal.GetScreenHeight()+40)
                : originSize;
            rectTransform.sizeDelta = sizeDelta;
            closeRt.anchoredPosition = isFullScreen ? new Vector2(-100, -100) :new Vector2(38.2f,-6.4f);
        });
    }

    void OnEnable()
    {
        isFullScreen = true;
        fullScreenBtn.onClick?.Invoke();
    }

    void Update()
    {
        if (!videoPlayer.isPrepared || isSeeking)
            return;
        float process = (float)(videoPlayer.time / TotalVideoLength);
        procesSlider.SetValueWithoutNotify(process);
        int totalSecond = (int)ResidueVideoLength;
        int min = totalSecond / 60;
        int second = totalSecond - min * 60;
        timeText.text = $"{min:00}:{second:00}";
    }

    public void Play(string vname, string url)
    {
        loading.SetActive(true);
        titleText.text = vname;
        videoPlayer.url = url;
        videoPlayer.Prepare();
        videoPlayer.Play();
    }

    public void Play()
    {
        loading.SetActive(true);
        videoPlayer.Prepare();
        videoPlayer.Play();
        playToggle.isOn = true;
    }
}