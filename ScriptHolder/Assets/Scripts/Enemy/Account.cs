using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemy
{
    class Account
    {
        string name;
        string storymodeProgress;
        string currentmode;

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

        public string StorymodeProgress
        {
            get
            {
                return storymodeProgress;
            }

            set
            {
                storymodeProgress = value;
            }
        }

        public string Currentmode
        {
            get
            {
                return currentmode;
            }

            set
            {
                currentmode = value;
            }
        }

        public Account(string name, string storymode, string currentMode)
        {
            this.Name = name;
            this.StorymodeProgress = storymode;
            this.Currentmode = currentMode;
        }

        void newAccount()
        {
            
        }
    }
}
