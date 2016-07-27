using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartHouseWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        private IDictionary<int, Device> devicesDictionary;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }
            else
            {
                devicesDictionary = new SortedDictionary<int, Device>();
                devicesDictionary.Add(1, new Lamp(false, "Лампа", BrightnessLevel.High));
                devicesDictionary.Add(2, new Radio(false, "Радио"));
                devicesDictionary.Add(3, new TV(false, "Телевизор"));
                devicesDictionary.Add(4, new Fridge(false, "Холодильник"));
                devicesDictionary.Add(5, new Conditioner(false, "Кондиционер"));
                Session["Devices"] = devicesDictionary;
                Session["NextId"] = 6;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (IsPostBack)
                {
                    addDevicesButton.Click += AddDeviceButtonClick;
                }
                InitialiseDevicesPanel(); 
            }
            else
            {
                InitialiseDevicesPanel();
            }
        }
        protected void InitialiseDevicesPanel()
        {
            foreach (int key in devicesDictionary.Keys)
            {
                devicesPanel.Controls.Add(new DeviceControl(key, devicesDictionary));
            }
        }
        // Обработчик нажатия кнопки добавления устройств
        protected void AddDeviceButtonClick(object sender, EventArgs e)
        {
            Device newDevice;
            switch (dropDownDevicesList.SelectedIndex)
            {
                default:
                    newDevice = new Lamp(false, "Лампа", BrightnessLevel.High);
                    break;
                case 1:
                    newDevice = new Radio(false, "Радио");
                    break;
                case 2:
                    newDevice = new TV(false, "Телевизор");
                    break;
                case 3:
                    newDevice = new Fridge(false, "Холодильник");
                    break;
                case 4:
                    newDevice = new Conditioner(false, "Кондиционер");
                    break;
            }
            int id = (int)Session["NextId"];
            devicesDictionary.Add(id, newDevice); // Добавление устройства в коллекцию
            devicesPanel.Controls.Add(new DeviceControl(id, devicesDictionary)); // Добавление графики для фигуры устройства
            id++;
            Session["NextId"] = id;
        }
    }
}