var DataTable = null;
$(document).ready(function() {

    DataTable = $("#datatable").DataTable({
        "ajax": {
            "url": "/DomainUser/DisableUserList",
            "dataSrc": ""
        },
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.12/i18n/Turkish.json"
        },
        "columns": [
            { "data": "Button" },
            { "data": "FirstName" },
            { "data": "SeconName" },
            { "data": "UserLogonName" },
            { "data": "JobTitle" },
            { "data": "Department" },
            { "data": "Manager" },
            { "data": "distinguishedName" },
            { "data": "lastlogon" }
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

function MakeDisable(value) {
    $.ajax({
        url: "../../DomainUser/MakeDisable",
        type: "POST",
        data: {
            Value: value
        },
        beforeSend: function() {
            showLoading();
        },
        success: function(result) {
            Swal.fire({
                title: "Kullanıcı Devre Dışı Bırakıldı",
                icon: "success",
                allowEscapeKey: false,
                allowOutsideClick: false,
                showConfirmButton: false,
                timer: 700
            });
        }
    });
    DataTable.ajax.reload();
}