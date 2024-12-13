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
            int result = 0;
            if (result != 0)
            { 
            int.TryParse(input.ToString(), out result);    
            }
            return result;
        }
    }
}
