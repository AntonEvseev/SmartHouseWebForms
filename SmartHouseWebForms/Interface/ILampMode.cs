using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseWebForms
{
    public interface ILampMode
    {
         BrightnessLevel Level
        {
            get;
        }
         void SetLowBrightness();
         void SetMediumBrightness();
         void SetHighBrightness();
    
    }
}
