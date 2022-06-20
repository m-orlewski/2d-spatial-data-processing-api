using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Api
{
    public class Inserter
    {
        SqlConnection conn = null;

        public Inserter(SqlConnection c)
        {
            conn = c;
        }

        public void showMenu()
        {

        }
    }
}
