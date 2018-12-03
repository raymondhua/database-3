<%@ Page Title="Add new performance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.Performances.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <legend>Insert info here</legend>
        <asp:Label ID="MajorLabelID" Text="Major: " runat="server"></asp:Label> 
        <asp:DropDownList ID="MajorDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="MajorDDL_SelectedIndexChanged"></asp:DropDownList>
        <p></p>
        <asp:Label ID="VenueLabelID" Text="Select instrument: " runat="server"></asp:Label> 
        <asp:DropDownList ID="VenueDDL" runat="server"></asp:DropDownList>
        <p></p>
        <asp:Label ID="LevelLabelID" Text="Enter date: " runat="server"></asp:Label> 
        <asp:TextBox ID="DateID" runat="server" TextMode="Date"></asp:TextBox>
        <p></p>
        <asp:Label ID="TimeLabelID" Text="Enter time: (HH:MM)" runat="server"></asp:Label> 
        <asp:TextBox ID="TimeID" runat="server" TextMode="Time"></asp:TextBox>
        <p></p>
        <asp:Button ID="SubmitButton" runat="server" Text="Insert" OnClick="SumbitButton_OnClick" />
    </fieldset>
</asp:Content>
