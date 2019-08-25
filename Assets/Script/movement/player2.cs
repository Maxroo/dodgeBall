using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class player2 : movement
    {
        void Awake()
        {
            x = "p2Horizontal";
            y = "p2Vertical";
            throwKey = "p2Throw";
            catchKey = "p2Catch";
            teamColor = Color.red;
            animator = GetComponent<Animator>();
        }
        public override void customStart()
        {
            max = screenBounds.x;
            min = 0;
        }
    }

