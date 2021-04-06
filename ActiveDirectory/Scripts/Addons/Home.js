$(document).ready(function() {
    $.ajax({
        url: "../Home/DevreDisi",
        type: "GET",
        success: function(result) {
            var ss = [
                "svg-icon-primary", "svg-icon-danger", "svg-icon-info", "svg-icon-warning", "svg-icon-secondary",
                "svg-icon-light"
            ];
            document.getElementById("count").innerHTML = "Toplam Devre Dışı Kullanıcı Sayısı: " + result.length;
            $.each(result,
                function(i, item) {
                    if (i <= 6) {
                        $("#Context").append(
                            '<div class="d-flex align-items-center justify-content-between mb-10" >' +
                            '<div class="d-flex align-items-center mr-2" id="Context">' +
                            '<div class="symbol symbol-40 symbol-light-primary mr-3 flex-shrink-0">' +
                            '<div class="symbol-label">' +
                            '<span class="svg-icon ' +
                            ss[i] +
                            ' svg-icon-2x"><!--begin::Svg Icon | path:C:\wamp64\www\keenthemes\themes\metronic\theme\html\demo1\dist/../src/media/svg/icons\General\User.svg--><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">' +
                            '<g stroke = "none" stroke - width="1" fill = "none" fill - rule="evenodd" >' +
                            '<polygon points="0 0 24 0 24 24 0 24" />' +
                            '<path d="M12,11 C9.790861,11 8,9.209139 8,7 C8,4.790861 9.790861,3 12,3 C14.209139,3 16,4.790861 16,7 C16,9.209139 14.209139,11 12,11 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />' +
                            '<path d="M3.00065168,20.1992055 C3.38825852,15.4265159 7.26191235,13 11.9833413,13 C16.7712164,13 20.7048837,15.2931929 20.9979143,20.2 C21.0095879,20.3954741 20.9979143,21 20.2466999,21 C16.541124,21 11.0347247,21 3.72750223,21 C3.47671215,21 2.97953825,20.45918 3.00065168,20.1992055 Z" fill="#000000" fill-rule="nonzero" />   </g ></svg ></span >' +
                            "</div></div >" +
                            "<div>" +
                            '<a href="#" class="font-size-h6 text-dark-75 text-hover-primary font-weight-bolder">' +
                            item.FirstName +
                            " " +
                            item.LastName +
                            "</a>" +
                            '<div class="font-size-sm text-muted font-weight-bold mt-1"></div></div> </div >' +
                            '<div class="label label-light label-inline font-weight-bold text-dark-50 py-4 px-3 font-size-base">' +
                            item.LogonName +
                            "</div> </div>"
                        );
                    }
                });
        },
        error: function(request, error) {
            ErrorSwall();
        }
    });
    $.ajax({
        url: "../Home/TotalUser",
        type: "GET",
        success: function(e) {
            document.getElementById("totalcount").innerHTML = e;
        },
        error: function(request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
});
$(document).ready(function() {
    $.ajax({
        url: "../Home/DisableUserCount",
        type: "GET",
        success: function(e) {
            document.getElementById("eight").innerHTML = e[2];
            document.getElementById("nine").innerHTML = e[1];
            document.getElementById("thirt").innerHTML = e[0];
        },
        error: function(e) {
            ErrorSwall();
        }
    });
});
$(document).ready(function() {
    $("#datatable").DataTable({
        "order": [[0, "desc"]],
        "ajax": {
            "url": "/Home/OnayBekleyen",
            "dataSrc": ""
        },
        "columns": [
            { "data": "FirstName" },
            { "data": "SeconName" },
            { "data": "UserLogonName" },
            { "data": "TelephoneNumber" },
            { "data": "button" }
        ],
        dom:
            '<"row"<"col-md-12"<"row"<"col-md-6"B><"col-md-6"f> > ><"col-md-12"rt> <"col-md-12"<"row"<"col-md-5"i><"col-md-7"p>>> >',
    });
});
function RenderCratesss(url) {
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
            ErrorSwall();
        }
    });
}
$(document).ready(function() {
    $.ajax({
        url: "../Home/Chart1",
        type: "GET",
        success: function(e) {
            var options = {
                series: [
                    {
                        name: "User Adding Rate",
                        data: e
                    }
                ],
                chart: {
                    height: 350,
                    type: 'line',
                    zoom: {
                        enabled: true
                    }
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    width: 7,
                    curve: 'smooth'
                },
                grid: {
                    row: {
                        colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
                        opacity: 0.5
                    }
                },
                xaxis: {
                    labels: {
                        format: 'MM'
                    },
                    categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
                },
                fill: {
                    type: 'gradient',
                    gradient: {
                        shade: 'dark',
                        gradientToColors: ['#FDD835'],
                        shadeIntensity: 1,
                        type: 'horizontal',
                        opacityFrom: 1,
                        opacityTo: 1,
                        stops: [0, 100, 100, 100]
                    },
                },
                markers: {
                    size: 4,
                    colors: ["#FFA41B"],
                    strokeColors: "#fff",
                    strokeWidth: 2,
                    hover: {
                        size: 7,
                    }
                },
                yaxis: {
                    min: -10,
                    max: 40,
                    title: {
                        text: 'Count',
                    }
                }
            };
            var chart = new ApexCharts(document.querySelector("#chart"), options);
            chart.render();
        },
        error: function(e) {
            ErrorSwall();
        }
    });
});