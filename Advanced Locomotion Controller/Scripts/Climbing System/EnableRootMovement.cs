using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FasTPS
{
    public class EnableRootMovement : StateMachineBehaviour
    {
        ClimbEvents ce;

        public float timer = 0.2f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (ce == null)
            {
                ce = animator.transform.GetComponent<ClimbEvents>();
            }

            if (ce == null)
                return;

            ce.EnableRootMovement(timer);
        }
    }
}
