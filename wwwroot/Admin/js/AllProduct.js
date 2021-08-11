var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#mytable').DataTable({
        "ajax": {
            "url": "/admin/Admin/AllProduct",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "units", "width": "30%" },
            { "data": "price", "width": "40%" },
            //{
            //    "data": "id",
            //    "render": function (data) {
            //        return `<div class="text-center"> 
            //                <a  class="text-primary" asp-action="AddQuantityById" asp-controller="Admin" asp-route-id="id" data-toggle="modal" data-target="#exampleModal">
            //                 <i class="fas fa-plus"></i>
            //                </a>
            //            `;
            //    }, "width": "20%"
            //}
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

