using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    class Datamanager
    {
        public static Datamanager instance;
        public LoginRole role;
        public string id;

        public enum LoginRole
        {
            User,
            Developer,
            admin
        }

        public Datamanager()
        {
            instance = this;            
        }


    }
}
