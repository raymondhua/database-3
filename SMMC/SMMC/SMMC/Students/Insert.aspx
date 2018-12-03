<%@ Page Title="Enrol student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.Students.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Insert info here</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Is the person in the database: " ID="Label1"></asp:Label>
            <asp:DropDownList ID="PersonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PersonDDL_SelectedIndexChanged"></asp:DropDownList>
        </p>
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

        <p><asp:Label runat="server" Text="Are you graduated from school: " ID="opLabelID"></asp:Label>
        <asp:RadioButtonList ID="OpenDivision" runat="server">

            <asp:ListItem Text ="Yes" Value="True" />
            <asp:ListItem Text ="No" Value="False" />

        </asp:RadioButtonList>
        </p>

    </fieldset>

    <fieldset>
        <legend><asp:Label runat="server" Text="Address details" ID="AddressHeaderLabel"></asp:Label></legend>

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

    <fieldset>
        <legend>Parents information</legend>
        <%-- First part --%>
        <p><asp:Label runat="server" Text="Are you under 18?: " ID="U18Label"></asp:Label>
        <p></p>
        <asp:RadioButtonList ID="ParentRRL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ParentRRL_SelectedIndexChanged">

            <asp:ListItem Text ="Yes" Value="True" />
            <asp:ListItem Text ="No" Value="False" />

        </asp:RadioButtonList>
        <p><asp:Label runat="server" Text="Select parent below: " ID="SelectParentLabel"></asp:Label>
        <asp:DropDownList ID="ExistingParentDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ExistingParentDDL_SelectedIndexChanged"></asp:DropDownList></p>
        <%-- Second part --%>
        <p><asp:Label runat="server" Text="First name: " ID="ParentFNLabel"></asp:Label>
            <asp:TextBox ID="ParentFNID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Last name: " ID="ParentLNLabel"></asp:Label>
            <asp:TextBox ID="ParentLNID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Date of birth: " ID="ParentDOBLabel"></asp:Label>
            <asp:TextBox ID="ParentDOB" runat="server" TextMode="Date"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Phone number: " ID="ParentsPhoneLabel"></asp:Label>
            <asp:TextBox ID="ParentsPhoneID" runat="server"></asp:TextBox>
        </p>

    </fieldset>

    <p><asp:Button ID="ButtonId" runat="server" Text="Insert" OnClick="ButtonId_Click" /></p>
</asp:Content>
