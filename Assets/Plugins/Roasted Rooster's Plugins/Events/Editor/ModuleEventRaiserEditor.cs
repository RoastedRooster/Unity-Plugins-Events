using UnityEngine;
using System.Collections;
using UnityEditor;

namespace rr.plugins.events.editor {

    [CustomEditor(typeof(ModuleEventRaiser))]
    public class ModuleEventRaiserEditor : Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var raiser = (ModuleEventRaiser)target;

            EditorGUILayout.LabelField("Current listeners");

            EditorGUILayout.BeginVertical();
            var map = raiser.ListenerMap;
            if(map != null && map.Keys != null) {
                foreach (var key in map.Keys) {
                    EditorGUILayout.LabelField(key.ModuleName + "." + key.EventType);
                }
            }

            EditorGUILayout.EndVertical();

        }

    }
}
