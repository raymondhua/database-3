<%@ Page Title="Delete person" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="SMMC.People.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Select person</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p></p>
        <asp:CheckBoxList ID="PeopleCB" runat="server"></asp:CheckBoxList>
        <asp:Button ID="SubmitButton" runat="server" Text="Delete person(s)" OnClick="SubmitButton_Click" />
    </fieldset>
</asp:Content>
