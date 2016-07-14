using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWebForms
{
    public class Lamp : Device, ILampMode
    {
        private BrightnessLevel level;
        public Lamp(bool status, string name, BrightnessLevel level)
          :base (status, name)
        {
          this.level = level;
        }
        public BrightnessLevel Level
        {
            get
            {
                return level;
            }
        }
        public void SetLowBrightness()
        {
            if (Status)
            {
                level = BrightnessLevel.Low;
            }
        }
        public void SetMediumBrightness()
        {
            if (Status)
            {
                level = BrightnessLevel.Medium;
            }
        }
        public void SetHighBrightness()
        {
            if (Status)
            {
                level = BrightnessLevel.High;
            }
        }
        public override string ToString()
        {
            string mod;
            if (level == BrightnessLevel.Low)
            {
                mod = "Низкий";
            }
            else if (level == BrightnessLevel.Medium)
            {
                mod = "Средний";
            }
            else if (level == BrightnessLevel.High)
            {
                mod = "Высокий";
            }
            else
            {
                mod = "Не задан";
            }
            return base.ToString() + "<br />" + "Режим яркости: " + mod.ToString() ;
        }
    }
}