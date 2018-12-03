<%@ Page Title="Insert sheet music" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.SheetMusic.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Insert info here</legend>
        <p><asp:Label runat="server" ID="SuccessLabelID"></asp:Label></p>
         <p><asp:Label runat="server" Text="Name: " ID="NameLabelID"></asp:Label>
            <asp:TextBox ID="NameID" runat="server"></asp:TextBox>
        </p>
        <p><asp:Label runat="server" Text="Copies allowed: " ID="CopiesAllowedLabelID"></asp:Label>
            <asp:TextBox ID="CopiesAllowedID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Distrubited copies: " ID="DistrubitedLabelID"></asp:Label>
            <asp:TextBox ID="DistrubitedCopiesID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Is the sheet music orchestral: " ID="opLabelID"></asp:Label>
        <asp:RadioButtonList ID="OrchestralRBL" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="OrchestralRBL_SelectedIndexChanged">

            <asp:ListItem Text ="Yes" Value="True" />
            <asp:ListItem Text ="No" Value="False" />

        </asp:RadioButtonList>
        </p>

        <p><asp:Label runat="server" Text="Select an instrument: " ID="InstrumentID"></asp:Label>
        <asp:DropDownList ID="InstrumentDDL" runat="server"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Select an instrument(s) below: " ID="InstrumentsID"></asp:Label>
        <asp:CheckBoxList ID="InstrumentCBL" runat="server"></asp:CheckBoxList>
        </p>

    </fieldset>
    <asp:Button ID="SubmitButton" runat="server" Text="Insert" OnClick="SumbitButton_OnClick" />
</asp:Content>
