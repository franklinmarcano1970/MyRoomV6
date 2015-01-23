app.controller('MenuAccessController', ['$scope', '$http', '$state', 'accountService', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$filter', 'toaster', function ($scope, $http, $state, accountService, DTOptionsBuilder, DTColumnDefBuilder, $filter, toaster) {
    var ischeckedArray = $filter('ischeckedArray');
    $scope.IsNewRecord = 1;
    $scope.sw = 1;
    $scope.IdUser = 0;
    $scope.RelUserHotel = [{ IdUser: 0, IdHotel: 0, ReadOnly: true, ReadWrite: 0 }];

    $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');

    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef('Id'),
        DTColumnDefBuilder.newColumnDef('MainMenuOption')

    ];

    $scope.hotels = [];
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The User Menu Access has been saved'
    };
    $scope.getMenuAll = function () {
        accountService.getAllMenuAccess().then(function (response) {
            $scope.menus = response.data;
        },
        function (err) {
            $scope.error_description = err.error_description;
        });
    };
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    $scope.saveUserMenuAccess = function () {
        var permissions = [];
        if (!$scope.person.selected) {
            $scope.toaster = { type: 'info', title: 'Info', text: 'Please select a user' };
            $scope.pop();
        }
        else {
            $scope.menus.filter(function (value) {
                if (value.checked == true) {
                    permissions.push({ IdUser: $scope.person.selected.id, IdPermission: value.MenuAccessId });
                }
            });

            if (permissions.length == 0) {
                permissions.push({ IdUser: $scope.person.selected.id });
            }

            accountService.savePermissions(permissions).then(function (response) {
                //$scope.message = "The Product has been saved";
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Permission has been assigned' };
                $scope.pop();
            },
            function (err) {
                $scope.toaster = { type: 'success', title: 'Info', text: err.error_description };
                $scope.pop();
            });
        }

        //angular.forEach($scope.selectMenuUser, function (value, key) {
        //    $("input[name='post[]']").each(function () {
        //        var f = parseInt($(this).val());
        //        if ($("input[name='post[]']").eq(i).is(':checked') == false) {
        //            //Buscamos el registro para eliminarlo
        //            if (value.IdPermission == f) {
        //                accountService.removeUserMenu(value.Id).then(function (response) {
        //                    $scope.message = "The Product has been saved";
        //                },
        //                function (err) {
        //                    $scope.error_description = err.error_description;
        //                });
        //            } 
        //        }
        //        i++;
        //    });
        //});

        //$("input[name='post[]']:checked").each(function () {
        //    //cada elemento seleccionado
        //    //angular.forEach($scope.selectMenuUser, function (value, key) {
        //    //    if (value.IdPermission != $(this).val())
        //    //});
        //    var id = parseInt($(this).val());

            //});

    }
    $scope.getMenuAll();
    
}]);