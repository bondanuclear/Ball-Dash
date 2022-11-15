using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] GameObject gameOverPanel = null;
    PlayerMovement playerMovement;
    [SerializeField] bool showPanel = false;
    [SerializeField] Button reloadButton;
    [SerializeField] Slider changeSize;
    ChangeSize changeSizeComponent;
    [Header("Change Size ")]
    [SerializeField] Vector3 startingScale;
    [SerializeField] float startingPercent = 1f;
    public bool showSlider = false;
    public List<int> multipleObjects = new List<int>();
    //public bool ShowSlider {get; set;}
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null) return;
        else
        {
            instance = this;

            playerMovement = GetComponent<PlayerMovement>();
            Time.timeScale = 1;
            startingScale = transform.localScale;
            SetSlider();
        }
    }

    private void SetSlider()
    {
        changeSize.value = transform.localScale.z / startingScale.z;
        changeSize.minValue = 0.7f;
        changeSize.maxValue = 1f;
        changeSize.gameObject.SetActive(showSlider);
    }

    private void OnEnable() {
        changeSizeComponent = GetComponent<ChangeSize>();
        reloadButton.onClick.AddListener(ReloadLevel);
        changeSize.onValueChanged.AddListener(delegate { ResizePlayer(); });
    }

    // Update is called once per frame
    void Update()
    {
       showPanel = playerMovement.gameIsOver;
       if(showPanel)
       {
        gameOverPanel.SetActive(true);
        showPanel = false;
        Time.timeScale = 0;
       }
       if(showSlider)
       {
            //Debug.Log("Should show SLIDER");
            changeSize.gameObject.SetActive(true);
       } else if(!showSlider)
       {
            transform.localScale = startingScale;
            changeSize.value = startingPercent;
            //Debug.Log("Should NOT show SLIDER");
            changeSize.gameObject.SetActive(false);
       }

    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void ResizePlayer()
    {
        //Debug.Log($"value of slider {changeSize.value}");
        float percentToSize = (changeSize.value * startingScale.z)/startingPercent;
        transform.localScale = new Vector3(percentToSize, percentToSize, percentToSize);
    }
}
