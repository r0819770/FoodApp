using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }

}
