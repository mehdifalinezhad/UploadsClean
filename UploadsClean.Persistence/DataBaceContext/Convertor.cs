using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadsClean.Persistence.DataBaceContext
{
    public static class Convertor
    {
        public static int ToInt(this object input)
        {
            try
            {
               
                int.TryParse(input.ToString(), out int result);

                return result;
            }
            catch
            {

                return 0;   
            }
            
        }
    }
}
