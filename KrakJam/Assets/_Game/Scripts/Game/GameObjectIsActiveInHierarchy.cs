// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Tests if a GameObject Variable is Active or not.")]
    public class GameObjectIsActiveInHierarchy : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The GameObject variable to test.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Event to send if the GamObject is Active.")]
        public FsmEvent isActive;

        [Tooltip("Event to send if the GamObject is NOT Active.")]
        public FsmEvent isNotActive;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a bool variable.")]
        public FsmBool storeResult;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		
        GameObject go;
		

        public override void Reset()
        {
            gameObject = null;
            isActive = null;
            isNotActive = null;
            storeResult = null;
            everyFrame = false;
        }
		
        public override void OnEnter()
        {
            DoIsGameObjectActive();
			
            if (!everyFrame)
            {
                Finish();
            }
        }
		
        public override void OnUpdate()
        {
            DoIsGameObjectActive();
        }
		
		
        void DoIsGameObjectActive()
        {
            go = Fsm.GetOwnerDefaultTarget(gameObject);
            if(go != null)
            {
                var goIsActive = go.activeInHierarchy;
			
                storeResult.Value = goIsActive;

                Fsm.Event(goIsActive ? isActive : isNotActive);
            }
        }
    }
}