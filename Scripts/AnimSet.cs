using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimSet
    {
        public Animation walk;
        public Animation run;
        public Animation attack;
        public Weapon AnimSetWeapon;
       
        public AnimSet(Animation walk, Animation run, Animation attack, Weapon currentweapon)
        {
            this.walk = walk;
            this.run = run;
            this.attack = attack;
            this.AnimSetWeapon = currentweapon;
        }

    }
}
