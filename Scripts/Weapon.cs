using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Weapon
    {
        float damage;
        float ammo;
        float attackSpeed;
        float range;
        string name;
        bool melee;

        public float Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        public float Ammo
        {
            get
            {
                return ammo;
            }

            set
            {
                ammo = value;
            }
        }

        public float AttackSpeed
        {
            get
            {
                return attackSpeed;
            }

            set
            {
                attackSpeed = value;
            }
        }

        public float Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public bool Melee
        {
            get
            {
                return melee;
            }

            set
            {
                melee = value;
            }
        }

        public Weapon(float damage, float ammo, float attackSpeed, float range, string name, bool melee)
        {
            this.Name = name;
            this.Damage = damage;
            this.AttackSpeed = attackSpeed;
            if (melee == true)
            {
                this.Range = 1;
                this.Ammo = 1;
                this.Melee = true;
            }
            else
            {
                this.Range = range;
                this.Ammo = ammo;
                this.Melee = false;
            }

        }
    }
}
