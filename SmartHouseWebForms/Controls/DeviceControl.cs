using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace SmartHouseWebForms
{
    public class DeviceControl : Panel
    
    { 
        private int id; // Ключ  устройства в коллекции устройств
        private IDictionary<int, Device> devicesDictionary; // Коллекция устройств
        private Label statusLabel; // Отображение статуса устройства
        private Button onButton; // Кнопка Включения устройства
        private Button offButton; // Кнопка выключения устройства
        private Button deleteButton; // Кнопка удаления устройства
        private DropDownList brightnessLevelList;

        public DeviceControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
        }
        protected void Initializer()
        {
            CssClass = "device-div";
           
            Controls.Add(Span("Устройство: " + devicesDictionary[id].Name + "<br />"));
            if (devicesDictionary[id] is IStatus)
            {
                Controls.Add(Span("Состояние устройства:" + devicesDictionary[id].Status.ToString()  + "<br />"));
                
            }
            if (devicesDictionary[id] is ILampMode)
            {
                
                BrightnessLevel a = ((ILampMode)devicesDictionary[id]).Level;
                Controls.Add(Span("Уровень яркости:" + a.ToString() + "<br />"));
                
            }
            if (devicesDictionary[id] is ILampMode)
            {
                Controls.Add(Span("Выберите режим: "));
                brightnessLevelList = new DropDownList();
                brightnessLevelList.ID = "b" + id;
                brightnessLevelList.Items.Add(BrightnessLevel.High.ToString());
                brightnessLevelList.Items.Add(BrightnessLevel.Low.ToString());
                brightnessLevelList.Items.Add(BrightnessLevel.Medium.ToString());
                if (HttpContext.Current.Session["BLevel"] != null)
                {
                    brightnessLevelList.SelectedIndex = (int)HttpContext.Current.Session["BLevel"];
                }
                Controls.Add(brightnessLevelList);
                Button setBrightnessLevel = Button("Установить", "setB ");
                setBrightnessLevel.Click += SetBrightnessLevelButtonClick;
                Controls.Add(setBrightnessLevel);
                Controls.Add(Span("<br />"));
                
            }
            //if (devicesDictionary[id] is IStatus)
            //{
            //    onButton = Button("Включить", "on");
                            
            //}
            //onButton.Click += ButtonClick;
            //Controls.Add(onButton);
            onButton = new Button();
            onButton.ID = "o" + id.ToString();
            onButton.Text = "Включить";
            onButton.Click += OnButtonClick;
            Controls.Add(onButton);

            offButton = new Button();
            offButton.Text = "Выключить";
            offButton.Click += OffButtonClick;
            Controls.Add(offButton);


            Controls.Add(Span("<br />"));

            deleteButton = new Button();
            deleteButton.ID = "d" + id.ToString();
            deleteButton.Text = "Удалить устройство";
            deleteButton.Click += DeleteButtonClick;
            Controls.Add(deleteButton);
        }

        private void OffButtonClick(object sender, EventArgs e)
        {
            devicesDictionary[id].OffDevice();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            devicesDictionary[id].OnDevice();
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
            Controls.Clear();
            Initializer();
        }

    }
}