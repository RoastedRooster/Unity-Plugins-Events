using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace rr.plugins.events {

    /// <summary>
    /// Static manager to handle specific static events and
    /// configure the event system (unauthorized events, debugging)
    /// </summary>
    public class ModuleEventManager {

        #region SINGLETON PATTERN

        /// <summary>
        /// Returns (and create if necessary) the singleton of
        /// the event manager.
        /// </summary>
        /// <returns>A singleton of the event manager</returns>
        public static ModuleEventManager Get() {
            if (instance == null)
                instance = new ModuleEventManager();

            return instance;    
        }

        protected static ModuleEventManager instance;
        protected ModuleEventManager() {
            staticListenerList = new Dictionary<ModuleEvent, Action>();
            unauthorizedEventList = new List<ModuleEvent>();
            debugEventList = new List<ModuleEvent>();
        }

        #endregion

        // List of events to ignore
        public List<ModuleEvent> unauthorizedEventList;

        // List of events to debug
        public List<ModuleEvent> debugEventList;

        // List of static listeners
        public Dictionary<ModuleEvent, Action> staticListenerList;

    }

}


