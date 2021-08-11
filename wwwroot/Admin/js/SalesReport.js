var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tbldata').DataTable({
        "ajax": {
            "url": "/admin/salesReport/AllSales",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "salesId", "width": "20%" },
            { "data": "nameOfCustomer", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "dateOfSale", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/Admin/SalesReport/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class="far fa-info"></i> Details
                            </a>
                        `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

