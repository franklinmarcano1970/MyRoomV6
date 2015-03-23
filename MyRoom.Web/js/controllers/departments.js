'use strict';

/* Controllers */
// Departments controller
app.controller('DepartmentListController', ['$scope', '$http', '$state', 'departmentService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, departmentService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.departments = {};
    $scope.currentDepartmentId = 0;;

    angular.element(document).ready(function () {
        $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            DTColumnDefBuilder.newColumnDef(1),
            DTColumnDefBuilder.newColumnDef(2),
            DTColumnDefBuilder.newColumnDef(3),
            DTColumnDefBuilder.newColumnDef(5).notSortable(),
            DTColumnDefBuilder.newColumnDef(6).notSortable(),
            DTColumnDefBuilder.newColumnDef(7).notSortable()
        ];
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
        //Button Create
        $scope.createDepartment = function () {
            $state.go('app.page.department_create');
        }
        $scope.getAll = function () {
            departmentService.getAll().then(function (response) {
                $scope.departments = response.data;
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();

            });
        };

        $scope.selectDepartment = function (id) {
            $scope.currentDepartmentId = id;
            $('#deleteDepartment').modal({
                show: 'true'
            });
        }

        $scope.modifyDepartment = function (id) {
            $scope.currentDepartmentId = id;
            $state.go('app.page.department_edit', { "id": id });
        }

        $scope.removeDepartment = function (id) {
            departmentService.removeDepartment(id).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Info',
                    text: 'The department has been removed'
                };
                $scope.department = {
                    Director: '',
                    Name: '',
                    Email: '',
                    Active: true,
                    HotelId: 0,
                    IsExternal: false,
                    Translation: {
                        Active: true
                    }
                };
                $scope.pop();
                $scope.getAll();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();

            });
        };

        $scope.getAll();
    });
}]);
app.controller('DepartmentsController', ['$scope', '$http', '$state', 'departmentService', 'toaster', '$timeout', '$injector', function ($scope, $http, $state, departmentService, toaster, $timeout, $injector) {
    $scope.currentHotelId = 0;

    $scope.department = {
        Director: '',
        Name: '',
        Email: '',
        Active: true,
        HotelId: 0,
        IsExternal: false,
        Translation: {
            Spanish: '',
            English: '',
            French: '',
            German: '',
            Language5: '',
            Language6: '',
            Language7: '',
            Language8: '',
            Active: true
        },
    };

    //$scope.toaster = {
    //    type: 'success',
    //    title: 'Info',
    //    text: 'The User has been saved'
    //};
   
    angular.element(document).ready(function () {
        var hotelService = $injector.get("hotelService");

        $scope.Mensaje = "";

        hotelService.getAll().then(function (response) {
            $scope.hotel = response.data;
            $scope.hotels = [$scope.hotel.length];

            angular.forEach($scope.hotel, function (value, key) {
                $scope.hotels[key] = { Id: value.HotelId, Name: value.Name };
            });
        });
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
        if ($state.current.name == "app.page.department_edit" && $state.params['id']) {
            departmentService.getDepartment($state.params['id']).then(function (response) {
                $scope.department = response.data;
                $scope.currentHotelId = response.data.HotelId;

                hotelService.getAll().then(function (response) {
                    $scope.hotel = response.data;
                    $scope.hotels = [$scope.hotel.length];

                    angular.forEach($scope.hotel, function (value, key) {
                        $scope.hotels[key] = { Id: value.HotelId, Name: value.Name };
                        if ($scope.currentHotelId != 0 && $scope.currentHotelId == value.HotelId) {
                            $scope.hotel.selected = $scope.hotels[key];
                        }
                    });

                },
                function (err) {
                     $scope.error_description = err.error_description;
                });
                
            });

        };

        $scope.selectActionHotel = function ()
        {
            $scope.department.HotelId = $scope.hotel.selected.Id;
        }

        $scope.saveDepartment = function () {
            if ($state.current.name == "app.page.department_create") {
                departmentService.saveDepartment($scope.department).then(function (response) {
                    $scope.department = {
                        Director: '',
                        Name: '',
                        Email: '',
                        Active: true,
                        HotelId: 0,
                        IsExternal: false,
                        Translation: {
                            Active: true
                        }
                    };
                    $timeout(function () {
                        $scope.toaster = {
                            type: 'success',
                            title: 'Success',
                            text: 'The Department has been saved'
                        };
                        $state.go('app.page.departments_list');
                        $scope.pop();
                    }, 1000);
                },
                function (err) {
                    $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                    $scope.pop();
                });
            }
            else {

                departmentService.updateDepartment($scope.department).then(function (response) {
                    $scope.toaster = {
                        type: 'success',
                        title: 'Success',
                        text: 'The Department has been updated'
                    };
                    $timeout(function () {
                        $scope.pop();

                    }, 2000).then(function () {
                       
                    });
                    $state.go('app.page.departments_list');

                },
                function (err) {
                    $scope.toaster = {
                        type: 'error',
                        title: 'Error',
                        text: err.error_description
                    };

                    $scope.pop();
                });
            }

           
        };

    });
  
}]);
