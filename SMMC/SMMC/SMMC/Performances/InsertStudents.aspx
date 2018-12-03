<%@ Page Title="Insert students into performances" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertStudents.aspx.cs" Inherits="SMMC.Performances.InsertStudents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Students</legend>
        <asp:Label ID="SuccessLabel" runat="server"></asp:Label>
        <asp:DropDownList ID="PerformanceDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectPerformanceButton_Click"></asp:DropDownList>
        <p></p>
        <asp:Label ID="StudentCBLabel" runat="server"></asp:Label>  
        <asp:DropDownList ID="StudentDDL" runat="server"></asp:DropDownList>
        <asp:Button ID="SelectStudentButton" runat="server" Text="Select student(s)" OnClick="SelectStudentButton_Click" />
        <asp:DropDownList ID="InstrumentDDL" runat="server"></asp:DropDownList>
        <asp:Button ID="SubmitButton" runat="server" Text="Add student" OnClick="SubmitButton_Click" />
    </fieldset>
</asp:Content>
