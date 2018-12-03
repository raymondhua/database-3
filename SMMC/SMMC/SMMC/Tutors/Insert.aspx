<%@ Page Title="Add tutor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="SMMC.Tutors.Insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Add info here (if you want to create a new person <a runat="server" href="~/People/Insert">click here</a>)</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Select person: " ID="personLabelID"></asp:Label>
            <asp:DropDownList ID="PersonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PersonDDL_SelectedIndexChanged"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Select instrument: " ID="InstrumentLabelID"></asp:Label>
            <asp:DropDownList ID="InstrumentDDL" runat="server"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Select type: " ID="Label1"></asp:Label>
            <asp:DropDownList ID="EnsemblesDDL" runat="server"></asp:DropDownList>
        </p>

    </fieldset>

    <p><asp:Button ID="ButtonId" runat="server" Text="Insert" OnClick="SubmitButton_Click" /></p>
</asp:Content>
