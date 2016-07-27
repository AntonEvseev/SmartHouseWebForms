using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWebForms
{
    public class Conditioner : Device, IConditionerMode, ISetTemp
    {
        private ConditionerMode mode;
        private int temp;
        public Conditioner(bool status, string name)
            : base(status, name)
        {
            
        }
        public void SetCoolMode()
        {
            if (Status)
            {
                mode = ConditionerMode.Cool;
            }
        }
        public void SetHeatMode()
        {
            if (Status)
            {
                mode = ConditionerMode.Heat;
            }
        }
        public void SetFanMode()
        {
            if (Status)
            {
                mode = ConditionerMode.Fan;
            }
        }
        public void SetDryMode()
        {
            if (Status)
            {
                mode = ConditionerMode.Dry;
            }
        }
        public void SetTemp(int input)
        {
            if (Status)
            {
                temp = input;
            }
        }
        public int Temp
        {
            get
            {
                return temp;
            }
            set
            {
                if (value >= 16 && value <= 30)
                {
                    temp = value;
                }
            }
        }
        public override string ToString()
        {
            string mod;
            if (mode == ConditionerMode.Cool)
            {
                mod = "Охлаждение";
            }
            else if (mode == ConditionerMode.Heat)
            {
                mod = "Обогрев";
            }
            else if (mode == ConditionerMode.Fan)
            {
                mod = "Вентилятор";
            }
            else if (mode == ConditionerMode.Dry)
            {
                mod = "Очистка";
            }
            else
            {
                mod = "Не задан";
            }
            return base.ToString() + ", Режим: " + mod + ", \nУровень температуры: " + temp + "*C";
        }
    }
}