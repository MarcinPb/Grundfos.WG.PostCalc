using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Utility
{
    public interface IEditedViewModel
    {
        bool Save(); 
        void Close(); 
    }
}
