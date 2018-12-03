<%@ Page Title="Update person's certifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCertifications.aspx.cs" Inherits="SMMC.People.UpdateCertifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
    <legend>Update</legend>
    <asp:DropDownList ID="PersonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PersonDDL_SelectedIndexChanged"></asp:DropDownList>
    <asp:DropDownList ID="InstrumentDDL" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="InstrumentDDL_SelectedIndexChanged"></asp:DropDownList>
    <p></p>
    </fieldset>
    <fieldset>
        <legend>Information</legend>
        <b><asp:Label ID="SuccessLabel" Text="" runat="server"></asp:Label></b>
        <p><asp:Label runat="server" Text="Level: " ID="levelLabelId"></asp:Label>
            <asp:TextBox ID="CertificationLevelID" runat="server"></asp:TextBox>
        </p>

        <p><asp:Label runat="server" Text="Are your qualifications ATCL: " ID="opLabelID"></asp:Label>
        <asp:RadioButtonList ID="ATCL" runat="server">

            <asp:ListItem Text ="Yes" Value="True" />
            <asp:ListItem Text ="No" Value="False" />

        </asp:RadioButtonList>
        </p>
    <asp:Button ID="SubmitButton" runat="server" Text="Update" OnClick="SubmitButton_Click" />
    </fieldset>
</asp:Content>
