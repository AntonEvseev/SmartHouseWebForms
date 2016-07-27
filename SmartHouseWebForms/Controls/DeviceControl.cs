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
        private Button minusVolume;// Кнопка уменьшения громкости
        private Button lessWave; // Кнопка уменьшения волны
        private Button upWave; // Кнопка увеличения волны
        private Button previousChannel; //Кнопка предыдущего канала
        private Button nextChannel; // Кнопка следующего канала
        private Button setChannel; // Кнопка установки канала
        private Button setTemp; //Кнопка установки температуры
        private Button setVol; // Кнопка установки значения громкости
        private Button setWave; // Кнопка установки значения волны
        private Button deleteButton; // Кнопка удаления устройства
        private DropDownList brightnessLevelList; 
        private DropDownList conditionerModeList;
        private DropDownList fridgeModeList;
        private Image im;
        private string error;  // Сообщение о необходимости включить устройство 
        public string Error
        {
            get
            {
                return error;
            }
        }
        private TextBox volSet;
        private TextBox waveSet;
        private TextBox tempSet;
        private TextBox channelSet;
       
        
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
            // Добавление изображений в зависимости от девайса
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
                im.ImageUrl = "image/radio2.png";
                Controls.Add(im);
            }
            if (devicesDictionary[id] is ISetChannel)
            {
                im.ImageUrl = "image/TV-Set.png";
                Controls.Add(im);
            }
            if (devicesDictionary[id] is IFridgeMode)
            {
                im.ImageUrl = "image/Fridge2.png";
                Controls.Add(im);
            }
            if (devicesDictionary[id] is IConditionerMode)
            {
                im.ImageUrl = "image/conditioner.png";
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
               volSet = new TextBox();
               volSet.ID = "vs" + id.ToString();
               volSet.Text = "";
               Controls.Add(volSet);
               setVol = new Button();
               setVol.ID = "sv" + id.ToString();
               setVol.CssClass = "button";
               setVol.Text = "Set Volume";
               setVol.Click += SetVolButtonClick;
               Controls.Add(setVol);
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
                waveSet = new TextBox();
                waveSet.ID = "ws" + id.ToString();
                Controls.Add(waveSet);
                setWave = new Button();
                setWave.ID = "sw" + id.ToString();
                setWave.CssClass = "button";
                setWave.Text = "Set Wave";
                setWave.Click += SetWaveButtonClick;
                Controls.Add(setWave);
                Controls.Add(Span("<br />"));
            }
            if (devicesDictionary[id] is ISetChannel)
            {
                previousChannel = new Button();
                previousChannel.ID = "pc" + id.ToString();
                previousChannel.CssClass = "button";
                previousChannel.Text = "Channel-";
                previousChannel.Click += PreviousChannelButtonClick;
                Controls.Add(previousChannel);
                nextChannel = new Button();
                nextChannel.ID = "nc" + id.ToString();
                nextChannel.CssClass = "button";
                nextChannel.Text = "Channel+";
                nextChannel.Click += NextChannelButtonClick;
                Controls.Add(nextChannel);
                channelSet = new TextBox();
                channelSet.ID = "cs" + id.ToString();
                Controls.Add(channelSet);
                setChannel = new Button();
                setChannel.ID = "sc" + id.ToString();
                setChannel.CssClass = "button";
                setChannel.Text = "Set Channel";
                setChannel.Click += SetChannelButtonClick;
                Controls.Add(setChannel);
                Controls.Add(Span("<br />"));
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
            if (devicesDictionary[id] is IConditionerMode)
            {
                Controls.Add(Span("Выберите режим: "));
                conditionerModeList = new DropDownList();
                conditionerModeList.ID = "cm" + id.ToString();
                conditionerModeList.CssClass = "list";
                conditionerModeList.Items.Add(ConditionerMode.Cool.ToString());
                conditionerModeList.Items.Add(ConditionerMode.Dry.ToString());
                conditionerModeList.Items.Add(ConditionerMode.Fan.ToString());
                conditionerModeList.Items.Add(ConditionerMode.Heat.ToString());
                if (HttpContext.Current.Session["CMode"] != null)
                {
                    conditionerModeList.SelectedIndex = (int)HttpContext.Current.Session["CMode"];
                }
                Controls.Add(conditionerModeList);
                Button setConditionerMode = Button("Установить", "setB ");
                setConditionerMode.CssClass = "button";
                setConditionerMode.Click += SetConditionerModeButtonClick;
                Controls.Add(setConditionerMode);
                Controls.Add(Span("<br />"));
                tempSet = new TextBox();
                tempSet.ID = "ts" + id.ToString();
                Controls.Add(tempSet);
                setTemp = new Button();
                setTemp.ID = "st" + id.ToString();
                setTemp.CssClass = "button";
                setTemp.Text = "Set Temp";
                setTemp.Click += SetTempButtonClick;
                Controls.Add(setTemp);
                Controls.Add(Span("<br />"));
            }
            if (devicesDictionary[id] is IFridgeMode)
            {
                Controls.Add(Span("Выберите режим: "));
                fridgeModeList = new DropDownList();
                fridgeModeList.ID = "fm" + id.ToString();
                fridgeModeList.CssClass = "list";
                fridgeModeList.Items.Add(FridgeMode.Defrost.ToString());
                fridgeModeList.Items.Add(FridgeMode.Freezing.ToString());
                fridgeModeList.Items.Add(FridgeMode.Superfreezing.ToString());
                if (HttpContext.Current.Session["FMode"] != null)
                {
                    fridgeModeList.SelectedIndex = (int)HttpContext.Current.Session["FMode"];
                }
                Controls.Add(fridgeModeList);
                Button setFridgeMode = Button("Установить", "setB ");
                setFridgeMode.CssClass = "button";
                setFridgeMode.Click += SetFridgeModeButtonClick;
                Controls.Add(setFridgeMode);
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

        private void SetChannelButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetChannel c = (ISetChannel)devicesDictionary[id];
                int correntChannel;
                string temp = channelSet.Text;
                if (Int32.TryParse(temp, out correntChannel))
                {
                    if (correntChannel >= 0 && correntChannel <= 100)
                    {
                        c.SetChannel(correntChannel);
                    }
                    else
                    {
                        error = "Введите значение от 0 до 100!";
                    }
                }
                else
                {
                    error = "Введите только цифры!";
                }
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

        private void NextChannelButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetChannel c = (ISetChannel)devicesDictionary[id];
                c.NextChannel();
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

        private void PreviousChannelButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetChannel c = (ISetChannel)devicesDictionary[id];
                c.PreviousChannel();
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
        }

        private void SetTempButtonClick(object sender, EventArgs e)
        {
          if (devicesDictionary[id].Status)
          {
              ISetTemp t = (ISetTemp)devicesDictionary[id];
              int currentTemp;
              string temp = tempSet.Text;
              if (Int32.TryParse(temp, out currentTemp))
              {
                  if (currentTemp >= 16 && currentTemp <= 30)
                    {
                        t.SetTemp(currentTemp);
                    }
                    else
                    {
                        error = "Введите значение от 16 до 30!";
                    }
              }
              else
              {
                  error = "Введите только цифры!";
              }
          }
          else
          {
              error = "Включите устройство!";
          }
          Controls.Clear();
          Initializer();
        }
    
        private void SetFridgeModeButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                HttpContext.Current.Session["FMode"] = fridgeModeList.SelectedIndex;
                IFridgeMode f = (IFridgeMode)devicesDictionary[id];
                switch (fridgeModeList.SelectedIndex)
                {
                    default:
                        f.SetDefrost();
                        break;
                    case 1:
                        f.SetFreezing();
                        break;
                    case 2:
                        f.SetSuperFreezing();
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

        private void SetConditionerModeButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                HttpContext.Current.Session["CMode"] = conditionerModeList.SelectedIndex;
                IConditionerMode c = (IConditionerMode)devicesDictionary[id];
                switch (conditionerModeList.SelectedIndex)
                {
                    default:
                        c.SetCoolMode();
                        break;
                    case 1:
                        c.SetDryMode();
                        break;
                    case 2:
                        c.SetFanMode();
                        break;
                    case 3:
                        c.SetHeatMode();
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

        private void SetWaveButtonClick(object sender, EventArgs e)
        {
          
            if (devicesDictionary[id].Status)
            {
                ISetWave w = (ISetWave)devicesDictionary[id];
                double currentWave;
                string temp = waveSet.Text;
                temp = temp.Replace('.', ',');
                if (Double.TryParse(temp, out currentWave))
                {
                    if (currentWave >= 87.5 && currentWave <= 108)
                    {
                        w.SetWave(currentWave);
                    }
                    else
                    {
                        error = "Введите значение от 87.5 до 108!";
                    }
                }
                else
                {
                    error = "Введите только цифры!";
                }
            }
            else
            {
                error = "Включите устройство!";
            }
            Controls.Clear();
            Initializer();
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

        private void SetVolButtonClick(object sender, EventArgs e)
        {
            if (devicesDictionary[id].Status)
            {
                ISetVolume s = (ISetVolume)devicesDictionary[id];
                int correntVol;
                string temp = volSet.Text;
                if (Int32.TryParse(temp, out correntVol))
                {
                    if (correntVol >= 0 && correntVol <= 100)
                    {
                        s.SetVolume(correntVol);
                    }
                    else
                    {
                        error = "Введите значение от 0 до 100!";
                    }
                }
                else
                {
                    error = "Введите только цифры!";
                }
            }
            else
            {
                error = "Включите устройство!";
            }
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