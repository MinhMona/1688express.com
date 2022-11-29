<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang.aspx.cs" Inherits="NHST.chi_tiet_don_hang1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        dl dt {
            float: left;
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
            color: #3e3e3e;
            margin-right: 5px;
        }

        .full-width {
            width: 100% !important;
        }

        .order-panels {
            float: left;
            width: 100%;
            display: -ms-flexbox;
            display: -webkit-box;
            display: flex;
            -ms-flex-direction: row;
            -webkit-box-orient: horizontal;
            -webkit-box-direction: normal;
            flex-direction: row;
            -ms-flex-wrap: nowrap;
            flex-wrap: nowrap;
            -ms-flex-pack: justify;
            -webkit-box-pack: justify;
            justify-content: space-between;
            -ms-flex-line-pack: stretch;
            align-content: stretch;
            -ms-flex-align: stretch;
            -webkit-box-align: stretch;
            align-items: stretch;
        }

        @media (max-width: 768px) {
            .order-panels {
                -ms-flex-direction: column;
                -webkit-box-orient: vertical;
                -webkit-box-direction: normal;
                flex-direction: column;
            }
        }

        .order-panel {
            float: left;
            width: 100%;
            margin-bottom: 30px;
            padding: 10px;
            line-height: 1.6;
            box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.2);
        }

            .order-panel .title {
                text-transform: uppercase;
                color: #2b2e4a;
                font-weight: bold;
                font-size: 16px;
                padding-bottom: 5px;
                border-bottom: solid 1px #fff;
                margin-bottom: 10px;
            }

            .order-panel .cont {
                display: block;
                width: 100%;
            }

            .order-panel .bottom {
                border-top: solid 1px #fff;
                padding-top: 10px;
                margin-top: 10px;
                text-align: right;
            }

                .order-panel .bottom .btn {
                    text-transform: uppercase;
                    font-weight: bold;
                }

            .order-panel dl {
                float: none;
            }

                .order-panel dl dt {
                    width: 60%;
                }

            .order-panel textarea {
                width: 100%;
                height: 60px;
            }

            .order-panel table.ratting-tb {
                width: 100%;
            }

                .order-panel table.ratting-tb th {
                    height: 40px;
                    font-weight: bold;
                    border-bottom: solid 2px #ebebeb;
                    vertical-align: middle;
                }

                .order-panel table.ratting-tb td {
                    height: 40px;
                    vertical-align: middle;
                }

            .order-panel table.tb-product {
                width: 100%;
            }

                .order-panel table.tb-product caption {
                    color: #ff7e67;
                    text-transform: uppercase;
                    font-size: 16px;
                    text-align: left;
                    line-height: 50px;
                    font-weight: bold;
                }

                .order-panel table.tb-product th {
                    vertical-align: middle;
                    background-color: #f8f8f8;
                }

                .order-panel table.tb-product td {
                    vertical-align: top;
                }

                .order-panel table.tb-product td, .order-panel table.tb-product th {
                    padding: 10px;
                    border: solid 1px #ebebeb;
                }

                .order-panel table.tb-product .qty input {
                    display: block;
                    margin: 0 auto;
                    text-align: center;
                }

            .order-panel .table-wrap + .table-wrap {
                margin-top: 20px;
            }

            .order-panel .table-wrap {
                width: 100%;
                overflow: auto;
            }

                .order-panel .table-wrap table {
                    min-width: 600px;
                }

            .order-panel + .order-panel {
                margin-left: 30px;
            }

            .order-panel.greypn {
                background-color: #eeeeee;
            }

        @media (max-width: 768px) {
            .order-panel + .order-panel {
                margin-left: 0;
            }
        }

        @media (max-width: 480px) {
            body {
                overflow-x: hidden;
            }

            table {
                width: 600px !important;
            }

            .tbl-product-wrap, .table-cont-overflow {
                width: 100%;
                overflow: auto;
            }
        }

        .total-order-block {
            float: left;
            width: 100%;
        }

            .total-order-block .order-panel {
                width: calc(50% - 15px);
                margin-right: 30px;
                padding: 20px 30px;
            }

                .total-order-block .order-panel + .order-panel {
                    margin-right: 0;
                    margin-left: 0;
                }

                .total-order-block .order-panel dl dt {
                    text-align: right;
                    float: right;
                    color: inherit;
                    font-weight: bold;
                }

                .total-order-block .order-panel dl dd {
                    text-align: right;
                }

        @media (max-width: 768px) {
            .total-order-block .order-panel {
                width: 100%;
            }
        }

        dl dd {
            display: block;
            padding-left: 100px;
        }

            .clear:before, dl dd:before, .thumb-blog-ul li:before, .primary-form:before, .panel .panel-heading:before, .panel .panel-body:before {
                display: table;
                content: " ";
            }

            .clear:after, dl dd:after, .thumb-blog-ul li:after, .primary-form:after, .panel .panel-heading:after, .panel .panel-body:after {
                content: "";
                display: table;
                clear: both;
            }

        .title-fee {
            float: left;
            width: 100%;
            border-bottom: solid 1px #ccc;
            font-size: 20px;
            margin: 20px 0;
            color: #000;
        }

        .brand-name-product {
            float: left;
            width: 100%;
            margin: 10px 0 40px 0;
        }

            .brand-name-product input {
                float: left;
                width: 100%;
            }

        .table-price-sec .tbp-top {
            float: left;
            width: 100%;
            margin-bottom: 30px;
        }

        .table-panel .table-panel-header {
            float: left;
            width: 100%;
            color: white;
            background-color: #ff7e67;
            padding: 25px;
        }

            .table-panel .table-panel-header .title {
                text-transform: uppercase;
                float: left;
                line-height: 20px;
                padding: 5px 0;
            }

            .table-panel .table-panel-header .delivery-opt {
                float: right;
            }

                .table-panel .table-panel-header .delivery-opt label {
                    float: left;
                    cursor: pointer;
                    margin-right: 50px;
                }

                    .table-panel .table-panel-header .delivery-opt label:last-child {
                        margin-right: 0;
                    }

                    .table-panel .table-panel-header .delivery-opt label input[type="checkbox"] {
                        display: none;
                    }

                    .table-panel .table-panel-header .delivery-opt label span {
                        display: inline-block;
                        vertical-align: middle;
                    }

                    .table-panel .table-panel-header .delivery-opt label .ip-avata {
                        border-radius: 50%;
                        -webkit-border-radius: 50%;
                        width: 30px;
                        height: 30px;
                        margin-right: 20px;
                        background-color: white;
                        position: relative;
                    }

                        .table-panel .table-panel-header .delivery-opt label .ip-avata:before {
                            content: '';
                            display: block;
                            margin: 0 auto;
                            -webkit-transition: all .3s ease-in-out;
                            transition: all .3s ease-in-out;
                            width: 20px;
                            height: 20px;
                            margin-top: 5px;
                            border-radius: 50%;
                            -webkit-border-radius: 50%;
                            background-color: #ff7e67;
                            transform: scale(0);
                            -webkit-transform: scale(0);
                            -moz-transform: scale(0);
                            -ms-transform: scale(0);
                            -o-transform: scale(0);
                        }

                    .table-panel .table-panel-header .delivery-opt label input[type="checkbox"]:checked + .ip-avata:before {
                        transform: none;
                        -webkit-transform: none;
                        -moz-transform: none;
                        -ms-transform: none;
                        -o-transform: none;
                    }

        .table-panel .table-panel-total {
            float: left;
            width: 33.3333333333333%;
            padding: 0 30px;
        }

            .table-panel .table-panel-total table {
                width: 100%;
            }

                .table-panel .table-panel-total table tr {
                    border-bottom: solid 1px #ebebeb;
                }

                    .table-panel .table-panel-total table tr:last-child {
                        border-bottom: none;
                    }

                .table-panel .table-panel-total table td {
                    height: 80px;
                    vertical-align: middle;
                }

                    .table-panel .table-panel-total table td:last-child {
                        text-align: right;
                        font-size: 18px;
                    }

            .table-panel .table-panel-total .note-block {
                float: left;
                width: 100%;
            }

                .table-panel .table-panel-total .note-block textarea.note {
                    float: left;
                    width: 100%;
                    resize: none;
                    height: 90px;
                    padding: 15px 20px;
                }

            .table-panel .table-panel-total .btn-wrap {
                float: left;
                width: 100%;
                padding-top: 30px;
            }

                .table-panel .table-panel-total .btn-wrap .btn {
                    display: block;
                    font-weight: bold;
                    width: 100%;
                    text-align: center;
                    font-size: 12px;
                }

        @media (max-width: 480px) {
            .table-panel .table-panel-header {
                padding: 15px;
            }

            .table-panel .delivery-opt label {
                margin-bottom: 10px;
            }

            .table-panel .table-panel-main {
                width: 100%;
                overflow: auto;
            }

                .table-panel .table-panel-main table {
                    width: 600px;
                }

            .table-panel .table-panel-total {
                width: 100%;
                padding: 0;
            }
        }

        .table-panel-main {
            float: left;
            width: 66.66666666666666%;
        }

            .table-panel-main table {
                float: left;
                width: 100%;
                position: relative;
                border: solid 1px #ebebeb;
                border-top: none;
            }

                .table-panel-main table th {
                    height: 80px;
                    vertical-align: middle;
                    background-color: #f8f8f8;
                    font-weight: bold;
                    padding: 0 25px;
                }

                .table-panel-main table td {
                    padding: 20px 0;
                    vertical-align: middle;
                }

                    .table-panel-main table td .checklb .ip-avata {
                        border-color: #ececec;
                    }

                    .table-panel-main table td.hover-td {
                        padding: 0;
                    }

                .table-panel-main table .checklb .ip-avata {
                    margin: 0;
                }

                .table-panel-main table .check {
                    padding: 0;
                    padding-left: 25px;
                }

                .table-panel-main table .img {
                    padding: 0 15px;
                }

                .table-panel-main table .qty {
                    padding: 0 15px;
                }

                    .table-panel-main table .qty input {
                        width: 70px;
                    }

                .table-panel-main table .price {
                    padding: 0 15px;
                }

                    .table-panel-main table .price p:last-child {
                        color: #777777;
                    }

                .table-panel-main table .total {
                    padding-right: 25px;
                }

                    .table-panel-main table .total p:last-child {
                        color: #777777;
                    }

                .table-panel-main table textarea.note {
                    width: 100%;
                    resize: none;
                    height: 90px;
                    padding: 15px 20px;
                }

                .table-panel-main table .hover-block {
                    display: none;
                }

                .table-panel-main table .note-td {
                    padding-right: 25px;
                    padding-left: 25px;
                    border-bottom: solid 1px #ebebeb;
                }

                .table-panel-main table .hover-tr {
                    display: none;
                }

                .table-panel-main table:hover {
                    border-color: #ff7e67;
                }

                    .table-panel-main table:hover .hover-tr {
                        display: table-row;
                    }

                    .table-panel-main table:hover .hover-block {
                        position: absolute;
                        top: 50%;
                        right: -15px;
                        display: block;
                        font-size: 18px;
                        line-height: 30px;
                        width: 30px;
                        height: 30px;
                        text-align: center;
                        margin-top: -15px;
                        border: solid 1px #ebebeb;
                        background-color: white;
                    }

                        .table-panel-main table:hover .hover-block a {
                            display: block;
                        }

        .table-price-total {
            float: left;
            width: 100%;
            padding: 30px 25px;
            margin-top: 30px;
            border-top: solid 1px #ebebeb;
        }

            .table-price-total .order-btn {
                padding-right: 50px;
                padding-left: 50px;
                display: inline-block;
                vertical-align: middle;
                font-weight: bold;
                font-size: 12px;
            }

            .table-price-total .final-total {
                display: inline-block;
                vertical-align: middle;
                margin-right: 25px;
            }

                .table-price-total .final-total strong.hl-txt {
                    font-size: 30px;
                }

        .thumb-product {
            display: table;
            width: 100%;
        }

            .thumb-product .pd-img {
                float: none;
                display: table-cell;
                vertical-align: middle;
                width: 70px;
                margin-right: 20px;
                position: relative;
            }

                .thumb-product .pd-img .badge {
                    position: absolute;
                    right: 0;
                    top: 0;
                    z-index: 1;
                    width: 20px;
                    text-align: center;
                    height: 20px;
                    line-height: 20px;
                    background-color: #959595;
                    color: white;
                    font-weight: bold;
                    border-radius: 50%;
                    margin-right: -10px;
                    margin-top: -10px;
                }

            .thumb-product .info {
                float: none;
                display: table-cell;
                vertical-align: middle;
            }

        .checklb {
            cursor: pointer;
            float: left;
        }

            .checklb input {
                display: none;
            }

            .checklb span {
                display: inline-block;
                vertical-align: middle;
            }

            .checklb .ip-avata {
                border-radius: 50%;
                -webkit-border-radius: 50%;
                width: 30px;
                height: 30px;
                border: solid 1px #f8f8f8;
                margin-right: 10px;
                background-color: white;
                position: relative;
            }

                .checklb .ip-avata:before {
                    content: '';
                    display: block;
                    margin: 0 auto;
                    -webkit-transition: all .3s ease-in-out;
                    transition: all .3s ease-in-out;
                    width: 20px;
                    height: 20px;
                    margin-top: 4px;
                    border-radius: 50%;
                    -webkit-border-radius: 50%;
                    background-color: #ff7e67;
                    transform: scale(0);
                    -webkit-transform: scale(0);
                    -moz-transform: scale(0);
                    -ms-transform: scale(0);
                    -o-transform: scale(0);
                }

            .checklb input:checked + .ip-avata:before {
                transform: none;
                -webkit-transform: none;
                -moz-transform: none;
                -ms-transform: none;
                -o-transform: none;
            }

            .checklb + .checklb {
                margin-left: 20px;
            }

        .radiolb {
            cursor: pointer;
            float: left;
        }

            .radiolb input {
                display: none;
            }

            .radiolb span {
                display: inline-block;
                vertical-align: middle;
            }

            .radiolb .ip-avata {
                border-radius: 0;
                -webkit-border-radius: 0;
                width: 20px;
                height: 20px;
                border: solid 1px #ebebeb;
                margin-right: 10px;
                background-color: white;
                position: relative;
            }

                .radiolb .ip-avata:before {
                    content: '';
                    display: block;
                    margin: 0 auto;
                    -webkit-transition: all .3s ease-in-out;
                    transition: all .3s ease-in-out;
                    width: 10px;
                    height: 10px;
                    margin-top: 4px;
                    border-radius: 0;
                    -webkit-border-radius: 0;
                    background-color: #ff7e67;
                    transform: scale(0);
                    -webkit-transform: scale(0);
                    -moz-transform: scale(0);
                    -ms-transform: scale(0);
                    -o-transform: scale(0);
                }

            .radiolb input:checked + .ip-avata:before {
                transform: none;
                -webkit-transform: none;
                -moz-transform: none;
                -ms-transform: none;
                -o-transform: none;
            }

            .radiolb + .radiolb {
                margin-left: 20px;
            }

        .dropdown {
            position: relative;
        }

        .left {
            float: left;
        }

        .right {
            float: right;
        }

        .table-price-sec .tbp-top .left {
            line-height: 20px;
            padding: 10px 0;
        }

        .hl-txt {
            color: #ff7e67;
        }

        input[type=checkbox] {
            height: auto;
            width: auto;
        }

        .shop-info {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #2772db;
            color: #959595;
            font-weight: bold;
            padding: 0 15px;
            color: #fff;
            float: left;
        }

        .font-size-20 {
            font-size: 16px;
        }

        .tbl-subtotal tr {
            border-bottom: 1px dashed #999;
            width: 100%;
            float: left;
            padding-bottom: 5px;
        }

        .float-right {
            float: right;
        }

        .float-left {
            float: left;
        }

        .color-orange {
            color: orange;
        }

        b, strong, .b {
            font-weight: bold;
        }

        .comment {
            float: left;
            width: 100%;
        }

        .green {
            color: green;
        }

        .comment_content {
            min-height: 0px;
            text-align: left;
            vertical-align: top;
            border: 1px solid #E3E3E3;
            background: #FFFFF0;
            color: #666666;
            padding: 10px;
            max-height: 300px;
            overflow-y: scroll;
        }

        .user-comment {
            color: black;
            font-weight: bold;
        }

        .font-size-10 {
            font-size: 10px;
        }

        .comment-text {
            float: left;
            width: 85%;
            padding: 5px 10px;
        }

        input, select {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        #sendnotecomment {
            padding: 0px 25px;
            float: right;
            line-height: 40px;
        }

        .shop-info {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #2772db;
            color: #959595;
            font-weight: bold;
            padding: 0 15px;
            color: #fff;
            float: left;
        }

        .font-size-20 {
            font-size: 16px;
        }

        .tbl-subtotal tr {
            border-bottom: 1px dashed #999;
            width: 100%;
            float: left;
            padding-bottom: 5px;
        }

        .float-right {
            float: right;
        }

        .float-left {
            float: left;
        }

        .color-orange {
            color: orange;
        }

        b, strong, .b {
            font-weight: bold;
        }

        .comment {
            float: left;
            width: 100%;
        }

        .green {
            color: green;
        }

        .comment_content {
            min-height: 0px;
            text-align: left;
            vertical-align: top;
            border: 1px solid #E3E3E3;
            background: #FFFFF0;
            color: #666666;
            padding: 10px;
            max-height: 300px;
            overflow-y: scroll;
        }

        .user-comment {
            color: black;
            font-weight: bold;
        }

        .font-size-10 {
            font-size: 10px;
        }

        .comment-text {
            float: left;
            width: 85%;
            padding: 5px 10px;
        }

        input, select {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        #sendnotecomment {
            padding: 0px 25px;
            float: right;
        }

        .custom-link {
            float: left;
            width: 100%;
            margin-bottom: 10px;
            text-align: center;
            font-size: 90%;
            white-space: nowrap;
            /*width: 80%;
            text-overflow: ellipsis;*/
            overflow: hidden;
        }

        .chk-export {
            float: left;
            margin: 10px 10px 0 0;
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all" style="width: 100%;">
                <h4 class="sec__title center-txt">Chi tiết đơn hàng</h4>
                <div class="primary-form">
                    <div class="waitting">
                        <div class="all">
                            <div class="wait__hd">
                                <asp:Literal ID="ltrstep" runat="server"></asp:Literal>
                                <%--<article class="step active color-1">
                                    <h3 class="fz-24">01</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Chưa gửi</h4>
                                    </section>
                                </article>
                                <article class="step active color-2">
                                    <h3 class="fz-24">02</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Đang xử lý</h4>
                                    </section>
                                </article>
                                <article class="step active color-3">
                                    <h3 class="fz-24">03</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Đã báo giá</h4>
                                    </section>
                                </article>
                                <article class="step color-4">
                                    <h3 class="fz-24">04</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Đang đặt hàng</h4>
                                    </section>
                                </article>
                                <article class="step color-5">
                                    <h3 class="fz-24">05</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Đã đặt hàng</h4>
                                    </section>
                                </article>
                                <article class="step color-6">
                                    <h3 class="fz-24">06</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Thành công</h4>
                                    </section>
                                </article>
                                <article class="step active color-1">
                                    <h3 class="fz-24">07</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Chưa gửi</h4>
                                    </section>
                                </article>
                                <article class="step active color-1">
                                    <h3 class="fz-24">08</h3>
                                    <section class="status">
                                        <h4 class="fz-14">Chưa gửi</h4>
                                    </section>
                                </article>--%>
                            </div>
                        </div>
                    </div>
                    <div class="order-tool clearfix">
                        <div class="order-panels mar-bot2 color-white">
                            <asp:Label ID="ltr_info" runat="server" Visible="false" CssClass="inforshow"></asp:Label>
                        </div>
                        <div class="order-panels mar-bot2">
                            <a href="javascript:;" onclick="printDiv()" class="btn pill-btn primary-btn admin-btn main-btn hover btn-1">In đơn hàng</a>
                        </div>
                        <div class="order-panels" style="display: none">
                            <asp:Literal ID="ltr_OrderFee_UserInfo" runat="server"></asp:Literal>
                        </div>
                        <div class="order-panels">
                            <div class="order-panel">
                                <div class="title" style="text-align: center;">Thông tin người đặt</div>
                                <div class="form-row">
                                    <div class="lb">Họ tên</div>
                                    <asp:TextBox ID="txt_Fullname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Địa chỉ</div>
                                    <asp:TextBox ID="txt_Address" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Email</div>
                                    <asp:TextBox ID="txt_Email" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="lb">Số điện thoại</div>
                                    <asp:TextBox ID="txt_Phone" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="order-panel">
                                <div class="title" style="text-align: center;">Chat với chúng tôi</div>
                                <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <asp:Panel ID="pnOrderType23" runat="server">

                            <div class="order-panels">
                                <div id="danhsachkien" class="order-panel">
                                    <div class="title">Danh sách kiện hàng</div>
                                    <div class="cont clear">
                                        <div class="mobile-show-scroll">
                                            <strong>Lưu ý: </strong>trượt ngang bảng bên dưới để có thể xem hết thông tin
                                        </div>
                                        <div class="tbl-product-wrap">
                                            <asp:Literal ID="Literal1" runat="server" EnableViewState="false"></asp:Literal>
                                            <table class="tb-product">
                                                <tr>
                                                    <th class="pro">Mã đơn hàng</th>
                                                    <th class="pro">Mã vận đơn</th>
                                                    <th class="pro">Cân nặng</th>
                                                    <th class="pro">Ghi chú</th>
                                                    <th class="pro">Trạng thái</th>
                                                    <th class="price"></th>
                                                </tr>
                                                <asp:Literal ID="ltrOrderCodeList" runat="server"></asp:Literal>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="order-panel print3">
                            <div class="title">Danh sách sản phẩm</div>
                            <div class="mobile-show-scroll">
                                <strong>Lưu ý: </strong>trượt ngang bảng bên dưới để có thể xem hết thông tin
                            </div>
                            <div class="cont clear">
                                <div class="tbl-product-wrap">
                                    <asp:Literal ID="ltrshopinfo" runat="server" EnableViewState="false"></asp:Literal>
                                    <a href="javascript:;" class="btn pill-btn primary-btn main-btn hover" id="outAll"
                                        style="float: right;" onclick="requestoutstockAll()">Yêu cầu xuất kho tất cả kiện</a>
                                    <a href="javascript:;" id="exportselected" class="btn pill-btn primary-btn main-btn hover"
                                        style="float: right; margin-right: 10px; background: #21bfa2; display: none" 
                                        onclick="requestexportselect()"> Yêu cầu xuất kho các kiện đã chọn</a>
                                    <asp:Literal ID="ltrProductOrder" runat="server"></asp:Literal>
                                    <asp:Panel ID="pnOrderType23_1" runat="server">
                                        <table class="tb-product">
                                            <tr>
                                                <th class="pro">STT</th>
                                                <th class="pro">Sản phẩm</th>
                                                <th class="pro">Thuộc tính</th>
                                                <th class="qty">Số lượng</th>
                                                <th class="price">Đơn giá (¥)</th>
                                                <th class="price">Đơn giá (VNĐ)</th>
                                                <th class="price">Ghi chú riêng sản phẩm</th>
                                                <th class="price">Trạng thái</th>
                                                <%--<th class="price">Mã đơn hàng</th>
                                                <th class="price"></th>--%>
                                            </tr>
                                            <asp:Literal ID="ltrProducts" runat="server"></asp:Literal>
                                        </table>
                                    </asp:Panel>
                                    <a href="javascript:;" class="btn pill-btn primary-btn main-btn hover" id="outAll1"
                                        style="float: right; margin-bottom: 20px;" onclick="requestoutstockAll()">Yêu cầu xuất kho tất cả kiện</a>
                                    <a href="javascript:;" id="exportselected1" class="btn pill-btn primary-btn main-btn hover"
                                        style="float: right; margin-right: 10px; background: #21bfa2; display: none" onclick="requestexportselect()">Yêu cầu xuất kho các kiện đã chọn</a>
                                </div>
                                <div class="tbl-product-wrap1">
                                    <asp:Literal ID="ltrProducts1" runat="server"></asp:Literal>
                                    <span style="float: left; width: 100%; color: red; font-weight: bold; font-size: 20px; text-align: center">CƯỚC VẬN CHUYỂN VIỆT NAM – TRUNG QUỐC SẼ ĐƯỢC THỐNG KÊ SAU KHI QUÝ KHÁCH YÊU CẦU XUẤT KHO
                                    </span>
                                    <a href="javascript:;" class="btn pill-btn primary-btn main-btn hover" id="outAll1"
                                        style="float: right; margin-bottom: 20px; margin-top: 20px;" onclick="requestoutstockAll()">Yêu cầu xuất kho tất cả kiện</a>
                                </div>
                                <asp:Panel ID="pn" runat="server" Visible="false">
                                    <div class="order-panels">
                                        <asp:Literal ID="ltrCancelorder" runat="server"></asp:Literal>
                                        <%--<a class="btn pill-btn primary-btn main-btn hover" href="javascript:;" onclick="cancelOrder()">Hủy đơn hàng</a>--%>
                                        <asp:Button ID="btn_cancel" runat="server" CssClass="btn pill-btn primary-btn main-btn hover" CausesValidation="false" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
                                        <asp:Literal ID="ltrbtndeposit" runat="server"></asp:Literal>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnthanhtoan" runat="server" Visible="false">
                                    <div class="order-panels" style="display: none">
                                        <a class="btn pill-btn primary-btn main-btn hover" href="javascript:;" onclick="payallorder()">Thanh toán</a>
                                        <asp:Button ID="btnPayAll" runat="server" CssClass="btn pill-btn primary-btn main-btn hover" CausesValidation="false" Text="Thanh toán" Style="display: none;" OnClick="btnPayAll_Click" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="order-panels" style="display: none">
                            <asp:Literal ID="ltrOrderFee" runat="server"></asp:Literal>

                        </div>
                        <div class="order-panels">
                            <div class="order-panel">
                                <div class="title">Phí vận chuyển TQ-VN</div>
                                <div class="mobile-show-scroll">
                                    <strong>Lưu ý: </strong>trượt ngang bảng bên dưới để có thể xem hết thông tin
                                </div>
                                <div class="cont table-cont-overflow">
                                   
                                    <table class="tb-product">
                                        <tr>
                                            <th class="pro" style="vertical-align: middle; text-align: center;">Ngày yêu cầu xuất kho</th>
                                            <th class="pro" style="vertical-align: middle; text-align: center;">Cân nặng (kg)</th>
                                            <th class="pro" style="vertical-align: middle; text-align: center;">Số kiện</th>
                                            <th class="pro" style="vertical-align: middle; text-align: center;">Tổng tiền (tệ)</th>
                                            <th class="qty" style="vertical-align: middle; text-align: center;">Tổng tiền (VNĐ)</th>
                                            <th class="qty" style="vertical-align: middle; text-align: center;">HTVC</th>
                                            <th class="qty" style="vertical-align: middle; text-align: center;">Ghi Chú</th>

                                        </tr>
                                        <%--<tr>
                                            <td class="vermid-tecenter" rowspan="2">18/07/2018</td>
                                            <td class="vermid-tecenter">2</td>
                                            <td class="vermid-tecenter">32</td>
                                            <td class="vermid-tecenter">116.960</td>
                                        </tr>
                                        <tr>
                                            <td class="vermid-tecenter">2</td>
                                            <td class="vermid-tecenter">32</td>
                                            <td class="vermid-tecenter">116.960</td>
                                        </tr>--%>
                                        <asp:Literal ID="ltrExportHistory" runat="server"></asp:Literal>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="order-panel print5">
                            <div class="title">Lịch sử thanh toán </div>
                            <div class="mobile-show-scroll">
                                <strong>Lưu ý: </strong>trượt ngang bảng bên dưới để có thể xem hết thông tin
                            </div>
                            <div class="cont table-cont-overflow">
                                
                                <table class="tb-product">
                                    <tr>
                                        <th class="pro">Ngày thanh toán</th>
                                        <th class="pro">Loại thanh toán</th>
                                        <th class="pro">Hình thức thanh toán</th>
                                        <th class="qty">Số tiền</th>
                                    </tr>
                                    <asp:Repeater ID="rptPayment" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="pro">
                                                    <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td class="pro">
                                                    <%# PJUtils.ShowStatusPayHistory( Eval("Status").ToString().ToInt()) %>
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %>
                                                </td>
                                                <td class="qty"><%#Eval("Amount","{0:N0}") %> VNĐ
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="printcontent" class="printdetail" style="display: none;">
                </div>
                <asp:HiddenField ID="hdfCommentText" runat="server" />
                <asp:HiddenField ID="hdfShopID" runat="server" />
                <asp:HiddenField ID="hdfOrderID" runat="server" />
            </div>

        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Chi tiết đơn hàng</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
    <asp:Button ID="btnPostComment" runat="server" Style="display: none;" OnClick="btnPostComment_Click" CausesValidation="false" />
    <asp:Button ID="btnDeposit" runat="server" CssClass="btn pill-btn primary-btn" Style="display: none" CausesValidation="false" Text="Đặt cọc" OnClick="btnDeposit_Click" />
    <asp:HiddenField ID="hdfListPackage" runat="server" />
    <asp:HiddenField ID="hdfmID" runat="server" />
    <script>
        $(document).ready(function () {
            var countpackages = $("#<%=hdfListPackage.ClientID%>").val();
            if (countpackages != "0") {
                $("#danhsachkien").show();
            }
            else {
                $("#danhsachkien").hide();
            }
        });
        $(function () {
            var chat = $.connection.chatHub;
            $.connection.hub.start().done(function () {
                $('#sendnotecomment').click(function () {
                    var obj = $('#sendnotecomment');
                    var parent = obj.parent();
                    var commentext = parent.find(".comment-text").val();
                    var shopid = obj.attr("order");
                    if (commentext == "" || commentext == null) {
                        alert("Vui lòng không để trống nội dung");
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "/chi-tiet-don-hang.aspx/PostComment",
                            data: "{commentext:'" + commentext + "',shopid:'" + shopid + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var data = msg.d;
                                if (data != "0") {
                                    var outdata = data.split('|');
                                    var id = outdata[0];
                                    var message = outdata[1];
                                    swal({
                                        title: 'Thông báo',
                                        text: 'Gửi nội dung thành công',
                                        type: 'success'
                                    },
                                        function () {
                                            window.location.replace(window.location.href);
                                        });
                                    chat.server.send("Đánh giá",
                                        "<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + id + "','" + shopid + "','2')\">" + message + "</a></li>");
                                }
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                //alert('lỗi post comment');
                            }
                        });
                    }
                });
            });
        });
    </script>
    <asp:HiddenField ID="hdfListShippingVN" runat="server" />
    <script type="text/javascript">
        function printDiv() {
            var html = "";
            $('link').each(function () { // find all <link tags that have
                if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                    html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                }
            });
            html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
            var w = window.open("", "print");
            if (w) { w.document.write(html); w.document.close() }
        }
        $(document).ready(function () {
            $(".print1").clone().appendTo(".printdetail");
            $(".print2").clone().appendTo(".printdetail");
            //$(".print3").clone().appendTo(".printdetail");
            $(".print4").clone().appendTo(".printdetail");
            $(".print5").clone().appendTo(".printdetail");
        });
        function payallorder() {
            var r = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (r == true) {
                $("#<%= btnPayAll.ClientID%>").click();
            }
            else {
            }
        }
        function cancelOrder() {
            var r = confirm("Bạn muốn hủy đơn hàng này?");
            if (r == true) {
                $("#<%= btn_cancel.ClientID%>").click();
            }
            else {
            }
        }
        function depositOrder() {
            var r = confirm("Bạn muốn đặt cọc đơn hàng này?");
            if (r == true) {
                $("#<%= btnDeposit.ClientID%>").click();
            }
            else {
            }
        }
        function PrintDiv() {
            var contents = document.getElementById("dvContents").innerHTML;
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>DIV Contents</title>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        }
        function postcomment(obj) {
            var parent = obj.parent();
            var commentext = parent.find(".comment-text").val();
            var shopid = obj.attr("order");
            if (commentext == "" || commentext == null) {
                alert("Vui lòng không để trống nội dung");
            }
            else {
                $("#<%= hdfCommentText.ClientID%>").val(commentext);
                $("#<%= hdfShopID.ClientID%>").val(shopid);
                $("#<%= btnPostComment.ClientID%>").click();

            }
        }
        function showsmallpackagedetail(obj) {
            var id = obj.attr("data-smallpackageid");
            var mainorderid = obj.attr("data-mainorderid");
            var status = parseFloat(obj.attr("data-smallpackagestatus"));
            var weight = obj.attr("data-smallpackageweight");
            var code = obj.attr("data-smallpackagecode");
            var checkisout = obj.attr("data-issendoutstock");
            var staffnote = obj.attr("data-smallstaffnote");
            var html = "";
            html += "<div id=\"transactioncodeList1\" class=\"cont\">";
            //if (status == 3) {
            //    if (checkisout == "0")
            //    {
            //        html += "   <div class=\"ordercode smallpackage-item isxuatkhook\" data-packageID=\"" + id + "\">";
            //    }                    
            //    else
            //        html += "   <div class=\"ordercode smallpackage-item\" data-packageID=\"" + id + "\">";
            //}
            //else
            //{

            //}
            html += "   <div class=\"ordercode smallpackage-item\" data-packageID=\"" + id + "\">";
            html += "       <div class=\"item-element\">";
            html += "           <span>Mã Vận đơn:</span>";
            html += "           <input class=\"sm-transactionCode form-control\" type=\"text\" placeholder=\"Mã vận đơn\" value=\"" + code + "\" disabled/>";
            html += "       </div>";
            html += "       <div class=\"item-element\">";
            html += "           <span>Cân nặng:</span>";
            html += "           <input class=\"sm-transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"" + weight + "\" disabled/><br/>";
            html += "           <span>Quy đổi cm3 ( chiều dài * chiều rộng * chiều cao / 6000 ):</span>";
            html += "           <textarea class=\"sm-transactionStaffnote form-control\" type=\"text\" placeholder=\"Quy đổi\" disabled>" + staffnote + "</textarea>";
            html += "       </div>";
            //html += "       <div class=\"item-element\">";
            //html += "           <span>Ghi chú:</span>";
            //html += "           <input class=\"sm-staffnote form-control\" type=\"text\" placeholder=\"Ghi chú\" value=\"" + staffnote + "\" disabled/>";
            //html += "       </div>";
            html += "       <div class=\"item-element\">";
            html += "           <span>Trạng thái:</span>";
            html += "           <select class=\"sm-transactionCodeStatus form-control\" disabled>";
            if (status == 1)
                html += "               <option value=\"1\" selected>Chưa về kho TQ</option>";
            else
                html += "               <option value=\"1\">Chưa về kho TQ</option>";
            if (status == 2)
                html += "               <option value=\"2\" selected>Đã về kho TQ</option>";
            else
                html += "               <option value=\"2\">Đã về kho TQ</option>";
            if (status == 3)
                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
            else
                html += "               <option value=\"3\">Đã về kho đích</option>";
            if (status == 4)
                html += "               <option value=\"4\" selected>Đã giao khách hàng</option>";
            else
                html += "               <option value=\"4\">Đã giao khách hàng</option>";

            html += "           </select>";
            html += "       </div>";
            html += "   </div>";
            html += "</div>";
            html += "<span id=\"error-txt\"></span>";
            var button = "";
            if (status == 4)
                button = "<a href=\"/them-khieu-nai/" + mainorderid + "/" + code + "\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" >Khiếu nại</a>";
            else if (status == 3) {
                //if (checkisout == "0")
                //    button = "<a href=\"javascript:;\" onclick=\"requestoutstock()\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" id=\"requestbtn\">Yêu cầu xuất kho</a>";
            }

            showPopup("Chi tiết mã vận đơn: " + code + "", html, button);
        }
        function addLoading() {
            $("#home").append("<div class=\"addloading\"></div>");
        }
        function removeLoading() {
            $(".addloading").remove();
        }
        function requestoutstockAll1() {
            if ($(".isxuatkhook").length > 0) {
                var c = confirm('Bạn muốn xuất kho tất cả các kiện?');
                if (c == true) {
                    var listID = "";
                    var mID = $("#<%=hdfmID.ClientID%>").val();
                    $(".isxuatkhook").each(function () {
                        var obj = $(this);
                        var id = obj.attr("data-smallpackageid");
                        listID += id + "|";
                    });
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/chi-tiet-don-hang.aspx/requestoutstockall",
                        data: "{listID:'" + listID + "', mID:'" + mID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret != "none") {
                                if (ret == "0") {
                                    swal
                                        (
                                            {
                                                title: 'Thông báo',
                                                text: 'Vui lòng thanh toán đơn hàng trước khi yêu cầu giao hàng.',
                                                type: 'error'
                                            }
                                            //function () { window.location.replace(window.location.href); }
                                         );
                                }
                                else {
                                    var ids = listID.split('|');
                                    if (ids.length - 1 > 0) {
                                        for (var i = 0; i < ids.length - 1; i++) {
                                            var id = ids[i];
                                            $(".packageorderitem").each(function () {
                                                if ($(this).attr("data-smallpackageid") == id) {
                                                    $(this).attr("data-issendoutstock", "1");
                                                    $(this).removeClass("isxuatkhook");
                                                }
                                            })
                                        }
                                        swal
                                        (
                                            {
                                                title: 'Thông báo',
                                                text: 'Gửi yêu cầu xuất kho thành công',
                                                type: 'success'
                                            }
                                            //function () { window.location.replace(window.location.href); }
                                         );
                                    }
                                }

                                //close_popup_ms();
                            }
                            removeLoading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            $("#error-txt").html("Có gián đoạn trong quá trình gửi yêu cầu, vui lòng thử lại sau.").attr("style", "color:red").show();
                            removeLoading();
                            //alert('lỗi checkend');
                        }
                    });
                }
            }
            else {
                alert('Hiện tại không có kiện thích hợp để yêu cầu xuất kho.');
            }
        }
        function requestoutstock() {
            if ($(".smallpackage-item").length > 0) {
                var obj = $(".smallpackage-item");
                var id = obj.attr("data-packageid");
                var mID = $("#<%=hdfmID.ClientID%>").val();
                addLoading();
                $.ajax({
                    type: "POST",
                    url: "/chi-tiet-don-hang.aspx/requestoutstock",
                    data: "{id:'" + id + "', mID:'" + mID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret != "none") {
                            if (ret == "0") {
                                $("#error-txt").html("Vui lòng thanh toán đơn hàng trước khi yêu cầu giao hàng.").attr("style", "color:red").show();
                            }
                            else {
                                $("#requestbtn").remove();
                                $(".packageorderitem").each(function () {
                                    if ($(this).attr("data-smallpackageid") == id)
                                        $(this).attr("data-issendoutstock", "1");
                                })
                                $("#error-txt").html("Gửi yêu cầu thành công.").attr("style", "color:blue").show();
                            }
                            //close_popup_ms();
                        }
                        else {
                            $("#error-txt").html("Có gián đoạn trong quá trình gửi yêu cầu, vui lòng thử lại sau.").attr("style", "color:red").show();
                        }
                        removeLoading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        $("#error-txt").html("Có gián đoạn trong quá trình gửi yêu cầu, vui lòng thử lại sau.").attr("style", "color:red").show();
                        removeLoading();

                        //alert('lỗi checkend');
                    }
                });

            }
        }
        function selectExport() {
            var check = false;
            $(".chk-export").each(function () {
                if ($(this).is(':checked')) {
                    check = true;
                }
            });
            if (check == true) {
                $("#exportselected").show();
                $("#exportselected1").show();
            }
            else {
                $("#exportselected").hide();
                $("#exportselected1").hide();
            }
        }
        function requestoutstockAll() {
            if ($(".isxuatkhook").length > 0) {
                var c = confirm('Bạn muốn xuất kho tất cả các kiện?');
                if (c == true) {
                    var listID = "";
                    $(".isxuatkhook").each(function () {
                        var obj = $(this);
                        var id = obj.attr("data-smallpackageid");
                        listID += id + "|";
                    });
                    $.ajax({
                        type: "POST",
                        url: "/chi-tiet-don-hang.aspx/GetPrice",
                        data: "{listID:'" + listID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $("#<%= hdfListID.ClientID%>").val(listID);
                            var ret = msg.d;
                            var data = ret.split(':');
                            var money = parseFloat(data[0]);
                            var status = parseFloat(data[1]);
                            var weight = parseFloat(data[2]);
                            var moneyCYN = parseFloat(data[3]);
                            var totalPriceOrder = parseFloat(data[4]);
                            var totalWeightPrice = parseFloat(data[5]);
                            var totalWeightPriceCYN = parseFloat(data[6]);
                            if (status == 1) {
                                //var money = parseFloat(msg.d);
                                if (money > 0) {
                                    var strMoney = formatThousands(money, 0);
                                    var strMoneyCYN = formatThousands(moneyCYN, 0);
                                    var strTotalPrice = formatThousands(totalPriceOrder, 0);
                                    var strtotalWeightPrice = formatThousands(totalWeightPrice, 0);
                                    var strtotalWeightPriceCYN = formatThousands(totalWeightPriceCYN, 0);
                                    var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"paytoexport($(this))\">Thanh toán</a>";
                                    var html = "";
                                    html += "<div class=\"popup-row\">";
                                    html += "   <p>Số tiền hàng còn thiếu của bạn : <strong>" + strTotalPrice + "</strong> ( Là 20% tiền hàng còn thiếu + cước nội địa + phụ phí hiển thị cuối đơn hàng.)</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng cân nặng yêu cầu xuất kho: " + weight
                                        + " kg. Quy đổi: <strong>¥" + strtotalWeightPriceCYN + "</strong> - Quy đổi bằng: <strong>" + strtotalWeightPrice + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng tiền cần thanh toán ( bao gồm tiền hàng còn thiếu và cước vận chuyển Trung Quốc – Việt Nam): <strong>" + strMoney + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    //html += "<p>Tổng cân nặng yêu cầu xuất kho: " + weight
                                    //    + " kg. Bạn cần thanh toán số tiền: <strong>" + strMoney
                                    //    + "</strong> VNĐ - Quy đổi: ¥" + strMoneyCYN + " để yêu cầu xuất kho.</p>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                    var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                    var lists = s.split('|');
                                    if (lists.length - 1 > 0) {
                                        html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                        html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                        for (var i = 0; i < lists.length - 1; i++) {
                                            var item = lists[i].split(':');
                                            var sID = item[0];
                                            var sName = item[1];
                                            html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                        }
                                        html += "</select>";
                                    }
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                    html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuperror\" style=\"color:red\"></span>";
                                    html += "</div>";
                                    //showPopup("Thanh toán xuất kho", "Bạn cần thanh toán số tiền: <strong>" + strMoney
                                    //    + "</strong> VNĐ để yêu cầu xuất kho.", button);
                                    showPopup("Thanh toán xuất kho", html, button);
                                }
                            }
                            else {
                                //var money = parseFloat(msg.d);
                                if (money > 0) {
                                    var rechargeMore = parseFloat(data[7]);
                                    var wallet = parseFloat(data[8]);

                                    var strMoney = formatThousands(money, 0);
                                    var strMoneyCYN = formatThousands(moneyCYN, 0);
                                    var strTotalPrice = formatThousands(totalPriceOrder, 0);
                                    var strtotalWeightPrice = formatThousands(totalWeightPrice, 0);
                                    var strtotalWeightPriceCYN = formatThousands(totalWeightPriceCYN, 0);
                                    var strrechargeMore = formatThousands(rechargeMore, 0);
                                    var strwallet = formatThousands(wallet, 0);

                                    var button = "";
                                    var html = "";
                                    html += "<div class=\"popup-row\">";
                                    //html += "<p>Tổng cân nặng yêu cầu xuất kho: " + weight + " kg. Bạn cần thanh toán số tiền: <strong>" + strMoney + "</strong> VNĐ - Quy đổi: ¥" + strMoneyCYN + " để yêu cầu xuất kho.</p>";
                                    html += "<p>Số tiền hàng còn thiếu của bạn : <strong>" + strTotalPrice + "</strong> ( Là 20% tiền hàng còn thiếu + cước nội địa + phụ phí hiển thị cuối đơn hàng.)</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng cân nặng yêu cầu xuất kho: <strong>" + weight
                                        + "</strong> kg. Quy đổi: <strong>¥" + strtotalWeightPriceCYN + "</strong> - Quy đổi bằng: <strong>" + strtotalWeightPrice + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng tiền cần thanh toán ( bao gồm tiền hàng còn thiếu và cước vận chuyển Trung Quốc – Việt Nam): <strong>" + strMoney + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tài khoản của bạn còn : <strong>" + strwallet + "</strong> VNĐ. Bạn còn thiếu <strong>" + strrechargeMore + "</strong> VNĐ để thanh toán thành công.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuperror\" style=\"color:red\">Để nạp thêm tiền vào tài khoản, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a>.</span>";
                                    //html += "<span class=\"popuperror\" style=\"color:red\">Hiện tại tài khoản của bạn không đủ để thanh toán, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a> để nạp tiền vào tài khoản. Xin cám ơn. </span>";
                                    html += "</div>";
                                    showPopup("Thanh toán xuất kho", html, button);
                                }
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
            }
            else {
                alert('Hiện tại không có kiện thích hợp để yêu cầu xuất kho.');
            }
        }
        function requestexportselect() {
            var html = "";
            $(".chk-export").each(function () {
                if ($(this).is(':checked')) {
                    html += $(this).attr("data-smallpackageid") + "|";
                }
            });
            $("#<%= hdfListID.ClientID%>").val(html);
            $.ajax({
                type: "POST",
                url: "/chi-tiet-don-hang.aspx/GetPrice",
                data: "{listID:'" + html + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    var data = ret.split(':');
                    var money = parseFloat(data[0]);
                    var status = parseFloat(data[1]);
                    var weight = parseFloat(data[2]);
                    var moneyCYN = parseFloat(data[3]);
                    var totalPriceOrder = parseFloat(data[4]);
                    var totalWeightPrice = parseFloat(data[5]);
                    var totalWeightPriceCYN = parseFloat(data[6]);
                    if (status == 1) {
                        //var money = parseFloat(msg.d);
                        if (money > 0) {
                            var strMoney = formatThousands(money, 0);
                            var strMoneyCYN = formatThousands(moneyCYN, 0);
                            var strTotalPrice = formatThousands(totalPriceOrder, 0);
                            var strtotalWeightPrice = formatThousands(totalWeightPrice, 0);
                            var strtotalWeightPriceCYN = formatThousands(totalWeightPriceCYN, 0);
                            var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"paytoexport($(this))\">Thanh toán</a>";
                            var html = "";
                            html += "<div class=\"popup-row\">";
                            html += "   <p>Số tiền hàng còn thiếu của bạn : <strong>" + strTotalPrice + "</strong> ( Là 20% tiền hàng còn thiếu + cước nội địa + phụ phí hiển thị cuối đơn hàng.)</p>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "<p>Tổng cân nặng yêu cầu xuất kho: " + weight
                                + " kg. Quy đổi: <strong>¥" + strtotalWeightPriceCYN + "</strong> - Quy đổi bằng: <strong>" + strtotalWeightPrice + "</strong> VNĐ.</p>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "<p>Tổng tiền cần thanh toán ( bao gồm tiền hàng còn thiếu và cước vận chuyển Trung Quốc – Việt Nam): <strong>" + strMoney + "</strong> VNĐ.</p>";
                            html += "</div>";
                            //html += "<p>Tổng cân nặng yêu cầu xuất kho: " + weight
                            //    + " kg. Bạn cần thanh toán số tiền: <strong>" + strMoney
                            //    + "</strong> VNĐ - Quy đổi: ¥" + strMoneyCYN + " để yêu cầu xuất kho.</p>";
                            html += "<div class=\"popup-row\">";
                            html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                            var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                            var lists = s.split('|');
                            if (lists.length - 1 > 0) {
                                html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                for (var i = 0; i < lists.length - 1; i++) {
                                    var item = lists[i].split(':');
                                    var sID = item[0];
                                    var sName = item[1];
                                    html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                }
                                html += "</select>";
                            }
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                            html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "<span class=\"popuperror\" style=\"color:red\"></span>";
                            html += "</div>";
                            //showPopup("Thanh toán xuất kho", "Bạn cần thanh toán số tiền: <strong>" + strMoney
                            //    + "</strong> VNĐ để yêu cầu xuất kho.", button);
                            showPopup("Thanh toán xuất kho", html, button);
                        }
                    }
                    else {
                        //var money = parseFloat(msg.d);
                        if (money > 0) {
                            var rechargeMore = parseFloat(data[7]);
                            var wallet = parseFloat(data[8]);

                            var strMoney = formatThousands(money, 0);
                            var strMoneyCYN = formatThousands(moneyCYN, 0);
                            var strTotalPrice = formatThousands(totalPriceOrder, 0);
                            var strtotalWeightPrice = formatThousands(totalWeightPrice, 0);
                            var strtotalWeightPriceCYN = formatThousands(totalWeightPriceCYN, 0);
                            var strrechargeMore = formatThousands(rechargeMore, 0);
                            var strwallet = formatThousands(wallet, 0);

                            var button = "";
                            var html = "";
                            html += "<div class=\"popup-row\">";
                            //html += "<p>Tổng cân nặng yêu cầu xuất kho: " + weight + " kg. Bạn cần thanh toán số tiền: <strong>" + strMoney + "</strong> VNĐ - Quy đổi: ¥" + strMoneyCYN + " để yêu cầu xuất kho.</p>";
                            html += "   <p>Số tiền hàng còn thiếu của bạn : <strong>" + strTotalPrice + "</strong> ( Là 20% tiền hàng còn thiếu + cước nội địa + phụ phí hiển thị cuối đơn hàng.)</p>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "<p>Tổng cân nặng yêu cầu xuất kho: <strong>" + weight
                                 + "    </strong> kg. Quy đổi: <strong>¥" + strtotalWeightPriceCYN + "</strong> - Quy đổi bằng: <strong>" + strtotalWeightPrice + "</strong> VNĐ.</p>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "   <p>Tổng tiền cần thanh toán ( bao gồm tiền hàng còn thiếu và cước vận chuyển Trung Quốc – Việt Nam): <strong>" + strMoney + "</strong> VNĐ.</p>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "   <p>Tài khoản của bạn còn : <strong>" + strwallet + "</strong> VNĐ. Bạn còn thiếu <strong>" + strrechargeMore + "</strong> VNĐ để thanh toán thành công.</p>";
                            html += "</div>";
                            html += "<div class=\"popup-row\">";
                            html += "<span class=\"popuperror\" style=\"color:red\">Để nạp thêm tiền vào tài khoản, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a>.</span>";
                            //html += "<span class=\"popuperror\" style=\"color:red\">Hiện tại tài khoản của bạn không đủ để thanh toán, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a> để nạp tiền vào tài khoản. Xin cám ơn. </span>";
                            html += "</div>";
                            showPopup("Thanh toán xuất kho", html, button);
                        }
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }
        function paytoexport(obj) {
            //var html = "";
            //$(".chk-export").each(function () {
            //    if ($(this).is(':checked')) {
            //        html += $(this).attr("data-smallpackageid") + "|";
            //    }
            //});

            var html = $("#<%= hdfListID.ClientID%>").val();
            var shippingtype = parseFloat($(".shippingtypevn").val());
            var note = $(".requestnote").val();
            if (shippingtype > 0) {
                obj.removeAttr("onclick");
                $("#<%=hdfListID.ClientID%>").val(html);
                $("#<%=hdfNote.ClientID%>").val(note);
                $("#<%=hdfShippingType.ClientID%>").val(shippingtype);
                $("#<%=btnyeucau.ClientID%>").click();
                $(".popuperror").html("");
            }
            else {
                $(".popuperror").html("Vui lòng chọn hình thức vận chuyển.");
            }
        }
        var formatThousands = function (n, dp) {
            var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
            while ((i -= 3) > 0) { r = '.' + s.substr(i, 3) + r; }
            return s.substr(0, i + 3) + r +
              (d ? ',' + Math.round(d * Math.pow(10, dp || 2)) : '');
        };
        //Khu vực popup
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip_home").animate({ "opacity": 0 }, 400);
            $("#bg_popup_home").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip_home").remove();
                $(".zoomContainer").remove();
                $("#bg_popup_home").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
                $('form').css('overflow', 'auto');
            }, 500);
        }
        function showPopup(title, content, button) {
            var obj = $('form');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup_home'></div>";
            var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                     "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
            fr += "<div class=\"popup_header\">";
            fr += title;
            fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "</div>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <div class=\"content1\">";
            fr += content;
            fr += "         </div>";
            fr += "         <div class=\"content2\">";
            fr += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='close_popup_ms()'>Đóng</a>";
            //fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"addOrderShopCode('" + shopID + "', '" + MainOrderID + "')\">Thêm</a>";
            fr += button;
            fr += "         </div>";
            fr += "     </div>";
            fr += "<div class=\"popup_footer\">";
            //fr += "<span class=\"float-right\">" + email + "</span>";
            fr += "</div>";
            fr += "   </div>";
            fr += "</div>";
            $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
            $(fr).appendTo($(obj));
            setTimeout(function () {
                $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                $("#bg_popup").attr("onclick", "close_popup_ms()");
            }, 1000);
        }
        //End khu vực popup
    </script>
    <asp:HiddenField ID="hdfNote" runat="server" />
    <asp:HiddenField ID="hdfListID" runat="server" />
    <asp:HiddenField ID="hdfShippingType" runat="server" />
    <asp:Button ID="btnyeucau" runat="server" OnClick="btnyeucau_Click" Style="display: none" />
    <style>
        .vermid-tecenter {
            vertical-align: middle !important;
            text-align: center;
        }

        .popup-row {
            float: left;
            width: 100%;
            clear: both;
            margin: 10px 0;
        }

        .popuprow-left {
            float: left;
            width: 30%;
        }

        .popuprow-right {
            float: left;
            width: 60%;
        }

        .content1 {
            float: left;
            width: 100%;
        }

        .content2 {
            float: left;
            width: 100%;
            border-top: 1px solid #eee;
            clear: both;
            margin-top: 10px;
        }

        .ordercodes {
            width: 100%;
        }

        .ordercode {
            float: left;
            width: 100%;
            clear: both;
            margin-bottom: 10px;
        }

            .ordercode .item-element {
                float: left;
                width: 33%;
                padding: 0 10px;
            }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url('/App_Themes/NewUI/images/icon-plus.png') center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }

        .btn.btn-close {
            float: right;
            background: #29aae1;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 0 20px;
            font-weight: bold;
            text-transform: uppercase;
        }

        #bg_popup_home {
            position: fixed;
            width: 100%;
            height: 100%;
            background-color: #333;
            opacity: 0.7;
            filter: alpha(opacity=70);
            left: 0px;
            top: 0px;
            z-index: 999999999;
            opacity: 0;
            filter: alpha(opacity=0);
        }

        #popup_ms_home {
            background: #fff;
            border-radius: 0px;
            box-shadow: 0px 2px 10px #fff;
            float: left;
            position: fixed;
            width: 735px;
            z-index: 10000;
            left: 50%;
            margin-left: -370px;
            top: 200px;
            opacity: 0;
            filter: alpha(opacity=0);
            height: 360px;
        }

            #popup_ms_home .popup_body {
                border-radius: 0px;
                float: left;
                position: relative;
                width: 735px;
            }

            #popup_ms_home .content {
                /*background-color: #487175;     border-radius: 10px;*/
                margin: 12px;
                padding: 15px;
                float: left;
            }

            #popup_ms_home .title_popup {
                /*background: url("../images/img_giaoduc1.png") no-repeat scroll -200px 0 rgba(0, 0, 0, 0);*/
                color: #ffffff;
                font-family: Arial;
                font-size: 24px;
                font-weight: bold;
                height: 35px;
                margin-left: 0;
                margin-top: -5px;
                padding-left: 40px;
                padding-top: 0;
                text-align: center;
            }

            #popup_ms_home .text_popup {
                color: #fff;
                font-size: 14px;
                margin-top: 20px;
                margin-bottom: 20px;
                line-height: 20px;
            }

                #popup_ms_home .text_popup a.quen_mk, #popup_ms_home .text_popup a.dangky {
                    color: #FFFFFF;
                    display: block;
                    float: left;
                    font-style: italic;
                    list-style: -moz-hangul outside none;
                    margin-bottom: 5px;
                    margin-left: 110px;
                    -webkit-transition-duration: 0.3s;
                    -moz-transition-duration: 0.3s;
                    transition-duration: 0.3s;
                }

                    #popup_ms_home .text_popup a.quen_mk:hover, #popup_ms_home .text_popup a.dangky:hover {
                        color: #8cd8fd;
                    }

            #popup_ms_home .close_popup {
                background: url("/App_Themes/Camthach/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                display: block;
                height: 28px;
                position: absolute;
                right: 0px;
                top: 5px;
                width: 26px;
                cursor: pointer;
                z-index: 10;
            }

        #popup_content_home {
            height: auto;
            position: fixed;
            background-color: #fff;
            top: 15%;
            z-index: 999999999;
            left: 25%;
            border-radius: 10px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 50%;
            padding: 20px;
        }

        #popup_content_home {
            padding: 0;
        }

        .popup_header, .popup_footer {
            float: left;
            width: 100%;
            background: #29aae1;
            padding: 10px;
            position: relative;
            color: #fff;
        }

        .popup_header {
            font-weight: bold;
            font-size: 16px;
            text-transform: uppercase;
        }

        .close_message {
            top: 10px;
            background: url('/App_Themes/1688/images/close_button.png') no-repeat;
        }

        .changeavatar {
            padding: 10px;
            margin: 5px 0;
            float: left;
            width: 100%;
        }

        .spackage-row {
            float: left;
            width: 100%;
        }
    </style>
</asp:Content>
