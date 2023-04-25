﻿using FIMSpace.Generating.Checker;
using FIMSpace.Graph;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FIMSpace.Generating.Planning.PlannerNodes.BuildSetup
{

    public class PR_CollectFields : PlannerRuleBase
    {
        public override string GetDisplayName(float maxWidth = 120) { return "Collect All Fields"; }
        public override string GetNodeTooltipDescription { get { return "Get all active fields from build, port will output multiple fields inside"; } }
        public override EPlannerNodeType NodeType { get { return EPlannerNodeType.ReadData; } }
        public override Color GetNodeColor() { return new Color(1.0f, 0.75f, 0.25f, 0.9f); }
        public override Vector2 NodeSize { get { return new Vector2(200, _EditorFoldout ? 141 : 121); } }
        public override bool DrawInputConnector { get { return false; } }
        public override bool DrawOutputConnector { get { return false; } }
        public override bool IsFoldable { get { return true; } }


        [Port(EPortPinType.Input, EPortNameDisplay.Default, 1)] public PGGStringPort OnlyTagged;
        [Port(EPortPinType.Input, EPortNameDisplay.Default, 1)] public BoolPort GetDuplicates;
        [Port(EPortPinType.Output, EPortValueDisplay.HideValue, "Gathered Fields")] public PGGPlannerPort MultiplePlanners;
        [HideInInspector] public bool IgnoreThisField = false;

        public override void OnStartReadingNode()
        {
            FieldPlanner cplan = FieldPlanner.CurrentGraphExecutingPlanner;
            if (cplan == null) return;

            BuildPlannerPreset build = cplan.ParentBuildPlanner;
            if (build == null) return;

            List<FieldPlanner> planners = new List<FieldPlanner>();
            string tag = OnlyTagged.GetInputValue;
            bool checkTags = !string.IsNullOrEmpty(tag);

            for (int p = 0; p < build.BasePlanners.Count; p++)
            {
                var pl = build.BasePlanners[p];

                if (pl.DisableWholePlanner) continue;
                if (checkTags) if (pl.IsTagged(tag) == false) continue;

                if (!pl.Discarded)
                {
                    planners.Add(pl);
                }

                var duplList = pl.GetDuplicatesPlannersList();
                if (duplList != null)
                    for (int d = 0; d < duplList.Count; d++)
                    {
                        if (duplList[d].Discarded) continue;
                        planners.Add(duplList[d]);
                    }
            }

            if (IgnoreThisField)
            {
                var myPlanner = CurrentExecutingPlanner;
                if (myPlanner != null) planners.Remove(myPlanner);
            }

            MultiplePlanners.AssignPlannersList(planners);
        }



#if UNITY_EDITOR
        UnityEditor.SerializedProperty sp = null;
        public override void Editor_OnNodeBodyGUI(ScriptableObject setup)
        {
            base.Editor_OnNodeBodyGUI(setup);

            baseSerializedObject.Update();
            if (_EditorFoldout)
            {
                GUILayout.Space(1);
                if (sp == null) sp = baseSerializedObject.FindProperty("IgnoreThisField");
                UnityEditor.EditorGUILayout.PropertyField(sp, true);
            }
            baseSerializedObject.ApplyModifiedProperties();
        }


#endif

    }
}