using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BC
{
    public abstract class Pattern : MonoBehaviour
    {
        public SceneSlime order;

        public abstract void SlimeStateStart();
        public abstract void SlimeStateUpdate();
        public abstract void SlimeStateEnd();

    }

}
