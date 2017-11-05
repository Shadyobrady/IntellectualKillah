using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Account
    {
        string name;
        storymodeProgress accountProgress;
        currentmode accountMode;

        public enum storymodeProgress
        {
            StoryModeUnstarted,
            StoryModeStarted,
            Level1Complete,
            Cutscene1Complete,
            Level2Complete,
            Cutscene2Complete,
            Level3Complete,
            Cutscene3Complete,
            StoryModeComplete,
        };

        public enum currentmode
        {
            freeplay,
            storymode,
            loggedin,
            loggedout
        };

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


        public storymodeProgress AccountProgress
        {
            get
            {
                return AccountProgress;
            }

            set
            {
                AccountProgress = value;
            }
        }

        public currentmode AccountMode
        {
            get
            {
                return accountMode;
            }

            set
            {
                accountMode = value;
            }
        }

        public Account(string name)
        {
            this.Name = name;
            this.AccountProgress = storymodeProgress.StoryModeUnstarted;
            this.AccountMode = currentmode.loggedin;
        }

        public Account(string name, storymodeProgress inputProgress, currentmode modestatus)
        {
            this.Name = name;
            this.AccountProgress = inputProgress;
            this.AccountMode = modestatus;
        }

        public Dictionary<string,Account> AccountsList()
        {
            Dictionary<string, Account> tempList = new Dictionary<string, Account>();
            using (StreamReader fileRead = new StreamReader("Assets/files/accounts.txt"))
            {
                while (!fileRead.EndOfStream)
                {
                    string newinput = fileRead.ReadLine();
                    string[] pts = newinput.Split('/');
                    string name = pts[0];
                    int storymodeint = int.Parse(pts[1]);
                    storymodeProgress storymode = (storymodeProgress)storymodeint;
                    int gamemodeint = int.Parse(pts[2]);
                    currentmode gamemode = (currentmode) gamemodeint;
                    Account tempAccount = new Account(name, storymode, gamemode);
                    tempList.Add(name,tempAccount);
                }
            }
            return tempList;
        }

        public void Login(string newAccount)
        {
            Dictionary<string, Account> Accounts = AccountsList();
            Account currAccount;
            if (Accounts.ContainsKey(newAccount))
            {
                Accounts.TryGetValue(newAccount, out currAccount);
                currAccount.AccountMode = currentmode.loggedin;
            }
            else
            {
                using (StreamWriter writer = new StreamWriter("Assets/Files/accounts.txt"))
                {
                    Account newAccountYo = new Account(newAccount);
                    writer.Write(newAccountYo);
                }
            }
        }

        public override string ToString()
        {
            
            return string.Format(format: Name + "/" + (int)AccountProgress + "/" + (int)AccountMode);
        }

        public static string FindScene(storymodeProgress value)
        {
            Dictionary<storymodeProgress,string> newdic = new Dictionary<storymodeProgress, string>();
            using (StreamReader reader = new StreamReader("Assets/Files/SceneDic.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string inputstring = reader.ReadLine();
                    string[] pts = inputstring.Split('/');
                    string scenelist = pts[0];
                    int valueint = int.Parse(pts[1]);
                    storymodeProgress progesStorymodeProgress = (storymodeProgress) valueint;
                    newdic.Add(progesStorymodeProgress,scenelist);
                }
            }
            string scenename;
            newdic.TryGetValue(value, out scenename);
            return scenename;
        }
    }
}
