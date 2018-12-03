<%@ Page Title="Add venue" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddVenue.aspx.cs" Inherits="SMMC.Performances.AddVenue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>

    <p><asp:Label runat="server" Text="Name: " ID="nameLabelID"></asp:Label>
        <asp:TextBox ID="NameID" runat="server"></asp:TextBox>
    </p>

    <p><asp:Label runat="server" Text="Street address: " ID="addressLabelID"></asp:Label>
        <asp:TextBox ID="AddressID" runat="server"></asp:TextBox>
    </p>

    <p><asp:Label runat="server" Text="Suburb: " ID="SuburbLabelID"></asp:Label>
        <asp:TextBox ID="SuburbID" runat="server"></asp:TextBox>
    </p>

    <p><asp:Label runat="server" Text="City: " ID="cityLabelID"></asp:Label>
        <asp:TextBox ID="CityID" runat="server"></asp:TextBox>
    </p>

    <p><asp:Label runat="server" Text="Postcode: " ID="PostcodeLabelID"></asp:Label>
        <asp:TextBox ID="PostcodeID" runat="server"></asp:TextBox>
    </p>

    <p><asp:Label runat="server" Text="Phone number: " ID="phoneLabelID"></asp:Label>
        <asp:TextBox ID="PhoneID" runat="server"></asp:TextBox>
    </p>
    <p><asp:Button ID="InsertButton" runat="server" Text="Add venue" OnClick="InsertButton_Click" /></p>

</asp:Content>
