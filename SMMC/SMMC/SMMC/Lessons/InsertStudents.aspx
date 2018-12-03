<%@ Page Title="Insert student into lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertStudents.aspx.cs" Inherits="SMMC.Lessons.InsertStudents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Select lesson</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <asp:DropDownList ID="LessonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LessonDDL_SelectedIndexChanged"></asp:DropDownList>
        <p></p>
        <asp:CheckBoxList ID="StudentInstrumentCB" runat="server"></asp:CheckBoxList>
        <asp:Button ID="InsertInstrumentsButton" runat="server" Text="Insert students(s)" OnClick="InsertInstrumentsButton_Click" />
    </fieldset>
</asp:Content>
