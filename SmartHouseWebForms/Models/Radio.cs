using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWebForms
{
    public class Radio : Device, ISetVolume, ISetWave
    {
        private int temp;
        private double wave;
        public Radio(bool status, string name)
            : base(status, name)
        {
            
        }
        public int Volume
        {
            get
            {
                return temp;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    temp = value;
                }
            }
        }
        public void SetVolume(int input)
        {
            if (Status)
            {
                temp = input;
            }
        }
        public void UpVolume()
        {
            if (Status)
            {
                if (temp < 100)
                {
                    temp += 1;
                }
           }
        }
        public void LessVolume()
        {
            if (Status)
            {

                if (temp > 1)
                {
                    temp -= 1;
                }
                else
                {
                    temp = 0;
                }
            }
        }
        public double Wave
        {
            get
            {
                return wave;
            }
            set
            {
                if (value >= 87.5 && value <= 108)
                {
                    wave = value;
                }
            }
        }
        public void SetWave(double input)
        {
            if (Status)
            {
                wave = input;
            }
        }
        public void UpWave()
        {
            if (Status)
            {
                if (wave < 108)
                {
                    wave += 0.1;
                }
            }
        }
        public void LessWave()
        {
            if (Status)
            {
                if (wave > 87.6)
                {
                    wave -= 0.1;
                }
                else
                {
                    wave = 87.5;
                }
            }
        }
        public override string ToString()
        {
            return base.ToString() + ", Громкость: " + temp + ", \nВолна: " + wave + "FM";
        }
    }
}