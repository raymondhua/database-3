<%@ Page Title="Add lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.Lessons.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <legend>Insert info here</legend>
        <p><asp:Label ID="InstrumentID" Text="Select instrument: " runat="server"></asp:Label> 
        <asp:DropDownList ID="InstrumentDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="InstrumentDDL_SelectedIndexChanged"></asp:DropDownList>
        </p>
        <p><asp:Label ID="LevelLabelID" Text="Select level: " runat="server"></asp:Label> 
        <asp:DropDownList ID="LevelDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LevelDDL_SelectedIndexChanged"></asp:DropDownList>
        </p>
        <p><asp:Label ID="TimeLabelID" Text="Enter time: (HH:MM)" runat="server"></asp:Label> 
        <asp:TextBox ID="TimeID" runat="server" TextMode="Time"></asp:TextBox>
        </p>
        <p><asp:Label ID="TutorLabelID" Text="Select tutor: " runat="server"></asp:Label> 
        <asp:DropDownList ID="TutorDDL" runat="server"></asp:DropDownList>
        </p>
        <asp:Button ID="SubmitButton" runat="server" Text="Insert" OnClick="SumbitButton_OnClick" />
    </fieldset>
</asp:Content>
