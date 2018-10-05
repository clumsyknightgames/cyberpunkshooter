using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    private static Queue<GameObject> FIFOArray = new Queue<GameObject>();

    // TODO: Move to centralised settings, this one is definitely performance-heavy, CPU starts shitting bricks at ~300 physics objects.
    public static int FIFOLimit = 20;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    ///     FIFO Manager to keep track of FIFO objects like debris. Takes objects, deletes objects when the queue overflows.
    /// </summary>
    /// <param name="gameObject">GameObject to keep track of</param>
    public static void FIFO(GameObject gameObject)
    {
        FIFOArray.Enqueue(gameObject);
        if (FIFOArray.Count > FIFOLimit)
        {
            Destroy(FIFOArray.Dequeue());
        }
    }
}
