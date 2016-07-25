using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;



namespace SmartHouseWebForms
{
    public class DeviceControl : Panel
    
    { 
        private int id; // Ключ  устройства в коллекции устройств
        private IDictionary<int, Device> devicesDictionary; // Коллекция устройств
        private Label errorMessage; // Отображение статуса устройства
        private Button onButton; // Кнопка Включения устройства
        private Button plusVolume; // Кнопка увеличения громкости
        private Button minusVolume;
        private Button lessWave;
        private Button upWave;
        private Button set; // Кнопка установки значения
        private Button deleteButton; // Кнопка удаления устройства
        private DropDownList brightnessLevelList;//а
        private Image im;
        private string error;  // текст сообщение о необходимости включить устройство
        public string Error
        {
            get
            {
                return error;
            }
        }
        private TextBox volSet;
        
        
        public DeviceControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
        }
        protected void Initializer()
        {
           
            
            CssClass = "device-div";
            im = new Image();
            // Добавление gif анимации в зависимости от девайса
            if (devicesDictionary[id] is ILampMode)
            {
                
                if (devicesDictionary[id].Status == false)
                {
                    im.ImageUrl = "image/LampOff.gif";
                    
                }
                else
                {
                    im.ImageUrl = "image/LampOn.gif";
                }
                Controls.Add(im);
            }
            if (devicesDictionary[id] is ISetWave)
            {
                im.ImageUrl = "image/radio.png";
                Controls.Add(im);
            }
            errorMessage = new Label();
            errorMessage.ForeColor = System.Drawing.Color.Red;
            errorMessage.Text = Error;
            Controls.Add(errorMessage);
            Controls.Add(Span("<br />" + "Устройство: " + devicesDictionary[id].Name + "<br />"));
            if (devicesDictionary[id] is IStatus)
            {
               
               Controls.Add(Span("Состояние устройства:" + devicesDictionary[id].ToString() + "<br />"));
            }
            if (devicesDictionary[id] is ISetVolume)
            {
               minusVolume = new Button();
               minusVolume.ID = "vm" + id.ToString();
               minusVolume.CssClass = "button";
               minusVolume.Text = "Volume-";
               minusVolume.Click += MinusVolumeButtonClick;
               Controls.Add(minusVolume);
               plusVolume = new Button();
               plusVolume.ID = "vp" + id.ToString();
               plusVolume.CssClass = "button";
               plusVolume.Text = "Volume+";
               plusVolume.Click += PlusVolumeButtonClick;
               Controls.Add(plusVolume);
               Controls.Add(Span("<br />"));
               //int temp = ((ISetVolume)devicesDictionary[id]).Volume;   наверное лишнее
               volSet = new TextBox();
               //volSet = TextBox(temp);
               volSet.Text = "";
               Controls.Add(volSet);
               set = new Button();
               set.ID = "s" + id.ToString();
               set.CssClass = "button";
               set.Text = "Set Volume";
               set.Click += SetButtonClick;
               Controls.Add(set);
               Controls.Add(Span("<br />"));

               
               
            }
            if(devicesDictionary[id] is ISetWave)
            {
                lessWave = new Button();
                lessWave.ID = "lw" + id.ToString();
                lessWave.CssClass = "button";
                lessWave.Text = "Wave-";
                lessWave.Click += LessWaveButtonClick;
                Controls.Add(lessWave);
                upWave = new Button();
                upWave.ID = "uw" + id.ToString();
                upWave.CssClass = "button";
                upWave.Text = "Wave+";
                upWave.Click += UpWaveButtonClick;
                Controls.Add(upWave);
                
            }
            if (devicesDictionary[id] is ILampMode)
            {
                Controls.Add(Span("Выберите режим: "));
                brightnessLevelList = new DropDownList();
                brightnessLevelList.ID = "b" + id;
                brightnessLevelList.CssClass = "list";
                brightnessLevelList.Items.Add(BrightnessLevel.High.ToString());
                brightnessLevelList.Items.Add(BrightnessLevel.Low.ToString());
                brightnessLevelList.Items.Add(BrightnessLevel.Medium.ToString());
                if (HttpContext.Current.Session["BLevel"] != null)
                {
                    brightnessLevelList.SelectedIndex = (int)HttpContext.Current.Session["BLevel"];
                }
                Controls.Add(brightnessLevelList);
                Button setBrightnessLevel = Button("Установить", "setB ");
                setBrightnessLevel.CssClass = "button";
                setBrightnessLevel.Click += SetBrightnessLevelButtonClick;
                Controls.Add(setBrightnessLevel);
                Controls.Add(Span("<br />"));
            }
            onButton = new Button();
            onButton.Click += OnButtonClick;
            onButton.ID = "o" + id.ToString();
            if (devicesDictionary[id].Status == false)
            {
                onButton.CssClass = "off";
            }
            else
            {
               onButton.CssClass = "on";
            }
            Controls.Add(onButton);

            Controls.Add(Span("<br />"));

            deleteButton = new Button();
            deleteButton.ID = "d" + id.ToString();
            deleteButton.CssClass = "button";
            deleteButton.Text = "Удалить устройство";
            deleteButton.Click += DeleteButtonClick;
            Controls.Add(deleteButton);
        }

