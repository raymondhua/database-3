<%@ Page Title="Enrol student into instrument" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertInstrument.aspx.cs" Inherits="SMMC.Students.InsertInstrument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Insert here</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p><asp:Label ID="Label1" runat="server" Text="Student: "></asp:Label>  
        <asp:DropDownList ID="PersonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectStudentButton_Click"></asp:DropDownList></p>
        <p>
        <asp:Label ID="InstrumentLabel1" runat="server" Text="Instrument: "></asp:Label>  
        <asp:DropDownList ID="InstrumentDDL" runat="server"></asp:DropDownList></p>
        <p><asp:Label ID="Hire" runat="server" Text="Hire: "></asp:Label>
        <asp:DropDownList ID="HireDDL" runat="server">
            <asp:ListItem Text ="Yes" Value="True" />
            <asp:ListItem Text ="No" Value="False" />
        </asp:DropDownList></p>
        <asp:Button ID="DeleteInstrumentsButton" runat="server" Text="Insert instruments(s)" OnClick="DeleteInstrumentsButton_Click" />
    </fieldset>
</asp:Content>
