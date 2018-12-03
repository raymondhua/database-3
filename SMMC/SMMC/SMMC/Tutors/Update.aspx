<%@ Page Title="Update tutor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="SMMC.Tutors.Update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <fieldset>
    <legend>Update</legend>
    <asp:DropDownList ID="TutorDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TutorDDL_SelectedIndexChanged"></asp:DropDownList>
    <p></p>
    </fieldset>
    <fieldset>
        <legend>Update info here</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Phone number: " ID="phoneLabelID"></asp:Label>
            <asp:TextBox ID="PhoneID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="What type are you: " ID="opLabelID"></asp:Label>
        <asp:RadioButtonList ID="TutorType" runat="server">

            <asp:ListItem Text ="Head" Value="Head" />
            <asp:ListItem Text ="Junior" Value="Junior" />
            <asp:ListItem Text ="Senior" Value="Senior" />

        </asp:RadioButtonList>
        </p>

    </fieldset>

    <fieldset>
        <legend>Address details</legend>

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

        <asp:Button ID="SubmitButton" runat="server" Text="Update tutor" OnClick="SubmitButton_Click" />
    </fieldset>
</asp:Content>
