using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    [Header("References")]
    public GameObject CharacterBoard;
    public CharacterBoardManager characterBoard;
    public GameObject QuitBoard;
    public PlayerController playerController;
    public Image soundImage;
    public GameObject Menu;
    [Header("Show Menu Button")]
    public Button homeButton;
    public Button soundButton;
    public Button showMenuButton;
    public Button showBoardButton;
    [Header("Character Board")]
    public Button closeCBoardButton;
    public Button submitCBoardButton;
    [Header("Quit Board")]
    public Button closeQBoardButton;
    public Button confirmQBoardButton;
    [Header("Sprite")]
    public Sprite soundOn;
    public Sprite soundOff;

    private bool isSoundActive = true;
    private bool isMenuOpen = false;
    private float closeSize = 0f;
    private float openSize = 0.5f;

    private void Start()
    {
        closeCBoardButton.GetComponent<Button>().onClick.AddListener(CloseBoard);
        submitCBoardButton.GetComponent<Button>().onClick.AddListener(SubmitBoard);

        closeQBoardButton.GetComponent<Button>().onClick.AddListener(CloseQuitBoard);
        confirmQBoardButton.GetComponent<Button>().onClick.AddListener(QuitMenu);

        showMenuButton.GetComponent<Button>().onClick.AddListener(ShowMenu);
        homeButton.GetComponent<Button>().onClick.AddListener(ShowQuitBoard);
        soundButton.GetComponent<Button>().onClick.AddListener(SoundButton);
        showBoardButton.GetComponent<Button>().onClick.AddListener(ShowBoard);
    }

    void ShowBoard()
    {
        if (!CharacterBoard.activeInHierarchy)
        {
            characterBoard.GetCharacterData();
            CharacterBoard.SetActive(true);
        }
            
    }

    void CloseBoard()
    {
        //Reset Data

        CharacterBoard.SetActive(false);
    }

    void SubmitBoard()
    {
        //Save data
        characterBoard.SaveCharacterData();
        Debug.Log("Data Saved");
    }

    void QuitMenu()
    {
        Destroy(GameObject.FindObjectOfType<UnDestroy>().gameObject);
        playerController.ResetPlayer();
        SceneManager.LoadScene(0);
    }

    void ShowMenu()
    {
        if (!isMenuOpen)
            StartCoroutine(IE_Open(openSize));
        else
            StartCoroutine(IE_Open(closeSize));

        isMenuOpen = !isMenuOpen;
    }

    void ShowQuitBoard()
    {
        if (!QuitBoard.activeInHierarchy)
            QuitBoard.SetActive(true);
    }

    void CloseQuitBoard()
    {
        QuitBoard.SetActive(false);
    }

    void SoundButton()
    {
        AudioListener.pause = true;
        isSoundActive = !isSoundActive;
        if (!isSoundActive)
        {
            soundImage.sprite = soundOff;
        }
        else
        {
            soundImage.sprite = soundOn;
        }
    }

    IEnumerator IE_Open(float size)
    {
        float timeOfTravel = 1f;
        float currentTime = 0;
        float normalizedValue;
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 
            Menu.GetComponent<RectTransform>().localScale = Vector3.Lerp(Menu.GetComponent<RectTransform>().localScale, new Vector3(size, size, 1f), normalizedValue);
            yield return null;
        }
    }
}
