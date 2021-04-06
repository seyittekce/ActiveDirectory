$(document).ready(function() {
    $("#datatable").DataTable({
        "order": [[7, "desc"]],
        "ajax": {
            "url": "/DomainUser/PendingUserList",
            "dataSrc": ""
        },
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.12/i18n/Turkish.json"
        },
        "columns": [
            { "data": "FirstName" },
            { "data": "SeconName" },
            { "data": "YttGoster" },
            { "data": "UserLogonName" },
            { "data": "JobTitle" },
            { "data": "Department" },
            { "data": "Manager" },
            { "data": "EklenenDate" },
            { "data": "DurumGoster" },
            { "data": "button" }
        ],
        buttons: {
            buttons: [
                {
                    extend: "pdf",
                    text: '<i class="far fa-file-pdf"></i>',
                    titleAttr: "Excel",
                    className: "btn btn-secondary"
                },
                {
                    extend: "excelHtml5",
                    text: '<i class="far fa-file-excel"></i>',
                    titleAttr: "Excel",
                    className: "btn btn-secondary"
                }
            ]
        },
        "dom": "<'row'<'col-sm-6'l><'col-sm-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
    });
});

function SendAction(url) {
    $.ajax({
        url: "/DomainUser/Islem",
        type: "Get",
        data: {
            ID: url,
        },
        beforeSend: function() {
            showLoading();
        },
        success: function(result) {
            $("#RenderCrate").html(result);
            $("#GlobalModal").modal("show");
        },
        complete: function(aa) {
            hideSwall();
        },
        error: function(request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}