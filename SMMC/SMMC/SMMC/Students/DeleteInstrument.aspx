<%@ Page Title="Unenrol instrument from student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteInstrument.aspx.cs" Inherits="SMMC.Students.DeleteInstrument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Students</legend>
        <asp:Label ID="SuccessLabel" runat="server"></asp:Label>  
        <asp:DropDownList ID="PersonDDL" runat="server"></asp:DropDownList>
        <asp:Button ID="SelectPersonButton" runat="server" Text="Select student(s)" OnClick="SelectPersonButton_Click" />
        <p></p>

        <asp:CheckBoxList ID="ActorCheckBox" runat="server"></asp:CheckBoxList>
        <asp:Button ID="DeleteInstrumentsButton" runat="server" Text="Delete instruments(s)" OnClick="DeleteInstrumentsButton_Click" />
    </fieldset>
</asp:Content>
