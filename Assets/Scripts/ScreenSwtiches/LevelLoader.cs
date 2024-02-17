using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerRefs;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionSeconds = 1f;

    public bool isTransitioning = false;

    [Header("Loading Screen Handlers")]
    [SerializeField] private Image loadingImage;
    [SerializeField] private Eras eras;
    public Sprite preColonialEraImage;
    public Sprite spanishEraImage;
    public Sprite japaneseEraImage;
    public Sprite martialLawEraImage;
    public Sprite justBlackImage;

    private void Awake()
    {
        ChangeLoadingImage();
    }

    public void ChangeLoadingImage()
    {
        switch (eras)
        {
            case Eras.PreColonial:
                loadingImage.sprite = preColonialEraImage;
                break;
            case Eras.Spanish:
                loadingImage.sprite = spanishEraImage;
                break;
            case Eras.American:
                loadingImage.sprite = japaneseEraImage;
                break;
            case Eras.Japanese:
                loadingImage.sprite = japaneseEraImage;
                break;
            case Eras.MartialLaw:
                loadingImage.sprite = martialLawEraImage;
                break;
            case Eras.None:
                loadingImage.sprite = justBlackImage;
                break;
        }
    }

    public void ChangeErasForLoadingScreen(int eraIndex)
    {
        switch(eraIndex) {
            case 0:
                eras = Eras.PreColonial;
                break;
            case 1:
                eras = Eras.Spanish;
                break;
            case 2:
                eras = Eras.Japanese;
                break;
            case 3:
                eras = Eras.American;
                break;
            case 4:
                eras = Eras.MartialLaw;
                break;
            case 5:
                eras = Eras.None;
                break;
        }
    }

    public void LoadGame(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void LoadLastGame(string SaveStatePrefName, string fallbackScene)
    {
        string lastSave = PlayerPrefs.GetString(SaveStatePrefName, fallbackScene);

        if (lastSave != "")
        {
            StartCoroutine(LoadLevel(lastSave));
        }
        else
        {
            StartCoroutine(LoadLevel(fallbackScene));
        }

    }

    public bool IsWithLapuLapu()
    {
        string result = PlayerPrefs.GetString("PreColonial-Chapter3-LapuLapu", "n");
        return result != "n";
    }

    public void CutsceneConsequencesChapter3Ambush()
    {
        if (IsWithLapuLapu())
        {
            PlayerPrefs.SetString("PreColonial-Era", "PreColonial-Chapter3-WithLapuLapu");
            StartCoroutine(LoadLevel("PreColonial-Chapter3-WithLapuLapu"));
        }
        else
        {
            PlayerPrefs.SetString("PreColonial-Era", "PreColonial-Chapter3-WithoutLapuLapu");
            StartCoroutine(LoadLevel("PreColonial-Chapter3-WithoutLapuLapu"));
        }
    }

    public void LoadChapter3FinalConsqeuences()
    {
        string hasBeenAssaulted = PlayerPrefs.GetString("Chapter2-Assaulted", "y");

        if (hasBeenAssaulted == "n")
        {
            StartCoroutine(LoadLevel("Chapter3-FinalGuerrillaPrologue"));
        } else
        {
            StartCoroutine(LoadLevel("Chapter3-IsabelKillsMonsi"));
        }
    }

    public void LoadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentNameScene = currentScene.name;
        StartCoroutine(LoadLevel(currentNameScene));
    }

    IEnumerator LoadLevel (string scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSeconds);

        SceneManager.LoadScene(scene);
    }
}