        private void UpWaveButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetWave w = (ISetWave)devicesDictionary[id];
                w.UpWave();
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

        private void LessWaveButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetWave w = (ISetWave)devicesDictionary[id];
                w.LessWave();
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

        private void SetButtonClick(object sender, EventArgs e)
        {
            ISetVolume s = (ISetVolume)devicesDictionary[id];
            int a = Convert.ToInt32(volSet.Text);
            s.SetVolume(a);
            Controls.Clear();
            Initializer();
        }

        private TextBox TextBox(int temp)
        {
            TextBox textBox = new TextBox();
            textBox.Text = temp.ToString();
            textBox.Columns = 1;
            return textBox;
        }

        private void MinusVolumeButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetVolume v = (ISetVolume)devicesDictionary[id];
                v.LessVolume();
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

        private void PlusVolumeButtonClick(object sender, EventArgs e)
        {
            if(devicesDictionary[id].Status)
            {
                ISetVolume v = (ISetVolume)devicesDictionary[id];
                v.UpVolume();
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

       
        private void OnButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status == false)
            {
                devicesDictionary[id].OnDevice();
                onButton.CssClass = "off";
            }
            else
            {
                devicesDictionary[id].OffDevice();
                onButton.CssClass = "on";
            }
            Controls.Clear();
            Initializer();
       }
       private void DeleteButtonClick(object sender, EventArgs e)
       {
           devicesDictionary.Remove(id); // Удаление устройства из коллекции
           Parent.Controls.Remove(this); // Удаление графики для устройства
       }
       protected Button Button(string text, string pref)
       {
           Button button = new Button();
           button.ID = pref + id;
           button.Text = text;
           return button;
       }
       protected HtmlGenericControl Span(string innerHTML)
        {
           HtmlGenericControl span = new HtmlGenericControl("span");
           span.InnerHtml = innerHTML;
           return span;
        }
        private void SetBrightnessLevelButtonClick(object sender, EventArgs e)
       {
           
           if (devicesDictionary[id].Status)
           {
               HttpContext.Current.Session["BLevel"] = brightnessLevelList.SelectedIndex;
               ILampMode b = (ILampMode)devicesDictionary[id];
               switch (brightnessLevelList.SelectedIndex)
               {
                   default:
                       b.SetHighBrightness();
                       break;
                   case 1:
                       b.SetLowBrightness();
                       break;
                   case 2:
                       b.SetMediumBrightness();
                       break;
               }
           }
           else
           {
              error = "Включите устройство!";
           }
            Controls.Clear();
            Initializer();
       }

        
   }
}