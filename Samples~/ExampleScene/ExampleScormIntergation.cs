using ScormManager.ScormAPI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScormIntergation : MonoBehaviour
{
    [SerializeField] private Button initializeButton;
    [SerializeField] private Button setScoreButton30;
    [SerializeField] private Button setScoreButton60;
    [SerializeField] private Button markAsComplete;
    [SerializeField] private Button markAsFail;
    [SerializeField] private Button finishButton;
    [SerializeField] private GameObject timer;

    private bool timerRunning = false;
    private TextMeshProUGUI timerText;
    private Coroutine timerCoroutine;
    private readonly float sendSessionTimeEverySeconds = 10f;

    private void Start()
    {
        if (
                initializeButton == null
                || setScoreButton30 == null
                || setScoreButton60 == null
                || markAsComplete == null
                || finishButton == null
                || markAsFail == null
                || timer == null
            )
        {
            Debug.LogError("Buttons not attached to the script!");
            return;
        }
        if (timer.TryGetComponent<TextMeshProUGUI>(out timerText))
        {
            // Reset timer variables
            timerText.text = "00:00";
            timerRunning = false;
            
            // Set button listeners
            SetListeners();

            // Make enabled only Initialize button at start
            initializeButton.enabled = true;
            setScoreButton30.enabled = false;
            setScoreButton60.enabled = false;
            markAsComplete.enabled = false;
            markAsFail.enabled = false;
            finishButton.enabled = false;
        }
    }

    private void SetListeners()
    {
        initializeButton.onClick.AddListener(() => {
            // Enable all buttons, disable Initialize button
            setScoreButton30.enabled = true;
            setScoreButton60.enabled = true;
            markAsComplete.enabled = true;
            markAsFail.enabled = true;
            finishButton.enabled = true;
            initializeButton.enabled = false;

            // Send Initialize message to SCORM backend
            ScormPlayer.Initialize();

            // Start timer
            timerRunning = true;
            timerCoroutine = StartCoroutine(TimerCoroutine());
        });

        setScoreButton30.onClick.AddListener(() => ScormPlayer.SetScore(30));

        setScoreButton60.onClick.AddListener(() => ScormPlayer.SetScore(60));

        markAsComplete.onClick.AddListener(() => SetTrainngAsCompleted(true));

        markAsFail.onClick.AddListener(() => SetTrainngAsCompleted(false));

        finishButton.onClick.AddListener(() => {

            // Disable all buttons, enable Initialize button
            setScoreButton30.enabled = false;
            setScoreButton60.enabled = false;
            markAsComplete.enabled = false;
            markAsFail.enabled = false;
            finishButton.enabled = false;
            initializeButton.enabled = true;
            
            // Stop timer
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;

            // Send Finish message to SCROM backend
            ScormPlayer.Finish();
        });
    }

    private void SetTrainngAsCompleted(bool passed)
    {
        timerRunning = false;
        ScormPlayer.MarkAsComplete(passed);
    }

    private IEnumerator TimerCoroutine()
    {
        float elapsedTime = 0f;
        while (timerRunning)
        {
            int totalSeconds = Mathf.FloorToInt(elapsedTime);
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            string displayTimeText = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = displayTimeText;

            if (elapsedTime > 0 && elapsedTime % sendSessionTimeEverySeconds == 0)
            {
                string scormTimeText = string.Format("{0:0000}:{1:00}:{2:00}.00", hours, minutes, seconds);
                // Send 'cmi.core.session_time' message every sendSessionTimeEverySeconds seconds (10 seconds default)
                ScormPlayer.SetTime(scormTimeText);
            }

            elapsedTime++;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
