using ScormManager.ScormAPI;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScormIntergation : MonoBehaviour
{
    [SerializeField] private Button initializeButton;
    [SerializeField] private Button setScoreButton;
    [SerializeField] private Button markAsComplete;
    [SerializeField] private Button finishButton;

    private void Start()
    {
        if (initializeButton == null || setScoreButton == null || markAsComplete == null || finishButton == null)
        {
            Debug.LogError("Buttons not attached to the script!");
            return;
        }

        SetListeners();
    }

    private void SetListeners()
    {
        initializeButton.onClick.AddListener(ScormPlayer.Initialize);
        setScoreButton.onClick.AddListener(() => ScormPlayer.SetScore(60));
        markAsComplete.onClick.AddListener(() => ScormPlayer.MarkAsComplete(true));
        finishButton.onClick.AddListener(ScormPlayer.Finish);
    }
}
