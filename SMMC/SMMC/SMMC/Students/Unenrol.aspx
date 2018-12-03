<%@ Page Title="Unenrol student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unenrol.aspx.cs" Inherits="SMMC.Students.Unenrol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
    <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
    <legend>Students</legend>
    <asp:CheckBoxList ID="ActorCheckBox" runat="server"></asp:CheckBoxList>
    <asp:Button ID="SubmitButton" runat="server" Text="Unenrol student(s)" OnClick="SubmitButton_Click" />
    </fieldset>
</asp:Content>
