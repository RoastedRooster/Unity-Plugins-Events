using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace rr.plugins.events {

    /// <summary>
    /// Allow a gameObject to fire events and to hold
    /// a list of listeners
    /// </summary>
    public class ModuleEventRaiser : MonoBehaviour {

        // List of listeners
        public Dictionary<ModuleEvent, List<Action>> ListenerMap { get; private set; }

        // List of triggered listeners to process
        private List<Action> _triggeredListenerList;

        // Is the component processing a listener
        private bool _processingListener = false;

        /// <summary>
        /// Constructor, initialize the listener map before any call to it.
        /// </summary>
        public ModuleEventRaiser() {
            this.ListenerMap = new Dictionary<ModuleEvent, List<Action>>();
            this._triggeredListenerList = new List<Action>();
        }

        /// <summary>
        /// Register a callback method to invoke when the given event is fired.
        /// Several listener can be set for the same event.
        /// The listener react only for the current gameObject
        /// </summary>
        /// <param name="moduleEvent">The event which trigger the listener</param>
        /// <param name="listener">A method to call when the event is called</param>
        public void AddListener(ModuleEvent moduleEvent, Action listener) {

            if (!ListenerMap.ContainsKey(moduleEvent))
                ListenerMap[moduleEvent] = new List<Action>();

            var listenerList = ListenerMap[moduleEvent];

            if(!listenerList.Contains(listener))
                listenerList.Add(listener);
        }

        /// <summary>
        /// Trigger a specific event, every listener attached is invoked
        /// </summary>
        /// <param name="moduleName">The module in charge of the event</param>
        /// <param name="eventID">The event ID in the module events enum</param>
        public void FireEvent(string moduleName, int eventID) {

            var pair = ListenerMap.FirstOrDefault(l => l.Key.ModuleName.Equals(moduleName) && l.Key.EventType == eventID);
            if (pair.Key == null)
                return;

            var listenerList = ListenerMap[pair.Key];
            foreach(var listener in listenerList) {
                _triggeredListenerList.Add(listener);
            }
        }

        /// <summary>
        /// Process the triggered events sequencially.
        /// </summary>
        public void Update() {
            if (_processingListener)
                return;

            var listener = _triggeredListenerList.FirstOrDefault();
            if (listener == null)
                return;

            // Run the listener asynchronously.
            _processingListener = true;
            AsyncCallback callback = new AsyncCallback(ListenerProcessed);
            IAsyncResult result = listener.BeginInvoke(callback, null);
        }

        private void ListenerProcessed(IAsyncResult result) {
            _triggeredListenerList.Remove(((Action)((AsyncResult)result).AsyncDelegate));
            _processingListener = false;
        }
    }

}
