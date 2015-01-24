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
            $scope.products = response.data;
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
        var categories = [];
        var products = [];
        //angular.forEach($scope.sourceItems, function (value, key) {
        //    debugger

        //});
        //$scope.sourceItems.filter(function (value) {
        //    if (value.ActiveCheckbox == true) {
        //    }
        //});
        $scope.sourceItems = jQuery.grep($scope.sourceItems, function (element, index) {
            return element.ActiveCheckbox == true; // retain appropriate elements
        });
        $scope.products.filter(function (value) {
            if (value.checked == true) {
                //categories.push({ IdUser: $scope.person.selected.id, IdPermission: value.MenuAccessId });
            }
        });


        //if (permissions.length == 0) {
        //    permissions.push({ IdUser: $scope.person.selected.id });
        //}

        //accountService.savePermissions(permissions).then(function (response) {
        //    //$scope.message = "The Product has been saved";
        //    $scope.toaster = { type: 'success', title: 'Info', text: 'The Permission has been assigned' };
        //    $scope.pop();
        //},
        //function (err) {
        //    $scope.toaster = { type: 'success', title: 'Info', text: err.error_description };
        //    $scope.pop();
        //});

        //var i = 0;
        //$("input[name='post[]']:checked").each(function () {
        //    //cada elemento seleccionado
        //    $scope.RelCategoryProducts = { IdCategory: $scope.currentItem.Id, IdProduct: $(this).val(), Active: true };
        //    productService.saveAssingProductCatalog($scope.RelCategoryProducts).then(function (response) {

        //        $scope.message = "The Assign Product to Catalog has been saved";
        //    },
        //    function (err) {
        //        $scope.error_description = err.error_description;
        //    });
        //    i++;
        //});
        //$scope.pop();
    }

    $scope.getAllProduct();
}]);