using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    public static TorchManager instance;

    public bool allTorchesLit
    {
        get
        {
            return this.torchCount >= this.torchLimit;
        }
        private set
        {

        }
    }

    [SerializeField]
    private int torchLimit = 12;

    private int torchCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.torchCount = 0;
        foreach(GameObject g in new List<GameObject>(GameObject.FindGameObjectsWithTag("Torch Pair")))
        {
            TorchTrigger tt = g.GetComponentInChildren<TorchTrigger>();
            tt.torchOn += this.OnTorchTrigger;
        }
    }

    void OnTorchTrigger()
    {
        this.torchCount++;
    }
}
