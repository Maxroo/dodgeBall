using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class player3 : movement
    {
        void Awake()
        {
            x = "p3Horizontal";
            y = "p3Vertical";
            throwKey = "p3Throw";
            catchKey = "p3Catch";
            teamColor = Color.red;
            animator = GetComponent<Animator>();
        }
        public override void customStart()
        {
            max = 0;
            min = -screenBounds.x;
        }
    }

