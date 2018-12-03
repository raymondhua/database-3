<%@ Page Title="View sheet music" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="SMMC.SheetMusic.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
        <legend>Select sheet music</legend>
        <asp:DropDownList ID="SheetMusicDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SheetMusicDDL_SelectedIndexChanged"></asp:DropDownList>
        <p></p>
     </fieldset>

     <fieldset>
        <legend>Information</legend>
        <table class="auto-style1">  
               <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="NameID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="NameOutputID" runat="server"></asp:Label></td>  
            </tr>  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="CopiesAllowedID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="CopiesAllowedOutputID" runat="server"></asp:Label></td>  
            </tr>  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="DistrubitedCopiesID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="DistrubitedCopiesOutputID" runat="server"></asp:Label></td>  
            </tr>  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="OrchestralID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="OrchestralLabelID" runat="server"></asp:Label></td>  
            </tr>  
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="CopiesGivenToTutorsID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="CopiesGivenToTutorsOutputID" runat="server"></asp:Label></td>  
            </tr> 
            <tr>  
                <td class="auto-style3">  
                    <asp:Label ID="CopiesGivenToStudentsID" runat="server"></asp:Label></td>  
                <td>  
                    <asp:Label ID="CopiesGivenToStudentsOuptutID" runat="server"></asp:Label></td>  
            </tr> 
        </table>
        <p></p>
    </fieldset>

    <fieldset>
        <legend>Instrument information</legend>
        <asp:GridView ID="InstrumentGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Instrument" DataField="Instrument"/>
        </Columns>
        </asp:GridView>
        <p></p>
    </fieldset>

    <fieldset>
        <legend>Tutors information</legend>
        <asp:GridView ID="TutorsGridView" runat="server" AutoGenerateColumns="false">
         <Columns>
            <asp:BoundField HeaderText="Name" DataField="Name"/>
            <asp:BoundField HeaderText="Number of given copies" DataField="GivenCopies"/>
            <asp:BoundField HeaderText="Number of copies given to students" DataField="GivenToStudents" />
            <asp:BoundField HeaderText="Copies remaining" DataField="GivenToStudents" />
        </Columns>
        </asp:GridView>
        <p></p>
    </fieldset>

</asp:Content>
