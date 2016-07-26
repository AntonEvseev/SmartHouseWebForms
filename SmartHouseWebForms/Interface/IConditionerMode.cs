using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseWebForms
{
    public interface IConditionerMode
    {
        void SetCoolMode();
        void SetHeatMode();
        void SetFanMode();
        void SetDryMode();
    }
}
