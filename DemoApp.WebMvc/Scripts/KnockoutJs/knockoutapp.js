var viewModel = function () {
    var self = this;
    self.id = ko.observable();
    self.firstName = ko.observable();
    self.lastName = ko.observable();
    self.contactNumber = ko.observable();
    self.email = ko.observable();
    self.address = ko.observable();
    self.title = ko.observable();
    self.employeeList = ko.observableArray([]);
    //var employeeUri = '/api/Employees/';
    var employeeUri = 'https://localhost:44346/api/Employees/';

    //self.currentEmployee = ko.observable(null);
    //self.showEmployee = function (vm) {
    //    self.currentEmployee(vm);
    //    $('#myModal').modal('show');
    //    };


    function ajaxFunction(uri, method, data) {
        //self.errorMessage('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert('Error : ' + errorThrown);
        });
    }
    // Clear Fields
    self.clearFields = function clearFields() {
        self.firstName('');
        self.lastName('');
        self.contactNumber('');
        self.email('');
        self.address('');
    }
    //Add new Employee

    self.title("Add New Employee");
    self.addNewEmployee = function addNewEmployee(newEmployee) {
        var employeeObject = {
            id: self.id(),
            firstName: self.firstName(),
            lastName: self.lastName(),
            contactNumber: self.contactNumber(),
            email: self.email(),
            address: self.address(),
        };

        ajaxFunction(employeeUri, 'POST', employeeObject).done(function () {
            self.clearFields();
            alert('Employee Added Successfully !');
            getEmployeeList()
        });
    }
    //Get Employee List
    function getEmployeeList() {
        $("div.loadingZone").show();
        ajaxFunction(employeeUri, 'GET').done(function (data) {
            $("div.loadingZone").hide();
            self.employeeList(data);
        });
    }
    //Get Detail Employee
    self.detailEmployee = function (selectedEmployee) {
        self.id(selectedEmployee.id);
        self.firstName(selectedEmployee.firstName);
        self.lastName(selectedEmployee.lastName);
        self.contactNumber(selectedEmployee.contactNumber);
        self.email(selectedEmployee.email);
        self.address(selectedEmployee.address);
        self.title("Update Employee");
        $('#Save').hide();
        $('#Clear').hide();
        $('#Update').show();
        $('#Cancel').show();
    };
    self.cancel = function () {
        self.clearFields();
        $('#Save').show();
        $('#Clear').show();
        $('#Update').hide();
        $('#Cancel').hide();
    }
    //Update Employee
    self.updateEmployee = function () {
        var EmployeeObject = {
            id: self.id(),
            firstName: self.firstName(),
            lastName: self.lastName(),
            contactNumber: self.contactNumber(),
            email: self.email(),
            address: self.address()
        };
        ajaxFunction(employeeUri + self.id(), 'PUT', EmployeeObject).done(function () {
            alert('Employee Updated Successfully !');
            getEmployeeList();
            self.cancel();
        });
    }
    //Delete Employee
    self.deletesEmployee = function () {
        alert('hey');
    }

    self.deleteEmployee = function (employee) {
        console.log('tst');
        ajaxFunction(employeeUri + employee.id, 'DELETE').done(function () {
            alert('Employee Deleted Successfully');
            getEmployeeList();
        })
    }

    getEmployeeList();
};
ko.applyBindings(new viewModel());
