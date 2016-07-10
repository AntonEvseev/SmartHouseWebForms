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
        
       // private BrightnessLevel mode;
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
                mod = "низкий";
            }
            else if (level == BrightnessLevel.Medium)
            {
                mod = "средний";
            }
            else if (level == BrightnessLevel.High)
            {
                mod = "высокий";
            }
            else
            {
                mod = "не задан";
            }
            //return base.ToString() + ", режим яркости: " + mod;
            return base.ToString() + mod.ToString() ;
        }
    }
}