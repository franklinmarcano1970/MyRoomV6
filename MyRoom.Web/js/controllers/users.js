'use strict';

/* Controllers */
// user controller
app.controller('UsersController', ['$scope', '$http', '$state', 'accountService', 'toaster', '$timeout', function ($scope, $http, $state, accountService, toaster, $timeout) {
    $scope.account = {
        Name: '',
        Surname: '',
        Email: '',
        Password: '',
        ConfirmPassword: '',
        Active: true,
        PhoneNumberConfirmed: false,
        TwoFactorEnabled: false,
        LockoutEnabled: false,
        AccessFailedCount: 0,
        UserName: ''
    };
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The User has been saved'
    };
    if ($state.current.name == "app.page.user_edit" && $state.params['id'])
    {
        accountService.getUser($state.params['id']).then(function (response) {
            $scope.account = response.data;
        });
    };
   
    
    $scope.Mensaje = "";
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };

    $scope.saveUser = function () {
        if ($state.current.name == "app.page.user_create") {
            accountService.saveRegistration($scope.account).then(function (response) {
                //   $scope.Id = response.data.Id;
                $scope.account = { Active: true };
                $scope.toaster = {
                    type: 'success',
                    title: 'Success',
                    text: 'The User has been saved'
                };

                $timeout(function () {
                    $scope.pop();
                }, 1000);
                $state.go('app.page.user_list');

                //$scope.Mensaje = "The User has been saved";
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
        else {
            var userVm = $scope.createUserVm($scope.account);
            accountService.updateUser(userVm).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Success',
                    text: 'The User has been updated'
                };

                $timeout(function () {
                    $scope.pop();
                }, 1000);
                $state.go('app.page.user_list');
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

    $scope.createUserVm = function (entity) {
        return {
            UserId: entity.Id,
            Name: entity.Name,
            Surname: entity.Surname,
            Email: entity.Email,
            Password: entity.Password
        };
    };
   
    $scope.getAll = function () {
        accountService.all().then(function (response) {
            //   $scope.Id = response.data.Id;
            $scope.account = response;
        },
        function (err) {
            $scope.error_description = err.error_description;
        });
    };
}])

app.controller('UsersListController', ['$scope', '$http', '$state', 'accountService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, accountService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.users = {};
    $scope.currentUsers = {};
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The user has been removed'
    };

    angular.element(document).ready(function () {
        $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            DTColumnDefBuilder.newColumnDef(1),
            DTColumnDefBuilder.newColumnDef(2),
            DTColumnDefBuilder.newColumnDef(3),
            DTColumnDefBuilder.newColumnDef(4).notSortable(),
            DTColumnDefBuilder.newColumnDef(5).notSortable()
        ];
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
        //Select Current user with I press delete button
        $scope.selectUser = function (user) {
            $scope.currentUsers = user;
            $('#deleteUser').modal({
                show: 'true'
            });
        }

        //Button Create
        $scope.createUser = function () {
            $state.go('app.page.user_create');
        }
        $scope.clickMenuAccess = function () {
            $state.go('app.page.usermenuaccess');
        }
        $scope.clickHotelAccess = function () {
            $state.go('app.page.userhotel');
        }
        $scope.clickCatalogAccess = function () {
            $state.go('app.page.usercatalog');
        }
        $scope.modifyUser = function (id) {
            //$scope.currentUsers.Id = id;
            $state.go('app.page.user_edit', { "id": id});
        }

        //List of Users
        $scope.getAll = function () {
            accountService.getAll().then(function (response) {
                $scope.users = response.data;
             
            },
            function (err) {
                $scope.error_description = err.error_description;
            });
        };

        //Button delete
        $scope.removeUser = function (user) {
            accountService.removeUser(user).then(function (response) {
                //$scope.Id = response.data.Id;
                $scope.product = {
                    Active: true,
                    Image: 'img/prod.jpg',
                    Translation: {
                        Active: true
                    }
                };
                $scope.pop();
                $scope.getAll();
                $scope.message = "The user has been removed";
            },
            function (err) {
                $scope.error_description = err.error_description;
            });


        };

        $scope.getAll();
    });
}]);

