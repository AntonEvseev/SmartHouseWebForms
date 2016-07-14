<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHouseWebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SmartHouse</title>
    <style>
        body{
            background: #373C4A;
            color: rgb(209,209,217);
            font-size: 16px;
        }
        #devicesPanel{
            position: absolute;
            
            top: 100px;
        }
        .on{
            
            border-radius: 90px;
            background-repeat: no-repeat;
            background-image: url(image/on.png);
            width: 100px;
            height: 100px;
        }
        .off {
            
            border-radius: 90px;
            background-repeat: no-repeat;
            background-image: url(image/off.png);
            width: 100px;
            height: 100px;
        }
            /*//кнопка удаления*/
        
            .button{
              
  position: relative;
  display: inline-block;
  font-size: 90%;
  font-weight: 700;
  color: rgb(209,209,217);
  text-decoration: none;
  text-shadow: 0 -1px 2px rgba(0,0,0,.2);
  padding: .5em 1em;
  outline: none;
  border-radius: 3px;
  background: linear-gradient(rgb(110,112,120), rgb(81,81,86)) rgb(110,112,120);
  box-shadow:
   0 1px rgba(255,255,255,.2) inset,
   0 3px 5px rgba(0,1,6,.5),
   0 0 1px 1px rgba(0,1,6,.2);
  transition: .2s ease-in-out;
}
         .button:hover:not(:active) {
  background: linear-gradient(rgb(126,126,134), rgb(70,71,76)) rgb(126,126,134);
}
         .list{
             width: 200px;
             background: #373C4A;
             color: rgb(209,209,217);
            font-size: 16px;
            border-radius: 3px;
         }
         .device-div{
             width: 250px;
             float: left;
             position: relative;
             top: 20px;
         }
  #header{
            color: #40E0D0;
            font-size: 40px;
            position: absolute;
            left: 50%;
  }
  
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="header"  runat="server" Text="Умный дом"/>
               
        <br />
         <asp:DropDownList ID="dropDownDevicesList" class="button" runat="server">
                <asp:ListItem>Лампа</asp:ListItem>
                <asp:ListItem>Радио</asp:ListItem>
                
            </asp:DropDownList>
        
            <asp:Button ID="addDevicesButton" class="button" runat="server" Text="Добавить" />
            <br />
         <asp:Panel ID="devicesPanel" runat="server" ></asp:Panel>

    </div>
    </form>
</body>
</html>
