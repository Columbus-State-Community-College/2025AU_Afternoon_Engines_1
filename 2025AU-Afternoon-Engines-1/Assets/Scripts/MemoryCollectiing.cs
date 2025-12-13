using TMPro;
using UnityEngine;

public class MemoryCollectiing : MonoBehaviour
{

    //script for progressing memory collection
    // prevents player from running around and collecting everything at once

    AudioManager audioManager;
    [SerializeField] GameObject Jumpscaresound;
    [SerializeField] GameObject glassShatter;


    [SerializeField] GameObject memory1;
    [SerializeField] GameObject memory2;
    [SerializeField] GameObject memory3;
    [SerializeField] GameObject memory4;
    [SerializeField] GameObject memory5;

    [SerializeField] GameObject puzzleTrigger; //trigger for the puzzle for the final memory
    [SerializeField] GameObject puzzleCanvas;
    [SerializeField] GameObject finalDialogueTrigger;
    [SerializeField] GameObject EndGameTrigger;
    [SerializeField] GameObject puzzleBarrier;
    [SerializeField] GameObject TVTrigger;
    [SerializeField] GameObject frontDoor; //for end game

    [SerializeField] MonsterSpawn spawn;
    // Memory HUD
    [SerializeField] TextMeshProUGUI memoryHUD;

    public static bool finalMemorycollected= false;
    private int currentMemories = 0;







    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }

    private void Start()
    {
   
        // hides all memory besides the first 
        memory2.SetActive(false);
        memory3.SetActive(false);
        memory4.SetActive(false);
        memory5.SetActive(false);

        //set jumpscare as inactive
        Jumpscaresound.SetActive(false);
        glassShatter.SetActive(false);
        TVTrigger.SetActive(false);
       

        //set puzzle trigger inactive 
        puzzleTrigger.SetActive(false);
        puzzleCanvas.SetActive(false);
        finalDialogueTrigger.SetActive(false);
        EndGameTrigger.SetActive(false);
        puzzleBarrier.SetActive(false);

        //hide the front door
        frontDoor.SetActive(false);


      


    }

    private void Update()
    {
        checkPuzzle();
        updateMemoryHUD();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Memory1"))
        {
            Jumpscaresound.SetActive(true);
            memory2.SetActive(true);
            Debug.Log("memory 2 has spawned");
            currentMemories = 1;

        }
        if (other.CompareTag("Memory2"))
        {
            memory3.SetActive(true);
            glassShatter.SetActive(true);
            TVTrigger.SetActive(true);
            Debug.Log("memory 3 has spawned");
            currentMemories = 2;
        }
        if (other.CompareTag("Memory3"))
        {
            memory4.SetActive(true);
            Debug.Log("memory 4 has spawned");
            currentMemories = 3;
        }
        if (other.CompareTag("Memory4"))
        {
            memory5.SetActive(true);
            puzzleTrigger.SetActive(true);
            puzzleBarrier.SetActive(true);
            Debug.Log("memory 5 has spawned");
            currentMemories = 4;
        }
        if (other.CompareTag("Memory5"))
        {
            finalDialogueTrigger.SetActive(true);
            EndGameTrigger.SetActive(true);
            frontDoor.SetActive(true); //have the front door return
            finalMemorycollected = true;
            currentMemories = 5;
            
         


        }




        if (other.CompareTag("MonsterJumpscare"))
        {
            audioManager.PlayJumpScare(audioManager.monsterSFX);
            Destroy(other.gameObject);
            spawn.SpawnMonster();
            
        }

        if (other.CompareTag("GlassShatter"))
        {
            audioManager.PlayJumpScare(audioManager.glassSFX);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PuzzleTrigger"))
        {
            puzzleCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        if(other.CompareTag("TVTrigger"))
        {
            audioManager.PlayTVSFX();
            Destroy(other.gameObject);
        }
    }

    private void checkPuzzle()
    {
        if(puzzleTrigger != null)
        {
            finalDialogueTrigger.SetActive(false);
        }
    }

    private void updateMemoryHUD()
    {
        memoryHUD.text = "Current Memories: " + currentMemories.ToString();
    }

    

      

    }

