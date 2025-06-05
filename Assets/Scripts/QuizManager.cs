using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI questionText;
    public GameObject choiceButtonPrefab;
    public Transform choiceContainer;
    public GameObject timeoutOverlay; // Optional UI blocker

    private bool isTimedOut = false;
    private float timeoutDuration = 60f;

    void Start()
    {
        story = new Story(inkJSON.text);
        DisplayNext();
    }

    void DisplayNext()
    {
        ClearChoices();

        if (story.canContinue)
        {
            questionText.text = story.Continue().Trim();
        }

        if (story.currentChoices.Count >= 0)
        {
            Button firstButton = null;

            for (int i = 0; i <= story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = Instantiate(choiceButtonPrefab, choiceContainer).GetComponent<Button>();
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text.Trim();
                int choiceIndex = i;
                button.onClick.AddListener(() => OnChoiceSelected(choiceIndex));

                if (firstButton == null)
                    firstButton = button;
            }

            if (firstButton != null)
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
        else if (!story.canContinue)
        {
            questionText.text = "Quiz complete!";
        }
    }

    void OnChoiceSelected(int index)
    {
        if (isTimedOut) return;

        story.ChooseChoiceIndex(index);
        story.Continue(); // Evaluate variables like ~ correct = true

        bool isCorrect = story.variablesState["correct"] != null && (bool)story.variablesState["correct"];
        story.variablesState["correct"] = false;

        if (isCorrect)
        {
            DisplayNext();
        }
        else
        {
            StartCoroutine(TimeoutRoutine());
        }
    }

    IEnumerator TimeoutRoutine()
    {
        isTimedOut = true;
        questionText.text = "Wrong answer! Please wait 60 seconds...";
        if (timeoutOverlay != null)
            timeoutOverlay.SetActive(true);

        ClearChoices();

        yield return new WaitForSeconds(timeoutDuration);

        if (timeoutOverlay != null)
            timeoutOverlay.SetActive(false);

        DisplayNext();
        isTimedOut = false;
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("SampleScene");

    }
    public void Pause()
    {
        //pauseMenuUI.SetActive(true);
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
        SceneManager.LoadScene("Pause");

    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif


    }
}
