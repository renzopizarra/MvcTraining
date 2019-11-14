

var grid;
var dataView;
var slickdata = [];
var Popup, dataTable;
var urlEdit = "https://localhost:44364/EmployeesSlickGrids/AddOrEdit/";
var urlEdit1 = '@Url.Action(AddOrEdit, EmployeesSlickGrids)/';

function formatterGetId(row, cell, value, columnDef, dataContext) {
    return "<a class='btn btn-default btn-sm' style='margin-left:2px' onclick=Deleteemp(" + value + ")><i class='fa fa-trash'></i> Delete</a>"
}

function formatterEdit(row, cell, value, columDef, dataContext) {4
    //return "<a class='btn btn-default btn-sm' onclick=PopupForm('https://localhost:4436/EmployeesSlickGrids/AddOrEdit/'" + value + ")><i class='fa fa-pencil'></i>Update</a>"
    return "<a class='btn btn-default btn-sm' onclick=PopupForm('https://localhost:44364/EmployeesSlickGrids/AddOrEdit/" + value + "')><i class='fa fa-pencil'></i>Update</a>"
}


function forColumn(row, cell, columnID, fn) {
    var cellNode = grid.getCellNode(row, cell);
    var dataContext = grid.getDataItem(row);
    if (cellNode && grid.getColumns()[cell].id === columnID) {
        fn.call(this, cellNode, dataContext, row, cell);
    }
}

this.templates = {
    "deleterow": '<button href="#" class="recline-row-delete btn btn-default" title="Delete row">X</button>'
};
var columns = [
    { id: "id", name: "#", field: "id", cssClass: "cell-title", sortable: true, behavior: "selectAndMove", cssClass: "cell-selection", width: 40, resizable: false, selectable: false, },
    { id: "firstName", name: "First Name", field: "firstName", sortable: true },
    { id: "lastName", name: "Last Name", field: "lastName", sortable: true },
    { id: "contactNumber", name: "Contact Number", field: "contactNumber", sortable: true },
    { id: "email", name: "Email", field: "email", sortable: true },
    { id: "address", name: "Address", field: "address", sortable: true },
    {
        id: "id",
        name: "",
        field: "id",
        data: "id",
        width: 40,
        cssClass: "cell-title",
        formatter: formatterEdit
    },

    {
        id: "id",
        name: "",
        field: "id",
        data: "id",
        width: 40,
        cssClass: "cell-title",//row, cell, value, columnDef, dataContex
        formatter: formatterGetId
            
        
    }

];
var options = {
    enableCellNavigation: true,
    enableColumnReorder: false,
    editable: false,
    enableAddRow: false,
    //editable: true,
    //enableAddRow: true,
    //enableCellNavigation: true,
    //enableAutoResize: true,
    //enableHeaderMenu: true,
    enableFiltering: true,
    //autoEdit: false,
    //enableColumnReorder: false,
    multiColumnSort: true,
    forceFitColumns: true
    //enablePager: true
};

$(function () {
   
    $.getJSON("https://localhost:44346/api/Employees", function (items) { //API
        for (var i = 0; i < items.length; i++) {
            slickdata[i] = {
                id: items[i].id,
                firstName: items[i].firstName,
                lastName: items[i].lastName,
                contactNumber: items[i].contactNumber,
                email: items[i].email,
                address: items[i].address
            };
        }
        dataView = new Slick.Data.DataView({ inlineFilters: true });
        grid = new Slick.Grid("#myGrid", slickdata, columns, options);
        //grid.setSelectionModel(new Slick.RowSelectionModel());
        var pager = new Slick.Controls.Pager(dataView, grid, $("#pager"));

        //grid.setSelectionModel(new Slick.RowSelectionModel());
        //grid.setActiveCell(0, 0);

        dataView.onRowCountChanged.subscribe(function (e, args) {
            grid.updateRowCount();
            grid.render();
        });
        dataView.onRowsChanged.subscribe(function (e, args) {
            grid.invalidateRows(args.rows);
            grid.render();
        });

        dataView.beginUpdate();
        dataView.setItems(slickdata);
        dataView.endUpdate();
    });


})


function DeleteData(id) {
    if (confirm('Are you sure you want to delete this employee?')) {
        $.ajax({
            type: "POST",
            //url: '<a href="https://localhost:44364/EmployeesSlickGrids/Index/ + " id"+"> Delete</a>',
            url: '@Url.Action("Delete", "EmployeesSlickGrids")/' + id,
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })
                }
            }
        });
    }

};

function PopupForm(url) {
    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);

            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                title: 'Fill Employee Details',
                height: 500,
                width: 350,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });
        });
}


function SubmitForms(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    location.reload(true);

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })
                }
            }
        });
    }
    return false;
}

function Deleteemp(id) {
    if (confirm('Are you sure you want to delete this employee?')) {
        $.ajax({
            type: "POST",
            url: "https://localhost:44364/EmployeesSlickGrids/Delete/" + id,
            success: function (data) {
                if (data.success) {
                    location.reload(true);
                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })

                }
            }
        });
    }
}






