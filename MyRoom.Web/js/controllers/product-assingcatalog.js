'use strict';
/* Controllers */
// Product Assign Catalog controller
app.controller('AssignProductCataloguesController', ['$scope', '$http', '$state', 'productService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, productService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    var IdCatalog = 0;
    $scope.toaster = {
        type: 'success',
        title: 'Info',
        text: 'The Assign Product to Catalog has been saved'
    };
    $scope.pop = function () {
        toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
    };
    $scope.initTabsets = function () {
        $scope.showTabsetCategory = false;
        $scope.showTabsetModule = true;
    }
    $scope.getAllProduct = function () {
        productService.getAll().then(function (response) {
            $scope.products = response.data.value;
            $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
            $scope.dtColumnDefs = [
                DTColumnDefBuilder.newColumnDef('Id'),
                DTColumnDefBuilder.newColumnDef('Name'),
                //DTColumnDefBuilder.newColumnDef(''),
                DTColumnDefBuilder.newColumnDef('Active')
            ];

        },
        function (err) {
            $scope.error_description = err.error_description;
        });
    };

    $scope.saveAssingProduct = function () {
        var i = 0;
        $("input[name='post[]']:checked").each(function () {
            //cada elemento seleccionado
            $scope.RelCategoryProducts = { IdCategory: $scope.currentItem.Id, IdProduct: $(this).val(), Active: true };
            productService.saveAssingProductCatalog($scope.RelCategoryProducts).then(function (response) {

                $scope.message = "The Assign Product to Catalog has been saved";
            },
            function (err) {
                $scope.error_description = err.error_description;
            });
            i++;
        });
        $scope.pop();
    }

    $scope.getAllProduct();
}]);