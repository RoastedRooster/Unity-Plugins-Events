using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rr.plugins.events {

    /// <summary>
    /// Describes the data contained in a 
    /// module event. It's the minimal information
    /// required to releventlty distinguished two 
    /// different events.
    /// </summary>
    public class ModuleEvent {

        public string   ModuleName  { get; private set; } // name of the module firing this event
        public int      EventType   { get; private set; } // value of an enum defined in each module

        /// <summary>
        /// Constructor, require the module name and the event type ID
        /// </summary>
        /// <param name="moduleName">The name of the module that handle this event</param>
        /// <param name="eventType">The ID defined in the module for this event</param>
        public ModuleEvent(string moduleName, int eventType) {
            this.ModuleName = moduleName;
            this.EventType = eventType;
        }
    }

}
