<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHouseWebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
         <asp:DropDownList ID="dropDownDevicesList" runat="server">
                <asp:ListItem>Лампа</asp:ListItem>
                <asp:ListItem>Радио</asp:ListItem>
                
            </asp:DropDownList>
            <asp:Button ID="addDevicesButton" runat="server" Text="Добавить" />
            <br />
         <asp:Panel ID="devicesPanel" runat="server" ></asp:Panel>

    </div>
    </form>
</body>
</html>
