var table = "table";
$(document).ready(function() {
    table = $("#datatable").DataTable({
        "order": [[6, "desc"]],
        "ajax": {
            "url": "/DomainData/GetUserList",
            "dataSrc": ""
        },
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.12/i18n/Turkish.json"
        },
        "columns": [
            {
                data: null,
                render: function(data, type, row) {
                    var stateNo = Math.floor(Math.random() * 8);
                    var states = [
                        "success",
                        "primary",
                        "danger",
                        "success",
                        "warning",
                        "dark",
                        "primary",
                        "info"
                    ];
                    var state = states[stateNo];
                    var output = null;
                    if (data.FirstName != null) {
                        if (data.Mail != null) {
                            output = '<div class="d-flex align-items-center">\
								<div class="symbol symbol-40 symbol-light-' +
                                state +
                                ' flex-shrink-0">\
									<span class="symbol-label font-size-h4 font-weight-bold">' +
                                data.FirstName.substring(0, 1) +
                                '</span>\
								</div>\
								<div class="ml-4">\
									<div class="text-dark-75 font-weight-bolder font-size-lg mb-0">' +
                                data.FirstName +
                                " " +
                                data.LastName +
                                '</div>\
									<a href="mailto:"' +
                                data.Mail +
                                'class="text-muted font-weight-bold text-hover-primary">' +
                                data.Mail +
                                "</a>\
								</div>\
							</div>";
                        } else {
                            output = '<div class="d-flex align-items-center">\
								<div class="symbol symbol-40 symbol-light-' +
                                state +
                                ' flex-shrink-0">\
									<span class="symbol-label font-size-h4 font-weight-bold">' +
                                data.FirstName.substring(0, 1) +
                                '</span>\
								</div>\
								<div class="ml-4">\
									<div class="text-dark-75 font-weight-bolder font-size-lg mb-0">' +
                                data.FirstName +
                                " " +
                                data.LastName +
                                '</div>\
									<a href="#" class="text-muted font-weight-bold text-hover-primary"></a>\
								</div>\
							</div>';
                        }
                    } else {
                        if (data.Mail != null) {
                            output = '<div class="d-flex align-items-center">\
								<div class="symbol symbol-40 symbol-light-' +
                                state +
                                ' flex-shrink-0">\
									<span class="symbol-label font-size-h4 font-weight-bold">O</span>\
								</div>\
								<div class="ml-4">\
									<div class="text-dark-75 font-weight-bolder font-size-lg mb-0"></div>\
									<a href="mailto:"' +
                                data.Mail +
                                'class="text-muted font-weight-bold text-hover-primary">' +
                                data.Mail +
                                "</a>\
								</div>\
							</div>";
                        } else {
                            output = '<div class="d-flex align-items-center">\
								<div class="symbol symbol-40 symbol-light-' +
                                state +
                                ' flex-shrink-0">\
									<span class="symbol-label font-size-h4 font-weight-bold">O</span>\
								</div>\
								<div class="ml-4">\
									<div class="text-dark-75 font-weight-bolder font-size-lg mb-0"></div>\
									<a href="#" class="text-muted font-weight-bold text-hover-primary"></a>\
								</div>\
							</div>';
                        }
                    }
                    return output;
                }
            },
            { "data": "LogonName" },
            { "data": "JobTitle" },
            { "data": "Department" },
            {
                data: null,
                render: function(data, type, row) {
                    return '<div class="font-weight-bold font-size-xs ">' + data.Manager + "</div>";
                }
            },
            {
                data: null,
                render: function(data, type, row) {
                    return '<div class="font-weight-bold font-size-xs">' + data.DistinguishedName + "</div>";
                }
            },
            { "data": "lastlogon" },
            { "data": "" }
        ],
        "columnDefs": [
            {
                "targets": -1,
                "data": null,
                "defaultContent":
                    "<button class='btn btn-link text-decoration-none' title='Göster'><i class='fas fa-eye'></i></button>"
            }
        ],
        buttons: [
            $.extend(true,
                {},
                {
                    extend: "pdf",
                    text: '<i class="far fa-file-pdf"></i>',
                    titleAttr: "Excel",
                    className: "btn btn-secondary",
                    orientation: "landscape",
                    pageSize: "LEGAL"
                }),
            $.extend(true,
                {},
                {
                    extend: "excelHtml5",
                    text: '<i class="far fa-file-excel"></i>',
                    titleAttr: "Excel",
                    className: "btn btn-secondary"
                }),
            $.extend(true,
                {},
                {
                    text: "Kullanıcı Oluştur",
                    action: function(e, dt, button, config) {
                        Create();
                    },
                    className: "btn btn-primary"
                })
        ],
        "dom": "<'row'" +
            "<'col-sm-3'B>" +
            "<'col-sm-6'l>" +
            "<'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>"
    });
    table.buttons().container().appendTo("#button-cont .col-sm-6:eq(0)");
    $("#datatable tbody").on("click",
        "button",
        function() {
            var data = table.row($(this).parents("tr")).data();
            Details(data.DistinguishedName);
        });
});

function Details(IDS) {
    $.ajax({
        url: "/DomainUser/UserDetails/",
        data: { ID: IDS },
        type: "GET",
        beforeSend: function() {
            showLoading();
        },
        success: function(result) {
            $("#RenderCrate").html(result);
        },
        complete: function(aa) {
            $("#GlobalModal").modal("show");
            hideSwall();
        },
        error: function(request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function TableReload() {
    table.ajax.reload();
}