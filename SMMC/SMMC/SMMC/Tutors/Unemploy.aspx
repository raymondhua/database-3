<%@ Page Title="Remove tutor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unemploy.aspx.cs" Inherits="SMMC.Tutors.Unemploy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend></legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>

        <p><asp:Label runat="server" Text="Select tutor: " ID="InstrumentLabelID"></asp:Label>
        <asp:DropDownList ID="TutorDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TutorDDL_SelectedIndexChanged"></asp:DropDownList></p>

        <p><asp:Label runat="server" Text="Select instrument: " ID="Label1"></asp:Label>
        <asp:DropDownList ID="InstrumentDDL" runat="server"></asp:DropDownList></p>
        <p><asp:Button ID="ButtonId" runat="server" Text="Delete" OnClick="SubmitButton_Click" /></p>
    </fieldset>
</asp:Content>