app.controller('UsersHotelController', ['$scope', '$http', '$state', 'hotelService', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$filter', 'toaster', function ($scope, $http, $state, hotelService, DTOptionsBuilder, DTColumnDefBuilder, $filter, toaster) {
    //var ischeckedArray = $filter('ischeckedArray');
    $scope.IsNewRecord = 1;
    $scope.sw = 1;
    $scope.IdUser = 0;
    $scope.RelUserHotel = [{IdUser: 0, IdHotel: 0, ReadOnly: true, ReadWrite: 0}];
    
    $scope.hotels = [];
  
    $scope.getAll = function () {
        
        hotelService.getAll().then(function (response) {
            $scope.hotels = response.data;
            $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');

            $scope.dtColumnDefs = [
                DTColumnDefBuilder.newColumnDef(0),
                DTColumnDefBuilder.newColumnDef(1),
                DTColumnDefBuilder.newColumnDef(2).notSortable()
            ];
        },
        function (err) {
            $scope.error_description = err.error_description;
        });
    };
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    $scope.saveUserHotel = function () {
        var permissions = [];
        if (!$scope.person.selected) {
            $scope.toaster = { type: 'info', title: 'Info', text: 'Please select a user' };
            $scope.pop();
        }
        else {
            $scope.hotels.filter(function (value) {
                if (value.checked == true) {
                    permissions.push({ IdUser: $scope.person.selected.id, IdHotel: value.HotelId });
                }
            });

            if (permissions.length == 0) {
                permissions.push({ IdUser: $scope.person.selected.id });
            }
            hotelService.saveUserPermission(permissions).then(function (response) {
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Permission has been assigned' };
                $scope.pop();
            },
            function (err) {
                $scope.toaster = { type: 'success', title: 'Info', text: err.error_description };
                $scope.pop();
            });
        }
    }
    $scope.getAll();
}])

app.controller('UsersMenuAccessController', ['$scope', '$http', '$state', 'accountService', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$filter', 'toaster', function ($scope, $http, $state, accountService, DTOptionsBuilder, DTColumnDefBuilder, $filter, toaster) {
    var ischeckedArray = $filter('ischeckedArray');
    $scope.IsNewRecord = 1;
    $scope.sw = 1;
    $scope.IdUser = 0;
    $scope.RelUserHotel = [{ IdUser: 0, IdHotel: 0, ReadOnly: true, ReadWrite: 0 }];

    $scope.hotels = [];
    $scope.getAll = function () {
        accountService.getAllMenuAccess().then(function (response) {
            $scope.menus = response.data.value;
            $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');

            $scope.dtColumnDefs = [
                DTColumnDefBuilder.newColumnDef('Id'),
                DTColumnDefBuilder.newColumnDef('MainMenuOption')

            ];
        },
        function (err) {
            $scope.toaster = { type: 'success', title: 'Info', text: err.error_description };
            $scope.pop();
        });
    };
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    $scope.saveUserMenuAccess = function () {
        var passed = ischeckedArray($scope.hotels);

        $("input[name='post[]']:checked").each(function () {
            //cada elemento seleccionado
            $scope.RelUserAccess = { IdUser: $scope.IdUser, IdPermission: $(this).val()};
            accountService.saveUserMenuAccess($scope.RelUserAccess).then(function (response) {
                $scope.toaster = {
                    type: 'success',
                    title: 'Info',
                    text: 'The User Menu Access has been saved'
                };
                $scope.pop();
            },
            function (err) {
                $scope.toaster = { type: 'success', title: 'Info', text: err.error_description };
                $scope.pop();
            });
            i++;
        });
       
    }
    $scope.getAll();
}])
