using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWebForms
{
    public abstract class Device : IStatus
    {
        private bool status;
        public Device()
        {
            
        }
        public Device(bool status, string name)
        {
            Status = status;
            Name = name;
        }
        public string Name { get; set; }
        public bool Status 
        { 
            get 
        {
            return status;
        }
            set
            {
                status = value;
            }
        }
        public virtual void OnDevice()
        {
            if (Status == false)
            {
                status = true;
            }
        }
        public virtual void OffDevice()
        {
            if (Status)
            {
                status = false;
            }
        }
        public override string ToString()
        {
            string status;
            if (this.status)
            {
                status = "Включено";
            }
            else
            {
                status = "Выключено";
            }
            return status;
        }
    }
}