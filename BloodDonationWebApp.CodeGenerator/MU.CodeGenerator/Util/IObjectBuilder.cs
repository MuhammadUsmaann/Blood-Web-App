using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CodeGenerator.MVP.Util
{
    public interface IObjectBuilder
    {
       StringBuilder BuildEntity(string strProjectName, string strObjectName, DataTable dt, String InterfaceClass);
       StringBuilder BuildSevice(string strProjectName, string strObjectName, DataTable dt, String InterfaceClass);
    }
}
