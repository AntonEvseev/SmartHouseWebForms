using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseWebForms
{
    public interface ISetChannel
    {
        int Channel { get; set; }
        void SetChannel(int input);
        void NextChannel();
        void PreviousChannel();
    }
}
