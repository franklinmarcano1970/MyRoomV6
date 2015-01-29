app.controller('MenuAccessController', ['$scope', '$http', '$state', 'accountService', 'DTOptionsBuilder', 'DTColumnDefBuilder', '$filter', 'toaster', function ($scope, $http, $state, accountService, DTOptionsBuilder, DTColumnDefBuilder, $filter, toaster) {
    //var ischeckedArray = $filter('ischeckedArray');
    //$scope.IsNewRecord = 1;
    //$scope.sw = 1;
    //$scope.IdUser = 0;
    //$scope.RelUserHotel = [{ IdUser: 0, IdHotel: 0, ReadOnly: true, ReadWrite: 0 }];

    $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');

    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef('Id'),
        DTColumnDefBuilder.newColumnDef('MainMenuOption')

    ];

    //$scope.hotels = [];
    $scope.getMenuAll = function () {
        accountService.getAllMenuAccess().then(function (response) {
            $scope.menus = response.data;
        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
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
                $scope.toaster = { type: 'success', title: 'Info', text: 'The Permission has been assigned' };
                $scope.pop();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        }

    }
    $scope.getMenuAll();
    
}]);