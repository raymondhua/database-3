<%@ Page Title="View student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="SMMC.Students.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="NameLabel" runat="server"></asp:Label></h1>
    <fieldset>
        <legend>Select student</legend>
        <asp:DropDownList ID="PersonDDL" runat="server"></asp:DropDownList>
        <asp:Button ID="SelectPersonButton" runat="server" Text="Select student(s)" OnClick="SelectPersonButton_Click" />
        <p></p>
     </fieldset>

    <fieldset>
        <legend>Personal info</legend>
        <table class="auto-style1">  
               <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="BirthdayID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="BirthdayOutputID" runat="server"></asp:Label></td>  
            </tr>  
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
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="OpenDivID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="OpenDivOutputID" runat="server"></asp:Label></td>  
            </tr>  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="TotalID" runat="server"></asp:Label></td>  
                <td>  
        </table>
        <p></p>
    </fieldset>

    <fieldset>
        <legend><asp:Label ID="InstrumentsLabel" runat="server"></asp:Label></legend>
        <asp:GridView ID="StudentInstrumentsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
            <asp:BoundField HeaderText="Hire" DataField="Hire"/>
            <asp:BoundField HeaderText="Student fee" DataField="StudentFee" />
            <asp:BoundField HeaderText="Hire fee" DataField="HireFee"/>
        </Columns>
        </asp:GridView>
        <p></p>
        <asp:Label ID="TotalStudentFee" runat="server"></asp:Label>
        <p></p>
        <asp:Label ID="TotalHireFee" runat="server"></asp:Label>
        <p></p>
        <asp:Label ID="TotalFee" runat="server"></asp:Label>
        <p></p>

    </fieldset>

    <fieldset>
    <legend><asp:Label ID="PaymentLabel" runat="server"></asp:Label></legend>
        <asp:GridView ID="PaymentHistory" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Date paid" DataField="DatePaid" />
            <asp:BoundField HeaderText="Amount paid" DataField="Amount" />
        </Columns>
        </asp:GridView>
    </fieldset>

    <fieldset>
    <legend><asp:Label ID="PerformancesLabel" runat="server"></asp:Label></legend>
        <asp:GridView ID="PerformancesGV" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Date" DataField="Date" />
            <asp:BoundField HeaderText="Time" DataField="Time" />
            <asp:BoundField HeaderText="Venue" DataField="Venue" />
            <asp:BoundField HeaderText="Instrument" DataField="Instrument" />
        </Columns>
        </asp:GridView>
        <p></p>
    </fieldset>

    <fieldset>
    <legend><asp:Label ID="ParentsLabel" runat="server"></asp:Label></legend>
        <asp:GridView ID="ParentsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Phone" DataField="Phone" />
        </Columns>
        </asp:GridView>
    </fieldset>

</asp:Content>
