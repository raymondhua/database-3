<%@ Page Title="Make payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayBill.aspx.cs" Inherits="SMMC.Students.PayBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <h2><%: Title %></h2>
       <p><asp:Label runat="server" ID="SuccessLabelID"></asp:Label></p>
       <p><asp:Label runat="server" Text="Select student: " ID="Label1"></asp:Label>
            <asp:DropDownList ID="StudentDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="StudentDDL_SelectedIndexChanged"></asp:DropDownList>
       </p>
       <p><asp:Label runat="server" Text="Amount: " ID="hoursWorkedLabelID"></asp:Label>
           <asp:TextBox ID="AmountID" runat="server"></asp:TextBox>
       </p>
       <p><asp:Label runat="server" Text="Date: " ID="DatePaidLabel"></asp:Label>
           <asp:TextBox ID="DatePaidID" runat="server" TextMode="Date"></asp:TextBox>
       </p>
    </fieldset>
    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SumbitButton_OnClick" />

</asp:Content>
