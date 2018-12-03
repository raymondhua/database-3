<%@ Page Title="View all instruments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="SMMC.Instruments.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h2><%: Title %></h2>
  <fieldset>
    <legend>Instrument information</legend>
        <asp:GridView ID="InstrumentGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
          <asp:BoundField HeaderText="Instrument" DataField="Instrument" />
          <asp:BoundField HeaderText="Student fee" DataField="StudentFee"/>
          <asp:BoundField HeaderText="Open fee/year" DataField="OpenFee"/>
          <asp:BoundField HeaderText="Hire fee/year" DataField="HireFee"/>
          <asp:BoundField HeaderText="Comments" DataField="Comments"/>
        </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
