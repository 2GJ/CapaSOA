using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using Tools2GJ;

namespace CapaSOA2GJ
{
    public class CapaFacade2GJ
    {
        public DataSet QryFastByDS(String In_Qry)
        {
            //String2GJ Obj2GJ = new String2GJ();
            refFacade2GJBA.Facade2GJBA objFac = new refFacade2GJBA.Facade2GJBA();
            return objFac.QryFastStrByDS(Convert.ToBase64String(Encoding.UTF8.GetBytes(In_Qry)).ToString());
        }
    }
}
