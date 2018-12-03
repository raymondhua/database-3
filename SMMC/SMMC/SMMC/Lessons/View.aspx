<%@ Page Title="View lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="SMMC.Lessons.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Select lesson</legend>
        <asp:DropDownList ID="LessonDDL" runat="server"></asp:DropDownList>
        <asp:Button ID="SelectLessonButton" runat="server" Text="Select lesson" OnClick="SelectLessonButton_Click" />
        <p></p>
    </fieldset>
    <fieldset>
    <legend>Lesson info</legend>
    <table class="auto-style1">  
        <tr>  
            <td class="auto-style3">  
                <asp:Label ID="InstrumentID" runat="server"></asp:Label></td>  
            <td>  
                <asp:Label ID="InstrumentOutputID" runat="server"></asp:Label></td>  
        </tr>  
        <tr>  
            <td class="auto-style3">  
                <asp:Label ID="LevelID" runat="server"></asp:Label></td>  
            <td>  
                <asp:Label ID="LevelOutputID" runat="server"></asp:Label></td>  
        </tr>  
        <tr>  
            <td class="auto-style3">  
                <asp:Label ID="TimeID" runat="server"></asp:Label></td>  
            <td>  
                <asp:Label ID="TimeOutputID" runat="server"></asp:Label></td>  
        </tr>  
    </table>
    <p></p>
  </fieldset>
  <fieldset>
    <legend>Tutor information</legend>
        <asp:GridView ID="TutorsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
          <asp:BoundField HeaderText="Name" DataField="Name" />
        </Columns>
        </asp:GridView>
    </fieldset>
  <fieldset>
    <legend>Student information</legend>
        <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
          <asp:BoundField HeaderText="Name" DataField="Name" />
          <asp:BoundField HeaderText="Open division" DataField="OpenDivision"/>
        </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
