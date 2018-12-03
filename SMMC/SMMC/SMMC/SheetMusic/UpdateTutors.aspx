<%@ Page Title="Update tutor's sheet music" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateTutors.aspx.cs" Inherits="SMMC.SheetMusic.UpdateTutors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <fieldset>
       <legend>Insert info here</legend>
       <p><asp:Label runat="server" ID="SuccessLabelID"></asp:Label></p>
       <p><asp:Label runat="server" Text="Select sheet music: " ID="SheetMusicLabelID"></asp:Label>
            <asp:DropDownList ID="SheetMusicDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SheetMusicDDL_SelectedIndexChanged"></asp:DropDownList>
       </p>
       <p><asp:Label runat="server" Text="Select tutor: " ID="Label1"></asp:Label>
            <asp:DropDownList ID="TutorDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TutorDDL_SelectedIndexChanged"></asp:DropDownList>
       </p>
       <p><asp:Label runat="server" Text="" ID="DistrubitedCopies"></asp:Label></p>
       <p><asp:Label runat="server" Text="Given copies: " ID="copiesID1"></asp:Label>
           <asp:TextBox ID="GivenCopiesID" runat="server"></asp:TextBox>
       </p>

       <p><asp:Label runat="server" Text="Copies given to Students: " ID="copiesID2"></asp:Label>
           <asp:TextBox ID="GivenToStudentsID" runat="server"></asp:TextBox>
       </p>
    </fieldset>
    <asp:Button ID="SubmitButton" runat="server" Text="Update" OnClick="SumbitButton_OnClick" />
</asp:Content>
