<%@ Page Title="Update sheet music" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="SMMC.SheetMusic.Update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Select</legend>
        <p><asp:Label runat="server" ID="SuccessLabelID"></asp:Label></p>
        <p><asp:Label runat="server" Text="Select: " ID="SheetMusicLabelID"></asp:Label>
            <asp:DropDownList ID="SheetMusicDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SheetMusicDDL_SelectedIndexChanged"></asp:DropDownList>
        </p>
        <p><asp:Label runat="server" Text="Copies allowed: " ID="CopiesAllowedLabelID"></asp:Label>
            <asp:TextBox ID="CopiesAllowedID" runat="server"></asp:TextBox>
        </p>
        <p><asp:Label runat="server" Text="Distrubited copies: " ID="DistrubitedLabelID"></asp:Label>
            <asp:TextBox ID="DistrubitedCopiesID" runat="server"></asp:TextBox>
        </p>
    </fieldset>
    <asp:Button ID="SubmitButton" runat="server" Text="Update" OnClick="SumbitButton_OnClick" />
</asp:Content>
