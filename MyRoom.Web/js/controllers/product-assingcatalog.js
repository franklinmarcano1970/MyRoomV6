'use strict';
/* Controllers */
// Product Assign Catalog controller
app.controller('AssignProductCataloguesController', ['$scope', '$http', '$state', 'productService', 'catalogService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, productService, catalogService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
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
    $scope.getProductsByCategory = function () {
        catalogService.getProductsByCategory($scope.currentItem.CategoryId).then(function (response) {
                debugger

        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
    };

    $scope.getAllProduct = function () {
        productService.getAll().then(function (response) {
            $scope.products = response.data;
           // $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
            $scope.dtColumnDefs = [
                DTColumnDefBuilder.newColumnDef('Id'),
                DTColumnDefBuilder.newColumnDef('Name'),
                //DTColumnDefBuilder.newColumnDef(''),
                DTColumnDefBuilder.newColumnDef('Active')
            ];

            //for (var j = 0 ; j < $scope.product.ActiveProducts.length; j++) {
            //    for (var i = 0 ; i < $scope.products.length; i++) {
            //        if ($scope.products[i].Id == $scope.product.ActiveProducts[j].IdProduct) {
            //            $scope.products[i].checked = true;
            //        }
            //    }
            //}

        },
        function (err) {
            $scope.toaster = { type: 'success', title: 'Info', text: 'The Product cannot Assingned, Verified your connection' };
            $scope.pop();
        });
    };

    $scope.saveAssingProduct = function () {
        var categories = [];
        var products = [];

        if (!$scope.currentItem.CategoryId)
        {
            $scope.toaster = { type: 'info', title: 'Info', text: 'Please, select a final category and select products'};
            $scope.pop();
        }
        var vm = createCategoryProductsVm();
        catalogService.saveAssingProduct(vm).then(function (response) {
            $scope.toaster = { type: 'success', title: 'Success', text: 'The product has been assigned to category'};
            $scope.pop();
        },
        function (err) {
            $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
            $scope.pop();
        });
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


    function createCategoryProductsVm() {
        var vm = { 'ProductsIds': [] };
        vm.CategoryId = $scope.currentItem.CategoryId;

        $scope.products.filter(function (value) {
            if (value.checked == true) {
                vm.ProductsIds.push(value.Id);
            }
        });
        return vm;
    }

    $scope.getAllProduct();
}]);