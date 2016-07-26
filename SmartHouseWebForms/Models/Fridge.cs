using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWebForms
{
    public class Fridge : Device, IFridgeMode
    {
        private int temp;
        private FridgeMode mode;
        public Fridge(bool status, string name)
            : base(status, name)
        {
            //Status = status;
        }
        public void SetSuperFreezing()
        {
            if (Status)
            {
                mode = FridgeMode.Superfreezing;
            }
        }
        public void SetFreezing()
        {
            if (Status)
            {
                mode = FridgeMode.Freezing;
            }
        }
        public void SetDefrost()
        {
            if (Status)
            {
                mode = FridgeMode.Defrost;
            }
        }
        public override string ToString()
        {
            string mod;
            if (mode == FridgeMode.Defrost)
            {
                mod = "Разморозка";
                temp = 0;
            }
            else if (mode == FridgeMode.Freezing)
            {
                mod = "Заморозка";
                temp = -15;
            }
            else if (mode == FridgeMode.Superfreezing)
            {
                mod = "Суперзаморозка";
                temp = -24;
            }
            else
            {
                mod = "Не задан";
                temp = 0;
            }
            return base.ToString() + ", Режим: " + mod + ", \nУровень температуры: " + temp + "*C";
        }
    }
}