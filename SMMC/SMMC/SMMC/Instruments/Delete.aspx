<%@ Page Title="Delete instrument" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="SMMC.Instruments.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
    <legend>Instruments</legend>
    <asp:Label ID="SuccessLabel" runat="server"></asp:Label>  
    <asp:CheckBoxList ID="ActorCheckBox" runat="server"></asp:CheckBoxList>
    <asp:Button ID="Button1" runat="server" Text="Delete instrument(s)" />
    </fieldset>
</asp:Content>
