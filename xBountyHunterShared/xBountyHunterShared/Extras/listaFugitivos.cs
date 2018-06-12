using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xBountyHunterShared.Models;

namespace xBountyHunterShared.Extras
{
    public class listaFugitivos
    {
        public List<mFugitivos> ocFugitivos;
        databaseManager DB = new databaseManager();

        public listaFugitivos()
        {
            
        }

        public List<mFugitivos> getFugitivos()
        {
            ocFugitivos = DB.selectNoCaptured();
            return ocFugitivos;
        }

        public List<mFugitivos> getCapturados()
        {
            ocFugitivos = DB.selectCaptured();
            return ocFugitivos;
        }
    }
}
