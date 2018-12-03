<%@ Page Title="View all tutors in lessons" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllTutors.aspx.cs" Inherits="SMMC.Lessons.ViewAllTutors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <h2><%: Title %></h2>
  <fieldset>
    <legend>All students</legend>
        <asp:GridView ID="AllStudentsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
          <asp:BoundField HeaderText="Name" DataField="Name" />
          <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
          <asp:BoundField HeaderText="Level" DataField="Level"/>
          <asp:BoundField HeaderText="Time" DataField="Time" />
        </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
