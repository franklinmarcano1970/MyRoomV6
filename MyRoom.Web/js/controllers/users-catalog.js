app.controller('UserCatalogController', ['$scope', '$http', '$state', 'catalogService', 'toaster', '$timeout', '$filter', function ($scope, $http, $state, catalogService, toaster, $timeout, $filter) {
    $scope.saveUserCatalog = function () {
        var i = 0;
        //guardar el catalgo del user seleccionados
        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };
       
        createCatalogUserViewModel();
      
        catalogService.saveCatalogUser($scope.catalogUserVm).then(function (response) {
            $scope.toaster = { type: 'success', title: 'Success', text: 'The Permission has been saved' };
            $scope.pop();
        },
        function (err) {
            $scope.toaster = {
                type: 'error', title: 'Error', text: err.error_description
            };
            $scope.pop();
        });
             
    }

    $scope.saveUserCatalogPopup = function () {
        $('#savePermissions').modal({
            show: 'true'
        });
    }


    function createCatalogUserViewModel()
    {
       
        $scope.catalogUserVm = {
            UserId: $scope.IdUser, CatalogId: $scope.IdCatalog, Elements: createCatalog()
        }
    }

    function createCatalog()
    {
        var GetCheckedTreeNode = $filter('GetCheckedTreeNode');
        var items = GetCheckedTreeNode($scope.items, []);
        return items;
    }
}]);