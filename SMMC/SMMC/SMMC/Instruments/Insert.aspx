<%@ Page Title="Add instrument" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.Instruments.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add instrument</h2>
    <fieldset>
    <legend>Insert info here</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Instrument: " ID="instrumentLabelID"></asp:Label>
            <asp:TextBox ID="InstrumentID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Student fee: " ID="studentFeeLabelId"></asp:Label>
            <asp:TextBox ID="StudentFeeID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Open fee: " ID="openFeeLabelID"></asp:Label>
            <asp:TextBox ID="OpenFeeID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Hire fee: " ID="hireFeeLabelID"></asp:Label>
            <asp:TextBox ID="HireFeeID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Comments (optional): " ID="Label10"></asp:Label>
            <asp:TextBox ID="CommentsID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Button ID="ButtonId" runat="server" Text="Insert instrument" OnClick="ButtonId_Click" /></p>
    </fieldset>
</asp:Content>
