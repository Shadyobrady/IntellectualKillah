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
        float range;
        string name;
        bool melee;
        double fireRate;
        float mag;

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

        public float Mag
        {
            get
            {
                return mag;
            }

            set
            {
                mag = value;
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

        public double FireRate
        {
            get
            {
                return fireRate;
            }

            set
            {
                fireRate = value;
            }
        }

        public Weapon(float damage, float ammo,  float range, string name, bool melee, double firerate)
        {
            this.Name = name;
            this.Damage = damage;
            this.FireRate = firerate;
            if (melee == true)
            {
                this.Range = 1;
                this.Ammo = 1;
                this.Melee = true;
                this.Mag = 1;
            }
            else
            {
                this.Range = range;
                this.Ammo = ammo;
                this.Melee = false;
                this.Mag = ammo;

            }

        }
    }
}
