<%@ Page Title="Update instrument" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="SMMC.Instruments.Update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
    <legend>Instruments</legend>
    <b><asp:Label ID="SuccessLabel" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Select an instrument: " ID="instrumentLabelID"></asp:Label> <asp:DropDownList ID="InstrumentDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="InstrumentDDL_SelectedIndexChanged"></asp:DropDownList></p>
        <p><asp:Label runat="server" Text="Student fee: " ID="studentFeeLabelId"></asp:Label>
            <asp:TextBox ID="StudentFeeID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Open fee: " ID="openFeeLabelID"></asp:Label>
            <asp:TextBox ID="OpenFeeID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Hire fee: " ID="hireFeeLabelID"></asp:Label>
            <asp:TextBox ID="HireFeeID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Comments (optional): " ID="commentsLabelID"></asp:Label>
            <asp:TextBox ID="CommentsID" runat="server"></asp:TextBox>
        </p>
    <asp:Button ID="Button1" runat="server" Text="Update instrument" OnClick="Button1_Click"/>
    </fieldset>
</asp:Content>
