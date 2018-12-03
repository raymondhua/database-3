<%@ Page Title="Insert person's certifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertCertifications.aspx.cs" Inherits="SMMC.People.InsertCertifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Insert info here</legend>
        <b><asp:Label ID="SuccessLabelID" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Select person: " ID="firstNameLabelId"></asp:Label>
            <asp:DropDownList ID="PersonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PersonDDL_SelectedIndexChanged"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Select instrument: " ID="lastNameLabelId"></asp:Label>
            <asp:DropDownList ID="InstrumentDDL" runat="server"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Certification level: " ID="dobLabelID"></asp:Label>
            <asp:DropDownList ID="CLDDL" runat="server"></asp:DropDownList>
        </p>

        <p><asp:Label runat="server" Text="Are you have an ATCL qualification: " ID="ATCLLabelID"></asp:Label>
        <asp:RadioButtonList ID="ATCL" runat="server">

            <asp:ListItem Text ="Yes" Value="True" />
            <asp:ListItem Text ="No" Value="False" />

        </asp:RadioButtonList>
        </p>

        <p><asp:Button ID="SubmitButton" runat="server" Text="Insert" OnClick="SubmitButton_Click" /></p>
    </fieldset>
</asp:Content>
