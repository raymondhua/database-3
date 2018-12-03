<%@ Page Title="View performance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="SMMC.Performances.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="NameLabel" runat="server"></asp:Label></h1>
    <fieldset>
        <legend>Select performance</legend>
        <asp:DropDownList ID="PerformanceDDL" runat="server"></asp:DropDownList>
        <asp:Button ID="SelectPerformanceButton" runat="server" Text="Select performance" OnClick="SelectPerformanceButton_Click" />
        <p></p>
     </fieldset>

    <fieldset>
        <legend>Venue info</legend>
        <table class="auto-style1">  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="NameID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="NameOutputID" runat="server"></asp:Label></td>  
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
        </table>
    </fieldset>

    <fieldset>
        <legend>Students playing</legend>
        <asp:GridView ID="PerformancesStudentGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name"/>
            <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
        </Columns>
        </asp:GridView>
        <p></p>
    </fieldset>
</asp:Content>
