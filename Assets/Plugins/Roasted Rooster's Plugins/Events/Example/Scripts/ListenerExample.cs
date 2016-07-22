using UnityEngine;
using System.Collections;
using rr.plugins.events;

public class ListenerExample : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Awake () {
        var moduleEvent = new ModuleEvent("TestModule", 1);
        target.GetComponent<ModuleEventRaiser>().AddListener(moduleEvent, this.DebugTest);
	}
	
	public void DebugTest() {
        Debug.Log("Test");
    }
}
