using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class player4 : movement
    {
        void Awake()
        {
            x = "p4Horizontal";
            y = "p4Vertical";
            throwKey = "p4Throw";
            catchKey = "p4Catch";
            teamColor = Color.red;
            animator = GetComponent<Animator>();
        }
        public override void customStart()
        {
            max = screenBounds.x;
            min = 0;
        }
    }

