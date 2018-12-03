<%--<%@ Page Title="View tutor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="SMMC.Tutors.View" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="NameLabel" runat="server"></asp:Label></h1>
    <fieldset>
        <legend>View student</legend>
        <asp:DropDownList ID="PersonDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TutorDDL_SelectedIndexChanged"></asp:DropDownList>
        <p></p>
     </fieldset>

    <fieldset>
        <legend>Personal info</legend>
        <table class="auto-style1">  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="AddressID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="AddressOutputID" runat="server"></asp:Label></td>  
            </tr>  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="PhoneID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="PhoneOutputID" runat="server"></asp:Label></td>  
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend><asp:Label ID="InstrumentsLabel" runat="server"></asp:Label></legend>
        <asp:GridView ID="TutorTypeGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
            <asp:BoundField HeaderText="Type" DataField="Type"/>
        </Columns>
        </asp:GridView>
        <p></p>
    </fieldset>
    <fieldset>
        <legend><asp:Label ID="LessonsLabel" runat="server"></asp:Label></legend>
        <asp:GridView ID="LessonsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
            <asp:BoundField HeaderText="Level" DataField="Level"/>
            <asp:BoundField HeaderText="Time" DataField="Time" />
            <asp:BoundField HeaderText="Pay" DataField="Pay"/>
        </Columns>
        </asp:GridView>
        <p></p>
        <asp:Label ID="TotalLessonPay" runat="server"></asp:Label>
        <p></p>
    </fieldset>
</asp:Content>
