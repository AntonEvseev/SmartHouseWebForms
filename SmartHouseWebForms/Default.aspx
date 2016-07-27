<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHouseWebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SmartHouse</title>
    <link runat="server" href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="header"  runat="server" Text="SmartHouse"/>
            <br />
            <asp:DropDownList ID="dropDownDevicesList" class="button" runat="server">
                    <asp:ListItem>Лампа</asp:ListItem>
                    <asp:ListItem>Радио</asp:ListItem>
                    <asp:ListItem>Телевизор</asp:ListItem>
                    <asp:ListItem>Холодильник</asp:ListItem>
                    <asp:ListItem>Кондиционер</asp:ListItem>
             </asp:DropDownList>
             <asp:Button ID="addDevicesButton" class="button" runat="server" Text="Добавить" />
             <br />
             <asp:Panel ID="devicesPanel" runat="server" ></asp:Panel>
        </div>
    </form>
</body>
</html>
