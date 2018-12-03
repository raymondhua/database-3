<%@ Page Title="Suggested pay" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="SMMC.Tutors.Pay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <fieldset>
        <legend>Suggested pay</legend>
        <asp:DropDownList ID="TutorDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TutorDDL_SelectedIndexChanged"></asp:DropDownList>
        <p></p>
        <asp:GridView ID="PayGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name"/>
            <asp:BoundField HeaderText="Phone no" DataField="Phone"/>
            <asp:BoundField HeaderText="Total pay per week" DataField="TotalPay" />
            <asp:BoundField HeaderText="Hours worked" DataField="HoursWorked"/>
        </Columns>
        </asp:GridView>
        <p></p>
    </fieldset>
</asp:Content>
