using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuizTrigger : MonoBehaviour
{
    public GameObject visualCue;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject quizUI;
    public TextMeshProUGUI questionText;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;
    public TextAsset inkJSON;

    private Story story;
    private bool playerInRange;
    private bool quizActive;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        quizUI.SetActive(false);
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


        if (playerInRange && !quizActive && Input.GetKeyDown(KeyCode.E))
        {
            StartQuiz();
        }
    }


    private void StartQuiz()
    {
        PlayerMovement.inputLocked = true;
        story = new Story(inkJSON.text);
        quizUI.SetActive(true);
        quizActive = true;
        DisplayNext();
    }

    private void DisplayNext()
    {
        ClearChoices();

        if (story == null)
        {
            Debug.LogError("Story is null!");
            return;
        }

        if (story.canContinue)
        {
            string line = story.Continue().Trim();
            Debug.Log("Story line: " + line);
            questionText.text = line;
        }
        else
        {
            Debug.Log("Story can't continue.");
        }

        Debug.Log("Choices available: " + story.currentChoices.Count);

        Button firstButton = null;

        foreach (var choice in story.currentChoices)
        {
            Debug.Log("Spawning choice: " + choice.text);
            Button button = Instantiate(choiceButtonPrefab, choiceContainer).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text.Trim();
            int choiceIndex = story.currentChoices.IndexOf(choice);
            button.onClick.AddListener(() => OnChoiceSelected(choiceIndex));

            if (firstButton == null)
                firstButton = button;
        }

        if (firstButton != null)
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);

        if (story.currentChoices.Count == 0 && !story.canContinue)
        {
            Debug.Log("Story finished, ending quiz.");
            EndQuiz();
        }
    }

    private void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        DisplayNext();
    }

    private void EndQuiz()
    {
        quizUI.SetActive(false);
        quizActive = false;
        PlayerMovement.inputLocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visualCue.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visualCue.SetActive(false);
            playerInRange = false;
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
