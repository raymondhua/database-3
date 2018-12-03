<%@ Page Title="View all students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAll.aspx.cs" Inherits="SMMC.Students.ViewAll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
  <fieldset>
    <legend>All students</legend>
        <asp:GridView ID="AllStudentsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
          <asp:BoundField HeaderText="First name" DataField="FirstName" />
          <asp:BoundField HeaderText="Last name" DataField="LastName"/>
          <asp:BoundField HeaderText="Date of birth" DataField="DateOfBirth"/>
          <asp:BoundField HeaderText="Phone number" DataField="PhoneNo"/>
          <asp:BoundField HeaderText="Address" DataField="Street" />
          <asp:BoundField HeaderText="Suburb" DataField="Suburb"/>
          <asp:BoundField HeaderText="City" DataField="City"/>
          <asp:BoundField HeaderText="Postcode" DataField="Postcode"/>
        </Columns>
        </asp:GridView>
    </fieldset>
    <p></p>
    <fieldset>
    <legend>Instrument information</legend>
        <asp:GridView ID="StudentInstrumentsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Open division" DataField="OpenDivision" />
            <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
            <asp:BoundField HeaderText="Hire" DataField="Hire"/>
            <asp:BoundField HeaderText="Student fee" DataField="StudentFee" />
            <asp:BoundField HeaderText="Hire fee" DataField="HireFee"/>
            <asp:BoundField HeaderText="Level" DataField="CertificationLevel" />
            <asp:BoundField HeaderText="ATCL" DataField="ATCL"/>
            <asp:BoundField HeaderText="Type of orchestra" DataField="Type"/>
        </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
