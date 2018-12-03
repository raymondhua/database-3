<%@ Page Title="Insert person" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.People.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Insert info here</legend>
        <b><asp:Label ID="SuccessLabelID" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="First name: " ID="firstNameLabelId"></asp:Label>
            <asp:TextBox ID="FirstNameID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Last name: " ID="lastNameLabelId"></asp:Label>
            <asp:TextBox ID="LastNameID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Date of birth: " ID="dobLabelID"></asp:Label>
            <asp:TextBox ID="BirthID" runat="server" TextMode="Date"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Phone number: " ID="phoneLabelID"></asp:Label>
            <asp:TextBox ID="PhoneID" runat="server"></asp:TextBox>
        </p>
    </fieldset>

    <fieldset>
        <legend>Address details</legend>

        <p><asp:Label runat="server" Text="Select existing address (leave blank if not displayed and enter details below): " ID="addressLabelID"></asp:Label>
            <asp:DropDownList ID="AddressList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AddressList_SelectedIndexChanged"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Address: " ID="StreetLabel"></asp:Label>
            <asp:TextBox ID="StreetID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Suburb: " ID="SuburbLabel"></asp:Label>
            <asp:TextBox ID="SuburbID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="City: " ID="CityLabel"></asp:Label>
            <asp:TextBox ID="CityID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Postcode: " ID="PostcodeLabel"></asp:Label>
            <asp:TextBox ID="Postcode" runat="server"></asp:TextBox>
        </p>

    </fieldset>

    <p><asp:Button ID="ButtonId" runat="server" Text="Insert" OnClick="ButtonId_Click" /></p>
</asp:Content>
