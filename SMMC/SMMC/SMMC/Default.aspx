<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SMMC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Saturday Morning Music Classes</h1>
        <p class="lead">Welcome to Saturday Morning Music Classes.</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Students</h2>
            <p>
                <asp:Label ID="Students" runat="server" Text="Label"></asp:Label>
            </p>
            <p>
                <a class="btn btn-default" runat="server" href="~/Students/Insert">Enrol new student &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Tutors</h2>
            <p>
                <asp:Label ID="Tutors" runat="server" Text="Label"></asp:Label>
            </p>
            <p>
                <a class="btn btn-default" runat="server" href="~/Tutors/View">View a tutor &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Lessons</h2>
            <p>
                <asp:Label ID="Lessons" runat="server" Text="Label"></asp:Label>
            </p>
            <p>
                <a class="btn btn-default" runat="server" href="~/Lessons/Insert">Add a lesson &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
