using UnityEngine;
using System.Collections;
using rr.plugins.events;

public class ListenerExample : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Awake () {

        // Bind listeners
        var moduleEvent = new ModuleEvent("TestModule", 1);
        var moduleEvent2 = new ModuleEvent("TestModule", 2);
        target.GetComponent<ModuleEventRaiser>().AddListener(moduleEvent, this.DebugTest);
        target.GetComponent<ModuleEventRaiser>().AddListener(moduleEvent2, this.DebugTest2);

        // Trigger event
        target.GetComponent<ModuleEventRaiser>().FireEvent("TestModule", 2); // Print 100 logs
        Debug.Log("Main Thread Info"); // Should print before or during the execution of DebugTest2() (DebugTest2 is asynch)
        target.GetComponent<ModuleEventRaiser>().FireEvent("TestModule", 1); // Should print after the 100 logs, the listeners are processed sequentially
    }
	
	public void DebugTest() {
        Debug.Log("Asynch : Test Event 1");
    }

    public void DebugTest2() {
        for(var i = 0; i < 100; i++)
            Debug.Log("Asynch : Test Event 2");
    }
}
