$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport: {
            read:
            {
                url: "https://localhost:44346/api/Employees",
                dataType: "json",
                contentType: "application/json"
            },
            destroy:
            {
                url: "https://localhost:44346/api/Employees",
                type: "DELETE"
            },
            create:
            {
                url: "https://localhost:44346/api/Employees",
                type: "POST"
            },
            update:
            {
                url: "https://localhost:44346/api/Employees",
                type: "PUT",
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return {
                            models: kendo.stringify(options.models)
                        };
                    }
                }
            },
        },
        schema:
        {
            model:
            {
                id: "empid",
                fields: {
                    id: { editable: false, nullable: true },
                    firstName: { editable: true, nullable: true, type: "string" },
                    lastName: { editable: true, nullable: true, type: "string" },
                    contactNumber: { editable: true, nullable: true, type: "string" },
                    email: { editable: true, nullable: true, type: "string" },
                    address: { editable: true, nullable: true, type: "string" },
                }
            }
        }
    });
    $("#grid1").kendoGrid({
        dataSource: dataSource,
        groupable: true,
        sortable: true,
        editable: "inline",
        toolbar: ["create"],
        columns: [
            {
                field: "id",
                title: "#",
                hidden: true
            },
            {
                field: "firstName",
                title: "First Name"
            },
            {
                field: "lastName",
                title: "Last Name"
            },
            {
                field: "contactNumber",
                title: "ContactNumber"
            },
            {
                field: "email",
                title: "Email"
            },
            {
                field: "address",
                title: "Address"
            },
            {
                command: ["edit",
                    {
                        name: "destroy",
                        text: "Remove",
                    }
                ],
            }
        ],
        height: "500px",
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
    }).data("kendoGrid");
});

function error_handler(e) {
    var errors = $.parseJSON(e.xhr.responseText);

    if (errors) {
        alert("Errors:\n" + errors.join("\n"));
    }
}