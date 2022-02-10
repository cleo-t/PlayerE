using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    // The locations of each checkpoint (use empty child objects for this - easier to edit in the scene)
    // Also include the first checklpoint
    private List<Transform> checkpoints;
    [SerializeField]
    // How close does a player need to be to a checkpoint in order to activate it?
    private float checkpointDistanceRequirement = 1;

    private GameObject player;
    private uint currentCheckpointIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");

        AggressiveDoor.KillPlayerEvent += this.OnPlayerKill;
        KillFloor.KillPlayerEvent += this.OnPlayerKill;
        SpikeBehaviour.KillPlayerEvent += this.OnPlayerKill;
        this.currentCheckpointIndex = 0;
    }

    void Update()
    {
        if (this.currentCheckpointIndex < this.checkpoints.Count - 1)
        {
            float distanceToNextCheckpoint = Vector3.Distance(this.player.transform.position, this.checkpoints[(int)this.currentCheckpointIndex + 1].position);
            if (distanceToNextCheckpoint < this.checkpointDistanceRequirement)
            {
                this.currentCheckpointIndex++;
                Debug.Log("Now at checkpoint " + this.currentCheckpointIndex);
            }
        }
    }

    private void OnPlayerKill()
    {
        this.player.GetComponent<CharacterController>().enabled = false;
        this.player.transform.position = this.checkpoints[(int)this.currentCheckpointIndex].position;
        this.player.GetComponent<CharacterController>().enabled = true;
        Debug.Log("KILL");
    }
}
